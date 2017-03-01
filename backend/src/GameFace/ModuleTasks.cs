using GameFace.Modules;
using Nancy;
using Nancy.ModelBinding;
using GameFace.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GameFace
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
                    database.Tasks.Add(creationAttempt);
                    await database.SaveChangesAsync();
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
                    var tasks = database.Tasks;                  
                    return await Task.FromResult(tasks.ToList());
                }
            });

            Get("/{nickame:string}" , async args =>
            {
                using (var context = new GameFaceContext())
                {
                    var records = context.CompletingTasks.Include(x => x.Users).Include(x => x.Tasks);
                    //var tasks = context.Entry(records)
                    //    .Collection(c => c.date)
                    //    .Query()
                    //    .ToList();
                    return await Task.FromResult(records.ToList());

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
