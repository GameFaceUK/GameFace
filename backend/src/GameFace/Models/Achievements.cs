using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class Achieve
    {
        public int idAchieve { get; set; }
        public int idTask { get; set; }
        public int levelNeeded { get; set; }
        public string description { get; set; }

        public virtual Tasks tasks { get; set; }
        public virtual List<UsersAchievements> usersAchievement { get; set; }
       
     }
}
