using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class Tasks
    {
        public int id { get; set; }
        public string name { get; set; }
        public int desirability { get; set; }
        public int frequency { get; set; }
        public int efort { get; set; }

        public int value => efort *(2/frequency)+desirability;

        public virtual List<StatisticDatapoint> statisticDatapoint { get; set; }
        public virtual List<XP> xP { get; set; }
        public virtual List<Achieve> achievement { get; set; }
    }
}
