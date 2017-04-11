using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class XP
    {
        [DataMember]
        public int idUser { get; set; }
        [DataMember]
        public int idTask { get; set; }
        [DataMember]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Users User { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Tasks tasks { get; set; }
        
    }
}
