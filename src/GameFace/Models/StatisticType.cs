using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class StatisticType
    {
        public int idStatistic { get; set; }
        public string description { get; set; }

        public virtual List<StatisticDatapoint> statisticDatapoint { get; set; }

    }
}
