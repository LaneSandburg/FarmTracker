using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class MachineManager : IMachineManager        
    {
        private IMachineAccessor _machineAccessor;

        public MachineManager()
        {
            _machineAccessor = new MachineAccessor();
        }
        public MachineManager(IMachineAccessor machineAccessor)
        {
            _machineAccessor = machineAccessor;
        }

        public bool AddMachine(Machine machine)
        {
            bool result = false;
            try
            {
                result = (_machineAccessor.InsertMachine(machine) > 0);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("insert failed. ", ex);
            }
            return result;
        }

        public bool EditMachine(Machine oldMachine, Machine newMachine)
        {
            bool result = false;
            try
            {
                result = (1 == _machineAccessor.UpdateMachine(oldMachine, newMachine));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed", ex);
            }
            return result;
        }

        public List<Machine> GetMachineListByActive(bool active = true)
        {
            try
            {
                return _machineAccessor.SelectMachineByActive(active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("List not available", ex);
            }
        }

        public Machine GetMachineByID(string id)
        {
            try
            {
                return _machineAccessor.SelectMachineByID(id);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Machine not available", ex);
            }
        }

        public List<string> RetreiveMachineStatus()
        {
            List<string> statuss = null;

            try
            {
                statuss = _machineAccessor.SelectAllMachineStatus();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Roles not found", ex);
            }

            return statuss;
        }

        public List<string> RetreiveMachineTypes()
        {
            List<string> types = null;

            try
            {
                types = _machineAccessor.SelectAllMachineTypes();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Roles not found", ex);
            }

            return types;
        }

        public bool SetMachineActiveState(bool active, string machineID)
        {
            bool result = false;
            try
            {
                if (active)
                {
                    result = (1 == _machineAccessor.ActivateMachine(machineID));
                }
                else
                {
                    result = (1 == _machineAccessor.DeactivateMachine(machineID));
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
