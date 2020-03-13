using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IAssignmentAccessor
    {
        List<Assignment> SelectAssignmentByCompleted(bool completed = false);
        List<string> SelectAllUsageTypes();
        int UpdateAssignment(Assignment oldAssignment, Assignment newAssignment);
        int InsertAssignment(Assignment assignment);
        int ReOpenAssignment(int machineFieldUseID);
        int CompleteAssignment(int machineFieldUseID);
    }
}
