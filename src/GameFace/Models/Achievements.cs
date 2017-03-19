using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class Achieve
    {
        [DataMember]
        public int idAchieve { get; set; }
        [DataMember]
        public int idTask { get; set; }
        [DataMember]
        public int levelNeeded { get; set; }
        [DataMember]
        public string description { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember]
        public virtual Tasks tasks { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<UsersAchievements> usersAchievement { get; set; }
       
     }
}
