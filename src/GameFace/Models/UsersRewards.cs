using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class UsersRewards
    {
        public int idUser { get; set; }
        public int idReward { get; set; }
        public bool hasClaimed { get; set; }


        public virtual Users users { get; set; }
        public virtual  Rewards reward { get; set; }

    }
}
