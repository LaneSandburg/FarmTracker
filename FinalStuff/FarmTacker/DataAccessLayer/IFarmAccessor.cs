using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IFarmAccessor
    {
        List<Farm> SelectFarmByActive(bool active = true);
        Farm SelectFarmByFarmID(string FarmID);
        int UpdateFarm(Farm oldFarm, Farm newFarm);
        int InsertFarm(Farm farm);
        int DeactivateFarm(string farmID);
        int ActivateFarm(string farmID);
        List<Field> SelectFieldsByFarmID(string farmID);

    }
}
