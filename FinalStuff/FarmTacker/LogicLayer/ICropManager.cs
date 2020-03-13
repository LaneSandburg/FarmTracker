using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ICropManager
    {
        List<Crop> GetCropList();
        bool AddCrop(Crop crop);
        bool EditCrop(Crop oldCrop, Crop newCrop);
        bool DeleteCrop(string cropID);
    }
}
