using Nancy;
using Nancy.ModelBinding;
using GameFace.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameFace.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameFace.Modules
{
    public class ModuleTasks : Nancy.NancyModule
    {

        public ModuleTasks() : base("tasks")
        {


            Post("/", async args =>
            {
                var creationAttempt = this.Bind<Tasks>(c => c.id);
                using (var database = new GameFaceContext())
                {
                    try
                    {
                        database.Tasks.Add(creationAttempt);
                        await database.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;                        
                    }
                    }
            });

            Get("/{Id:int}", async args =>
            {
                using (var database = new GameFaceContext())
                {
                    var tasks = database.Tasks;
                    Tasks taskFound = null;
                    foreach (var task in tasks)
                    {
                        if (task.id == args.Id)
                        {
                            taskFound = task;
                        }
                    }
                    return await Task.FromResult(taskFound);
                }
            });

            Get("/all", async args =>
            {
                using (var database = new GameFaceContext())
                {
                    var tasks = database.Tasks.ToList();                  
                    return await Task.FromResult(tasks);
                }
            });

            

            Delete("/{id:int}", async args =>
            {
                using (var database = new GameFaceContext())
                {
                    var tasks = await database.Tasks.FindAsync(args.id);
                    database.Tasks.Remove(tasks);
                    await database.SaveChangesAsync();
                }
            });


        }                  
    }
}
