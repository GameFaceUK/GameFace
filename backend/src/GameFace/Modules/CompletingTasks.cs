using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Modules
{
    public class CompletingTasks
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int idUser { get; set; }
        public int idTask { get; set; }
        public virtual Tasks Tasks { get; set; }
        public virtual Users Users { get; set; }

    }
}
