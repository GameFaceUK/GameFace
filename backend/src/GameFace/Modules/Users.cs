using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Modules
{
    public class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surName { get; set; }
        public string nickName { get; set; }
        public bool active { get; set; }
        public virtual List<CompletingTasks> taskComplition { get; set; }
    }
}
