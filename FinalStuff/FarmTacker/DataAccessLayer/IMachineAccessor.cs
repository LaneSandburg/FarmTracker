using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IMachineAccessor
    {
        List<Machine> SelectMachineByActive(bool active = true);
        List<string> SelectAllMachineTypes();
        List<string> SelectAllMachineStatus();
        int UpdateMachine(Machine oldMachine, Machine newMachine);
        int InsertMachine(Machine machine);
        int DeactivateMachine(string machineID);
        int ActivateMachine(string machineID);
    }
}
