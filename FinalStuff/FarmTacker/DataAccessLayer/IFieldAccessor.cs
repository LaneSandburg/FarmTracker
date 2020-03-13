using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IFieldAccessor
    {
        Field SelectFieldByFarmFieldID(string farmFieldID);
        List<string> SelectAllFields();
        int UpdateField(Field oldField, Field newField);
        int InsertField(Field field);
    }
}
