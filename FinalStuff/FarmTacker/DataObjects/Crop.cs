using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Crop
    {
        public string CropID { get; set; }
        public string SeedNum { get; set; }
        public string Description { get; set; }
        public decimal PricePerBag { get; set; } 
    }
}
