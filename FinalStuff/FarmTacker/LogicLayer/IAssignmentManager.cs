using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IAssignmentManager
    {
        List<Assignment> GetAssignmentByCompleted(bool completed = false);
        List<string> RetreiveUsageTypes();
        bool AddAssignment(Assignment assignment);
        bool EditAssignment(Assignment oldAssignment, Assignment newAssignment);
        bool SetAssignmentCompletionState(bool completed, int machineFieldUseID);
    }
}

