using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IFarmManager
    {
        List<Farm> GetFarmListByActive(bool active = true);
        Farm GetFarmByFarmID(string farmID);
        bool AddFarm(Farm farm);
        bool EditFarm(Farm oldFarm, Farm newFarm);
        bool SetFarmActiveState(bool active, string farmID);
        List<Field> RetreiveFarmFields(string farmID);
    }
}
