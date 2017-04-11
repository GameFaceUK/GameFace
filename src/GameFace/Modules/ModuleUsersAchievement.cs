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
using GameFace.Struct;

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
                    DateTime date = DateTime.Now;
                    int quarterNumber = (date.Month - 1) / 3 + 1;
                    DateTime firstDayOfQuarter = new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1);
                    var userrecords = new List<UserRecords>();
                    string nickname = "";
                    int value = 0;
                    var liststring = new List<string>();
                    var users = database.Users.Include(x => x.xP).Where(x => x.id==iduser);
                    foreach (Users u in users)
                    {
                        foreach (XP xp in u.xP)
                        {
                            var tasks = database.Tasks.Where(t => t.id == xp.idTask).Single();
                            if (xp.date > firstDayOfQuarter)
                            {
                                userrecords.Add(new UserRecords(tasks.name, xp.date, tasks.value));
                            }
                            value += tasks.value;
                        }
                        nickname = u.nickName;
                    }

                    UserProfile list = new UserProfile(nickname, value, userrecords.OrderByDescending(c => c.Date).ToList());
                    return await Task.FromResult(list);
                }
            });

            Get("/history/{id:int}", async args =>
            {
                int iduser = args.id;
                using (var database = new GameFaceContext())
                {
                    var userrecords = new List<UserRecords>();
                    string nickname = "";
                    var liststring = new List<string>();
                    var users = database.Users.Include(x => x.xP).Where(x => x.id == iduser);
                    foreach (Users u in users)
                    {
                        foreach (XP xp in u.xP)
                        {
                            var tasks = database.Tasks.Where(t => t.id == xp.idTask).Single();
                            userrecords.Add(new UserRecords(tasks.name, xp.date, tasks.value));                           
                        }
                        nickname = u.nickName;
                    }
                    userrecords = userrecords.OrderByDescending(c => c.Date).ToList();
                    UserHistory list = new UserHistory(nickname, userrecords.Select(c => c.Value).Sum(), Helper.DevideBySprints(userrecords));
                    return await Task.FromResult(list);
                }
            });




            Get("/{name:string}", async args =>
            {
                string  namepar = args.name;
                using (var database = new GameFaceContext())
                {
                   // int Id = args.id;
                    List<string> liststring = new List<string>();
                    var users = database.Users.Include(x => x.xP).Where(x => x.nickName == namepar);
                    foreach (Users u in users)
                    {
                        foreach (XP a in u.xP)
                        {
                            var tasks = database.Tasks.Where(t => t.id == a.idTask).Single();
                            liststring.Add(u.nickName + "; " + u.name + "; " + a.date + "; " +"; " 
                                + tasks.name + "; " + tasks.value + "; " );
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
                                    +line.description + "; " + tasks.name + "; " + tasks.value + "; " + tasks.id+ "; ");
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
