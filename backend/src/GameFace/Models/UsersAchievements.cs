using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class UsersAchievements
    {
        public int idUser { get; set; }
        public int idAchievement { get; set; }
        public DateTime date { get; set; }

        public virtual Users users { get; set; }
        public virtual Achieve achievement { get; set; }

    }
}
