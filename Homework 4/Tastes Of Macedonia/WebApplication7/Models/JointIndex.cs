using System.Collections.Generic;

namespace WebApplication7.Models
{
    public class JointIndex
    {
        public List<favorite> favorites { get; set; }
        public List<mytable> mytables { get; set; }
        public List<Rating> ratings { get; set; }
        public string name { get; set; }
        public string cuisine { get; set; }
        public string opening_hours { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
    }
}