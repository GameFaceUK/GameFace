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


            Get("/", async args =>
            {
                try
                {
                    using (var database = new GameFaceContext())
                    {
                        // var user = database.Users.Single(b => b.nickName == "dianam");

                        //var user = database.Users.Include(u => u.userAchievements)
                        //  .ThenInclude(u => u.achievement)
                        //  .ToList();


                        //Eager loading. 
                        List<string> liststring = new List<string>();
                        var users = database.Users.Include(x => x.userAchievements);
                        foreach (Users u in users)
                        {
                            foreach (UsersAchievements a in u.userAchievements)
                            {
                                
                                 var line = database.Achieve.Where(ac => ac.idAchieve== a.idAchievement).Single();
                                liststring.Add(u.nickName + "; " + u.name + "; " + a.date + "; "+ line.levelNeeded +"; "+line.idTask + "; "+line.description + "; ");
                            }
                        }


                        //Explicit loading.
                        //List<string> liststring = new List<string>();
                        //var users = database.Users.ToList();
                        //foreach (Users u in users)
                        //{
                        //    database.Entry(u).Collection(c => c.userAchievements).Load();
                        //    foreach (UsersAchievements a in u.userAchievements)
                        //    {
                        //        liststring.Add(u.nickName + "; " + u.name + "; " + a.date + "; ");
                        //    }
                        //}

                        //   var achievement = database.Entry(user).Collection(b => b.userAchievements).Query().ToList();

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
