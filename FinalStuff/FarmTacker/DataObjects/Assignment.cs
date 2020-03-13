using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Assignment
    {
        public int MachineFieldUseID { get; set; }
        public string FarmFieldID { get; set; }
        public string UsageTypeID { get; set; }
        public string MachineID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
