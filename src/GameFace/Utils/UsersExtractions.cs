using GameFace.Controllers;
using GameFace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                foreach (Users u in users)
                {
                    foreach (XP xp in u.xP)
                    {
                        if (xp.idTask == task)
                        {
                            var tasks = database.Tasks.Where(t => t.id == xp.idTask).Single();
                            experience += tasks.value;
                        }
                    }
                }
                return experience;
            }
        }

        public static void CheckAvailableAcievementsForXP(int user, int task)
        {
            var experience = GetExperienceUserByTask(user, task);
            var newexp = experience;
            using (var database = new GameFaceContext())
            {
               var tasks = database.Tasks.Where(t => t.id == task).Single();
               newexp += tasks.value;
               CheckAvailableAchievements(user, task, experience, newexp).GetAwaiter();
            }          
        }

        public static async Task CheckAvailableAchievements(int user, int task, int currentxp, int newxp)
        {
            var experience = GetExperienceUserByTask(user, task);
            using (var database = new GameFaceContext())
            {
                var achieve = database.Achieve.Where(a => a.idTask == task 
                && (((a.levelNeeded > currentxp) && (a.levelNeeded < newxp )) 
                || (a.levelNeeded == newxp) ||
                ((currentxp==0) && (a.levelNeeded < newxp)))).Single();
                var userachieve = new UsersAchievements
                {
                    idUser = user,
                    idAchievement = achieve.idAchieve,
                    date = DateTime.Now
                };
                InsertingRecords.InsertAchievements(userachieve).GetAwaiter();
            }            
        }


        public static List<Achieve> GetUserAchievements(int user)
        {
            var achievements = new List<Achieve>();

            using (var database = new GameFaceContext())
            {
                var users = database.Users.Include(x => x.userAchievements).Where(x => x.id == user);
                foreach (Users u in users)
                {
                    foreach (UsersAchievements a in u.userAchievements)
                    {
                        var line = database.Achieve.Where(ac => ac.idAchieve == a.idAchievement).Single();
                        for(var i=0;i<achievements.Count;i++)
                        {
                            if ((achievements[i].idTask ==line.idTask)&&(achievements[i].levelNeeded< line.levelNeeded))
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
