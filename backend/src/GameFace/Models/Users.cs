using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surName { get; set; }
        public string nickName { get; set; }
        public bool status { get; set; }

        public virtual List<UsersRewards> userRewards { get; set; }
        public virtual List<UsersAchievements> userAchievements { get; set; }
        public virtual List<StatisticDatapoint> statisticDatapoint { get; set; }     
        public virtual List<XP> xP { get; set; }
    }
}
