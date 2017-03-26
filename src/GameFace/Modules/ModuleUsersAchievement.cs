using Nancy;
using Nancy.ModelBinding;
using GameFace.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameFace.Models;
using Microsoft.EntityFrameworkCore;
using Nancy.Owin;
using System;

namespace GameFace.Modules
{
    public class ModuleUsersAchievement: Nancy.NancyModule
    {
        public ModuleUsersAchievement() : base("userachieve")
            {

            Post("/", async args =>
            {
                var creationAttempt = this.Bind<UsersAchievements>();
                using (var database = new GameFaceContext())
                {
                    database.UsersAchievements.Add(creationAttempt);
                    await database.SaveChangesAsync();
                }

            });

            Get("/{id:int}", async args =>
            {
                int Id = args.id;
                using (var database = new GameFaceContext())
                {
                   // int Id = args.id;
                    List<string> liststring = new List<string>();
                    var users = database.Users.Include(x => x.userAchievements).Where(x => x.id == Id);
                    foreach (Users u in users)
                    {
                        foreach (UsersAchievements a in u.userAchievements)
                        {
                            var line = database.Achieve.Where(ac => ac.idAchieve == a.idAchievement).Single();
                            var tasks = database.Tasks.Where(t => t.id == line.idTask).Single();
                            liststring.Add(u.nickName + "; " + u.name + "; " + a.date + "; " + line.levelNeeded + "; " + line.idTask + "; "
                                + line.description + "; " + tasks.name + "; " + tasks.value + "; " + tasks. + "; ");
                        }
                    }

                    return await Task.FromResult(liststring);
                }
            });
            
          
            Get("/", async args =>
            {
                try
                {
                    using (var database = new GameFaceContext())
                    {
                       
                        List<string> liststring = new List<string>();
                        var users = database.Users.Include(x => x.userAchievements);
                        foreach (Users u in users)
                        {
                            foreach (UsersAchievements a in u.userAchievements)
                            {
                                var line = database.Achieve.Where(ac => ac.idAchieve== a.idAchievement).Single();
                                var tasks = database.Tasks.Where(t => t.id == line.idTask).Single();
                                liststring.Add(u.nickName + "; " + u.name + "; " + a.date + "; "+ line.levelNeeded +"; "+line.idTask + "; "
                                    +line.description + "; " + tasks.name + "; " + tasks.value + "; " + tasks. + "; ");
                            }
                        }

                        return await Task.FromResult(liststring);
                    }
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    return msg;
                }

            });


         
        }
    }
}
