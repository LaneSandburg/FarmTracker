using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class AssignmentManager : IAssignmentManager
    {

        private IAssignmentAccessor _assignmentAccessor;

        public AssignmentManager()
        {
            _assignmentAccessor = new AssignmentAccessor();
        }

        public AssignmentManager(IAssignmentAccessor assignmentAccessor)
        {
            _assignmentAccessor = assignmentAccessor;
        }

        public bool AddAssignment(Assignment assignment)
        {
            bool result = false;
            try
            {
                result = (_assignmentAccessor.InsertAssignment(assignment) > 0);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("insert failed. ", ex);
            }
            return result;
        }

        public bool EditAssignment(Assignment oldAssignment, Assignment newAssignment)
        {
            bool result = false;
            try
            {
                result = (1 == _assignmentAccessor.UpdateAssignment(oldAssignment, newAssignment));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed", ex);
            }
            return result;
        }

        public List<Assignment> GetAssignmentByCompleted(bool completed = false)
        {


            try
            {

                return _assignmentAccessor.SelectAssignmentByCompleted(completed);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("List Not Available", ex);
            }
        }

        public List<string> RetreiveUsageTypes()
        {
            List<string> usages = null;

            try
            {
                usages = _assignmentAccessor.SelectAllUsageTypes();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Usages not found", ex);
            }

            return usages;
        }

        public bool SetAssignmentCompletionState(bool completed, int machineFieldUseID)
        {
            bool result = false;
            try
            {
                if (completed)
                {
                    result = (1 == _assignmentAccessor.CompleteAssignment(machineFieldUseID));
                }
                else
                {
                    result = (1 == _assignmentAccessor.ReOpenAssignment(machineFieldUseID));
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
