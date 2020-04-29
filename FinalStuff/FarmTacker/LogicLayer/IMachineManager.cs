using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IMachineManager
    {
        List<Machine> GetMachineListByActive(bool active = true);
        Machine GetMachineByID(string id);
        bool AddMachine(Machine machine);
        bool EditMachine(Machine oldMachine, Machine newMachine);
        bool SetMachineActiveState(bool active, string machineID);
        List<string> RetreiveMachineTypes();
        List<string> RetreiveMachineStatus();
    }
}
