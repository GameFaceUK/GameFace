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
    public class ModuleAchievements: Nancy.NancyModule
        {

            public ModuleAchievements() : base("achieve")
            {

                Post("/", async args =>
                {
                        var creationAttempt = this.Bind<Achieve>(c => c.idAchieve);
                    using (var database = new GameFaceContext())
                    {
                        try
                        {
                            database.Achieve.Add(creationAttempt);
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
                    try
                    {
                        using (var database = new GameFaceContext())
                        {
                            int i = args.Id;
                            var achieve = database.Achieve.Single(c => c.idAchieve == i);
                            return await Task.FromResult(achieve);
                        }
                    }
                    catch(Exception e)
                    {
                        string msg = e.Message;
                        return msg;
                    }
                    
                });               


                Delete("/{id:int}", async args =>
                {
                    try
                    {
                        using (var database = new GameFaceContext())
                        {
                            int i = args.id;
                            var achieve = database.Achieve.Single(a => a.idAchieve ==i);
                            database.Achieve.Remove(achieve);
                            await database.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                    }
                });
                                    
        }
    }
}
