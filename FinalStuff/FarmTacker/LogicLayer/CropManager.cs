using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class CropManager : ICropManager
    {
        private ICropAccessor _cropAccessor;

        public CropManager()
        {
            _cropAccessor = new CropAccessor();
        }

        public CropManager(ICropAccessor cropAccessor)
        {
            _cropAccessor = cropAccessor;
        }

        public bool AddCrop(Crop crop)
        {
            bool result = false;
            try
            {
                result = (_cropAccessor.InsertCrop(crop) > 0);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("insert failed. ", ex);
            }
            return result;
        }

        public bool DeleteCrop(string cropID)
        {
            bool result = false;
            try
            {                
                    result = (1 == _cropAccessor.DeleteCrop(cropID));        

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed.", ex);
            }


            return result;
        }

        public bool EditCrop(Crop oldCrop, Crop newCrop)
        {
            bool result = false;
            try
            {
                result = (1 == _cropAccessor.UpdateCrop(oldCrop, newCrop));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("insert failed. ", ex);
            }
            return result;
        }

        public Crop GetCropByID(string ID)
        {
            try
            {

                return _cropAccessor.SelectCropByID(ID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Crop Not Available", ex);
            }
        }

        public List<Crop> GetCropList()
        {
            try
            {

                return _cropAccessor.SelectCrops();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("List Not Available", ex);
            }
        }
    }
}
