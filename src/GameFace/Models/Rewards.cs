using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class Rewards
    {
        public int idReward { get; set; }
        public string description { get; set; }

        public virtual List<UsersRewards> userRewards { get; set; }
    }
}
