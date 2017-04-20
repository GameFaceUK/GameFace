using Nancy.ModelBinding;
using GameFace.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameFace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using GameFace.Struct;
using GameFace.Utils;

namespace GameFace.Modules
{
    public class ModuleUsersAchievement: Nancy.NancyModule
    {
        public ModuleUsersAchievement() : base("users")
            {

            Post("/", async args =>
            {
                var creationAttempt = this.Bind<XP>();
                using (var database = new GameFaceContext())
                {
                    try
                    {
                        var userachieve = UsersExtractions.CheckAvailableAcievementsForXP(creationAttempt.idUser, creationAttempt.idTask);
                        if (userachieve!=null)
                        {
                            database.UsersAchievements.Add(userachieve);
                        }
                        database.XP.Add(creationAttempt);
                        await database.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                    }
                }
            });

            Get("/profile/{id:int}", async args =>
            {
                int iduser = args.id;
                using (var database = new GameFaceContext())
                {
                    var firstDayOfQuarter = (DateTime.Now).FirstDayOfQuarter();
                    var userrecords = new List<UserRecords>();
                    string nickname = "";
                    int value = 0;
                    var users = database.Users.Include(x => x.xP).Where(x => x.id==iduser);
                    foreach (var u in users)
                    {
                        foreach (var xp in u.xP)
                        {
                            var tasks = database.Tasks.Single(t => t.id == xp.idTask);
                            if (xp.date >= firstDayOfQuarter)
                            {
                                userrecords = Helper.AddElement(userrecords, new UserRecords(tasks.name, 1, tasks.value));
                            }
                            value += tasks.value;
                        }
                        nickname = u.nickName;
                    }
                    var listach = UsersExtractions.GetUserAchievements(iduser);
                    var ach = listach.Select(elem => new UserAchieve(elem.description, elem.idTask)).ToList();
                    UserProfile list = new UserProfile(nickname, value, ach, userrecords.ToList());
                    return await Task.FromResult(list);
                }
            });

            Get("/history/{id:int}", async args =>
            {
                int iduser = args.id;
                using (var database = new GameFaceContext())
                {
                    var userrecords = new List<UserRecordsDate>();
                    var nickname = "";
                    var users = database.Users.Include(x => x.xP).Where(x => x.id == iduser);
                    foreach (var u in users)
                    {
                        userrecords.AddRange(from xp in u.xP let tasks = database.Tasks.Single(t => t.id == xp.idTask)
                                             select new UserRecordsDate(tasks.name, xp.date, tasks.value));
                        nickname = u.nickName;
                    }
                    var list = new UserHistory(nickname, userrecords.Select(c => c.Value).Sum(), Helper.DevideInSprints(userrecords));
                    return await Task.FromResult(list);
                }
            });
          

            Get("/xp/{id:int}/{task:int}", async args =>
            {
                int iduser = args.id;
                using (var database = new GameFaceContext())
                {
                    int experience =0;
                    var users = database.Users.Include(x => x.xP).Where(x => x.id == iduser);
                    foreach (var u in users)
                    {
                        experience += (from xp in u.xP where xp.idTask == args.task select database.Tasks.Single(t => t.id == xp.idTask) into tasks select tasks.value).Sum();
                    }
                    return await Task.FromResult(experience);
                }
            });

            
           
         
        }
    }
}
