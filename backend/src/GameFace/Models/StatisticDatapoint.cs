using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class StatisticDatapoint
    {
        public int id { get; set; }
        public int idUser { get; set; }
        public int idStatistic { get; set; }
        public int idTask { get; set; }
        public string description { get; set; }

        public virtual Users users { get; set; }
        public virtual StatisticType statistics { get; set; }
        public virtual Tasks tasks { get; set; }
    }
}
