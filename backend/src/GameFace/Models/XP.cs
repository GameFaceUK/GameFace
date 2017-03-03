using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Models
{
    public class XP
    {
            public int idUser { get; set; }
            public int idTask { get; set; }
            public int Steps { get; set; }
                
            public int XpObtained => tasks.value * Steps;
        
            public virtual Users User { get; set; }
            public virtual Tasks tasks { get; set; }
        
    }
}
