using Nancy;
using Nancy.ModelBinding;
using GameFace.Controllers;
using GameFace.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace GameFace
{
    public class ModuleUsers: Nancy.NancyModule
    {

        public ModuleUsers() : base("user")
        {
            
            Post("/", async args =>
            {
                try
                {
                    var creationAttempt = this.Bind<Users>(c => c.id);
                    using (var database = new GameFaceContext())
                    {
                        database.Users.Add(creationAttempt);
                        await database.SaveChangesAsync();
                    }
                }
                catch(Exception e)
                {
                    string message = e.Message;
                }
               
            });


            Get("/{Id:int}", async args =>
            {
                using (var database = new GameFaceContext())
                {
                    int i = args.Id;
                    var user = database.Users.Single(u => u.id == i);
                    return await Task.FromResult(user);
                }
            });

            Get("/all", async args =>
            {
                using (var context = new GameFaceContext())
                {
                    var users = context.Users.Where(u => u.status == true).ToList();
                    List<string> userlist = new List <string>();
                    foreach (var user in users)
                    {
                            userlist.Add(user.nickName);
                    }
                    return await Task.FromResult(userlist);
                }
            });

            Delete("/{id:int}", async args =>
            {
                using (var database = new GameFaceContext())
                {
                    var users = database.Users;
                    foreach (var user in users)
                    {
                        if (user.id == args.Id)
                        {
                            database.Users.Remove(user);
                            await database.SaveChangesAsync();
                        }

                    }

                }
            });


            // all
            Get("/users", async arg => { return "Elena"; });

            Get("/users/{Id:int}", async arg => { return arg.Id; });

         }
    }



    public class TasksModule : Nancy.NancyModule
    {
        public TasksModule() : base("addtask")
        {
            Post("/", async args =>
            {
                var creationAttempt = this.Bind<Tasks>(c => c.id);
                using (var database = new GameFaceContext())
                {
                    database.Tasks.Add(creationAttempt);
                    await database.SaveChangesAsync();
                }
            });
        }
    }
  }
