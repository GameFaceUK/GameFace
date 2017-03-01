using GameFace.Modules;
using Nancy;
using Nancy.ModelBinding;
using GameFace.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace GameFace
{
    public class ModuleTaskCompletion : Nancy.NancyModule
    {

        public ModuleTaskCompletion():base("completetask")
        {

            Post("/", async args =>
            {
                try
                {
                    var creationAttempt = this.Bind<CompletingTasks>(c => c.id);
                    using (var database = new GameFaceContext())
                    {
                        database.CompletingTasks.Add(creationAttempt);
                        await database.SaveChangesAsync();
                    }
                }
               catch(Exception e)
                {
                    string message = e.Message;
                }
            });
        }
    }
}
