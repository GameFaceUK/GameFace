using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class Tasks
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int desirability { get; set; }
        [DataMember]
        public int frequency { get; set; }
        [DataMember]
        public int efort { get; set; }
        [DataMember]
        public int value => efort *(2/frequency)+desirability;

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<StatisticDatapoint> statisticDatapoint { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<XP> xP { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<Achieve> achievement { get; set; }
    }
}
