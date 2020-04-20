using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class FarmManager : IFarmManager
    {
        private IFarmAccessor _farmAccessor;

        public FarmManager() 
        {
            _farmAccessor = new FarmAccessor();
        }
        public FarmManager(IFarmAccessor farmAccessor)
        {
            _farmAccessor = farmAccessor;
        }

        public bool AddFarm(Farm farm)
        {
            bool result = false;
            try
            {
                result = (_farmAccessor.InsertFarm(farm) > 0);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("insert failed. ", ex);
            }
            return result;
        }

        public bool EditFarm(string id, Farm newFarm)
        {
            bool result = false;
            try
            {
                Farm oldFarm = _farmAccessor.SelectFarmByFarmID(id);
                result = (1 == _farmAccessor.UpdateFarm(oldFarm, newFarm));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed", ex);
            }
            return result;
        }

        public Farm GetFarmByFarmID(string farmID)
        {
            try
            {
                return _farmAccessor.SelectFarmByFarmID(farmID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("List not available", ex);
            }
        }

        public List<Farm> GetFarmListByActive(bool active = true)
        {
            try
            {
                return _farmAccessor.SelectFarmByActive(active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("List not available",ex);
            }
        }

        public List<Field> RetreiveFarmFields(string farmID)
        {
            List<Field> fields = null;

            try
            {
                fields = _farmAccessor.SelectFieldsByFarmID(farmID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("fields not found", ex);
            }

            return fields;
        }

        public bool SetFarmActiveState(bool active, string farmID)
        {
            bool result = false;
            try
            {
                if (active)
                {
                    result = (1 == _farmAccessor.ActivateFarm(farmID));
                }
                else
                {
                    result = (1 == _farmAccessor.DeactivateFarm(farmID));
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed.", ex);
            }


            return result;
        }
    }
    }

