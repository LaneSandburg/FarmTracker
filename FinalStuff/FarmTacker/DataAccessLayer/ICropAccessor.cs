using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ICropAccessor
    {
        List<Crop> SelectCrops();
        Crop SelectCropByID(string ID);
        int UpdateCrop(Crop oldCrop, Crop newCrop);
        int InsertCrop(Crop crop);
        int DeleteCrop(string CropID);
    }
}
