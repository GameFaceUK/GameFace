using GameFace.Controllers;
using GameFace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFace.Utils
{
    public static class UsersExtractions
    {

        public static int GetExperienceUserByTask(int user, int task)
        {
            using (var database = new GameFaceContext())
            {
                int experience = 0;
                var users = database.Users.Include(x => x.xP).Where(x => x.id == user);
                foreach (var u in users)
                {
                    experience += (from xp in u.xP where xp.idTask == task select database.Tasks.Single(t => t.id == xp.idTask) into tasks select tasks.value).Sum();
                }
                return experience;
            }
        }

        public static UsersAchievements CheckAvailableAcievementsForXP(int user, int task)
        {
            var experience = GetExperienceUserByTask(user, task);
            var newexp = experience;
            using (var database = new GameFaceContext())
            {
               var tasks = database.Tasks.Single(t => t.id == task);
               newexp += tasks.value;
               return CheckAvailableAchievements(user, task, experience, newexp);
            }          
        }

        public static UsersAchievements CheckAvailableAchievements(int user, int task, int currentxp, int newxp)
        {
            using (var database = new GameFaceContext())
            {
                var achievements = database.Achieve.Where(a => a.idTask == task ).ToList();
                var achieve = achievements.FirstOrDefault(b => (b.levelNeeded > currentxp && b.levelNeeded < newxp)
                                                           || b.levelNeeded == newxp ||
                                                           (currentxp == 0 && b.levelNeeded > newxp));
                if (achieve == null)
                {
                    return null;
                }
                var userachieve = new UsersAchievements
                {
                    idUser = user,
                    idAchievement = achieve.idAchieve,
                    date = DateTime.Now
                };
                return userachieve;
            }            
        }


        public static List<Achieve> GetUserAchievements(int user)
        {
            var achievements = new List<Achieve>();

            using (var database = new GameFaceContext())
            {
                var users = database.Users.Include(x => x.userAchievements).Where(x => x.id == user);
                foreach (var u in users)
                {
                    foreach (var a in u.userAchievements)
                    {
                        var line = database.Achieve.Single(ac => ac.idAchieve == a.idAchievement);
                        for(var i=0;i<achievements.Count;i++)
                        {
                            if (achievements[i].idTask ==line.idTask && achievements[i].levelNeeded< line.levelNeeded)
                            {
                                achievements.Remove(achievements[i]);
                            }
                        }
                        achievements.Add(line);
                    }                   
                }
            }

            return achievements;
        }
  

    }
}
