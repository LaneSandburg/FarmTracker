using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FieldAccessor : IFieldAccessor
    {
        public int InsertField(Field field)
        {
            int rows = 0;

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_insert_farm_field", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FarmFieldID", field.FarmFieldID);
            cmd.Parameters.AddWithValue("@CropID", field.CropID);
            cmd.Parameters.AddWithValue("@FarmID", field.FarmID);
            cmd.Parameters.AddWithValue("@Acres", field.Acres);            
            cmd.Parameters.AddWithValue("@PastYield", field.PastYield);
            cmd.Parameters.AddWithValue("@CurrentYield", field.CurrentYield);
            cmd.Parameters.AddWithValue("@PlantOnDate", field.PlantOnDate);
            cmd.Parameters.AddWithValue("@HarvestDate", field.HarvestDate);
            cmd.Parameters.AddWithValue("@LastSprayedOn", field.LastSprayedOn);



            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        public List<string> SelectAllFields()
        {
            List<string> fields = new List<string>();

            var conn = DBConn.GetConnection();

            var cmd = new SqlCommand("sp_select_all_fields", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    fields.Add(Reader.GetString(0));
                }
                Reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return fields;
        }

        public Field SelectFieldByFarmFieldID(string farmFieldID)
        {
            Field field = null;

            var conn = DBConn.GetConnection();

            var cmd1 = new SqlCommand("sp_select_field_by_farmfieldID", conn);
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.Add("@FarmFieldID", SqlDbType.NVarChar);

            try
            {
                conn.Open();
                var reader1 = cmd1.ExecuteReader();
                field = new Field();

                field.FarmFieldID = farmFieldID;
                if (reader1.Read())
                {                    
                    field.CropID = reader1.GetString(0);
                    field.FarmID = reader1.GetString(1);
                    field.Acres = reader1.GetInt32(2);
                    field.PastYield = reader1.GetInt32(3);
                    field.CurrentYield = reader1.GetInt32(4);
                    field.PlantOnDate = reader1.GetDateTime(5);
                    field.HarvestDate = reader1.GetDateTime(6);
                    field.LastSprayedOn = reader1.GetDateTime(7);

                    
                }
                else
                {
                    throw new ApplicationException("Field Not Found");
                }

                reader1.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return field;
        }

        public int UpdateField(Field oldField, Field newField)
        {
            int rows = 0;

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_update_farm_field", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FarmFieldID", oldField.FarmFieldID);

            cmd.Parameters.AddWithValue("@NewFarmFieldID", newField.FarmFieldID);
            cmd.Parameters.AddWithValue("@NewCropID", newField.CropID);
            cmd.Parameters.AddWithValue("@NewFarmID", newField.FarmID);
            cmd.Parameters.AddWithValue("@NewAcres", newField.Acres);
            cmd.Parameters.AddWithValue("@NewPastYield", newField.PastYield);
            cmd.Parameters.AddWithValue("@NewCurrentYield", newField.CurrentYield);
            cmd.Parameters.AddWithValue("@NewPlantOnDate", newField.PlantOnDate);
            cmd.Parameters.AddWithValue("@NewHarvestDate", newField.HarvestDate);
            cmd.Parameters.AddWithValue("@NewLastSprayedOn", newField.LastSprayedOn);

            cmd.Parameters.AddWithValue("@OldCropID", oldField.CropID);
            cmd.Parameters.AddWithValue("@OldFarmID", oldField.FarmID);
            cmd.Parameters.AddWithValue("@OldAcres", oldField.Acres);
            cmd.Parameters.AddWithValue("@OldPastYield", oldField.PastYield);
            cmd.Parameters.AddWithValue("@OldCurrentYield", oldField.CurrentYield);
            cmd.Parameters.AddWithValue("@OldPlantOnDate", oldField.PlantOnDate);
            cmd.Parameters.AddWithValue("@OldHarvestDate", oldField.HarvestDate);
            cmd.Parameters.AddWithValue("@OldLastSprayedOn", oldField.LastSprayedOn);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ApplicationException("record not found.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
