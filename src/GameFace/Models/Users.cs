using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GameFace.Models
{
    [DataContract(IsReference = true)]
    public class Users
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surName { get; set; }
        [DataMember]
        public string nickName { get; set; }
        [DataMember]
        public bool status { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<UsersRewards> userRewards { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<UsersAchievements> userAchievements { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<StatisticDatapoint> statisticDatapoint { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<XP> xP { get; set; }
    }
}
