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
                        var user = database.Users.Single(b => b.nickName == "marias");

                        var achievement = database.Entry(user)
                            .Collection(b => b.userAchievements)
                            .Query()
                            .ToList();
                        
                        return await Task.FromResult(achievement);
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
