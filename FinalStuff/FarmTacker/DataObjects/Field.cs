using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Field
    {
        public string FarmFieldID { get; set; }
        public string CropID { get; set; }
        public string FarmID { get; set; }
        public int Acres { get; set; }
        public int PastYield { get; set; }
        public int CurrentYield { get; set; }
        public DateTime PlantOnDate { get; set; }
        public DateTime HarvestDate { get; set; }
        public DateTime LastSprayedOn { get; set; }
        
    }
}
