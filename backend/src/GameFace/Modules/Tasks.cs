using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Modules
{
    public class Tasks
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int value { get; set; }
        public virtual List<CompletingTasks> taskComplition { get; set; }
    }
}
