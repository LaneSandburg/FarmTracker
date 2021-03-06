﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class FieldManager : IFieldManager
    {
        private IFieldAccessor _fieldAccessor;

        public FieldManager()
        {
            _fieldAccessor = new FieldAccessor();
        }
        public FieldManager(IFieldAccessor fieldAccessor)
        {
            _fieldAccessor = fieldAccessor;
        }


        public bool AddField(Field field)
        {
            bool result = false;
            try
            {
                result = (_fieldAccessor.InsertField(field) > 0);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("insert failed. ", ex);
            }
            return result;
        }

        public List<string> RetreiveAllFields()
        {
            List<string> fields = null;

            try
            {
                fields = _fieldAccessor.SelectAllFields();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Fields not found", ex);
            }

            return fields;
        }

        public Field RetrieveFieldByID(string farmFieldID)
        {
            Field field = null;

            try
            {
                field = _fieldAccessor.SelectFieldByFarmFieldID(farmFieldID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Fields not found", ex);
            }

            return field;
        }

        public bool UpdateField(string id, Field newField)
        {
            bool result = false;
            try
            {
                var oldField = _fieldAccessor.SelectFieldByFarmFieldID(id);
                result = (1 == _fieldAccessor.UpdateField(oldField, newField));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed", ex);
            }
            return result;
        }
    }
}
