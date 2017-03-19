using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace GameFace
{
    public class MyModelBinderFactory : IModelBinderFactory
        {
            private readonly IModelMetadataProvider _metadataProvider;
            private readonly IModelBinderProvider[] _providers;

            private readonly ConcurrentDictionary<object, IModelBinder> _cache;

          
            public MyModelBinderFactory(IModelMetadataProvider metadataProvider, IOptions<MvcOptions> options)
            {
                _metadataProvider = metadataProvider;
                _providers = options.Value.ModelBinderProviders.ToArray();

                _cache = new ConcurrentDictionary<object, IModelBinder>();
            }

            /// <inheritdoc />
            public IModelBinder CreateBinder(ModelBinderFactoryContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                // We perform caching in CreateBinder (not in CreateBinderCore) because we only want to
                // cache the top-level binder.
                IModelBinder binder;
                if (context.CacheToken != null && _cache.TryGetValue(context.CacheToken, out binder))
                {
                    return binder;
                }

                var providerContext = new DefaultModelBinderProviderContext(this, context);
                binder = CreateBinderCore(providerContext, context.CacheToken);
                if (binder == null)
                {
                    var message = $"Could not create model binder for {providerContext.Metadata.ModelType}.";
                    throw new InvalidOperationException(message);
                }

                if (context.CacheToken != null)
                {
                    _cache.TryAdd(context.CacheToken, binder);
                }

                return binder;
            }

            private IModelBinder CreateBinderCore(DefaultModelBinderProviderContext providerContext, object token)
            {
                if (!providerContext.Metadata.IsBindingAllowed)
                {
                    return NoOpBinder.Instance;
                }

                
                var key = new Key(providerContext.Metadata, token);
                           
                var collection = providerContext.Collection;

                IModelBinder binder;
                if (collection.TryGetValue(key, out binder))
                {
                    if (binder != null)
                    {
                        return binder;
                    }

                    // Recursion detected, create a DelegatingBinder.
                    binder = new PlaceholderBinder();
                    collection[key] = binder;
                    return binder;
                }
                               
                collection.Add(key, null);

                IModelBinder result = null;

                for (var i = 0; i < _providers.Length; i++)
                {
                    var provider = _providers[i];
                    result = provider.GetBinder(providerContext);
                    if (result != null)
                    {
                        break;
                    }
                }

                if (result == null && token == null)
                {
                    // Use a no-op binder if we're below the top level. At the top level, we throw.
                    result = NoOpBinder.Instance;
                }

                // If the DelegatingBinder was created, then it means we recursed. Hook it up to the 'real' binder.
                var delegatingBinder = collection[key] as PlaceholderBinder;
                if (delegatingBinder != null)
                {
                    delegatingBinder.Inner = result;
                }

                collection[key] = result;
                return result;
            }

            private class DefaultModelBinderProviderContext : ModelBinderProviderContext
            {
                private readonly MyModelBinderFactory _factory;

                public DefaultModelBinderProviderContext(
                    MyModelBinderFactory factory,
                    ModelBinderFactoryContext factoryContext)
                {
                    _factory = factory;
                    Metadata = factoryContext.Metadata;
                    BindingInfo = factoryContext.BindingInfo;

                    MetadataProvider = _factory._metadataProvider;
                    Collection = new Dictionary<Key, IModelBinder>();
                }

                private DefaultModelBinderProviderContext(
                    DefaultModelBinderProviderContext parent,
                    ModelMetadata metadata)
                {
                    Metadata = metadata;

                    _factory = parent._factory;
                    MetadataProvider = parent.MetadataProvider;
                    Collection = parent.Collection;

                    BindingInfo = new BindingInfo()
                    {
                        BinderModelName = metadata.BinderModelName,
                        BinderType = metadata.BinderType,
                        BindingSource = metadata.BindingSource,
                        PropertyFilterProvider = metadata.PropertyFilterProvider,
                    };
                }

                public override BindingInfo BindingInfo { get; }

                public override ModelMetadata Metadata { get; }

                public override IModelMetadataProvider MetadataProvider { get; }

                // Not using a 'real' Stack<> because we want random access to modify the entries.
                public Dictionary<Key, IModelBinder> Collection { get; }

                public override IModelBinder CreateBinder(ModelMetadata metadata)
                {
                    var nestedContext = new DefaultModelBinderProviderContext(this, metadata);
                    return _factory.CreateBinderCore(nestedContext, token: null);
                }
            }

            [DebuggerDisplay("{ToString(),nq}")]
            private struct Key : IEquatable<Key>
            {
                private readonly ModelMetadata _metadata;
                private readonly object _token; // Explicitly using ReferenceEquality for tokens.

                public Key(ModelMetadata metadata, object token)
                {
                    _metadata = metadata;
                    _token = token;
                }

                public bool Equals(Key other)
                {
                    return _metadata.Equals(other._metadata) && object.ReferenceEquals(_token, other._token);
                }

                public override bool Equals(object obj)
                {
                    var other = obj as Key?;
                    return other.HasValue && Equals(other.Value);
                }

                public override int GetHashCode()
                {
                    return _metadata.GetHashCode() ^ RuntimeHelpers.GetHashCode(_token);
                }

                public override string ToString()
                {
                    if (_metadata.MetadataKind == ModelMetadataKind.Type)
                    {
                        return $"{_token} (Type: '{_metadata.ModelType.Name}')";
                    }
                    else
                    {
                        return $"{_token} (Property: '{_metadata.ContainerType.Name}.{_metadata.PropertyName}' Type: '{_metadata.ModelType.Name}')";
                    }
                }
            }
        }
    }