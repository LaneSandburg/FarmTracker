using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
   public  class Farm
    {
        public string FarmID { get; set; }
        public int UserID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool Active{ get; set; }
        public List<string> Fields { get; set; }
    }
}
