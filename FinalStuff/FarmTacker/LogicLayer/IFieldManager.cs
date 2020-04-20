using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IFieldManager
    {
        bool AddField(Field field);
        bool UpdateField(string id, Field newField);
        List<string> RetreiveAllFields();
        Field RetrieveFieldByID(string farmFieldID);
    }
}
