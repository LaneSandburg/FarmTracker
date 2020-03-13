using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class FarmAccessor : IFarmAccessor
    {

        public List<Farm> SelectFarmByActive(bool active = true)
        {
            List<Farm> farms = new List<Farm>();

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_select_farms_by_active");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var farm = new Farm();
                        farm.FarmID = reader.GetString(0);
                        farm.UserID = reader.GetInt32(1);
                        farm.Address = reader.GetString(2);
                        farm.City = reader.GetString(3);
                        farm.State = reader.GetString(4);
                        farm.ZipCode = reader.GetString(5);
                        farm.Active = reader.GetBoolean(6);

                        farms.Add(farm);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return farms;

        }
        public int ActivateFarm(string farmID)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_reactivate_farm", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FarmID", farmID);

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

        public int DeactivateFarm(string farmID)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_farm", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FarmID", farmID);

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

        public int InsertFarm(Farm farm)
        {
            int rows = 0;

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_insert_farm", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FarmID", farm.FarmID);
            cmd.Parameters.AddWithValue("@UserID", farm.UserID);
            cmd.Parameters.AddWithValue("@Address", farm.Address);
            cmd.Parameters.AddWithValue("@City", farm.City);
            cmd.Parameters.AddWithValue("@State", farm.State);
            cmd.Parameters.AddWithValue("@ZipCode", farm.ZipCode);



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

        public int UpdateFarm(Farm oldFarm, Farm newFarm)
        {
            int rows = 0;

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_update_farm", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FarmID", oldFarm.FarmID);

            cmd.Parameters.AddWithValue("@OldUserID", oldFarm.UserID);
            cmd.Parameters.AddWithValue("@OldAddress", oldFarm.Address);
            cmd.Parameters.AddWithValue("@OldCity", oldFarm.City);
            cmd.Parameters.AddWithValue("@OldState", oldFarm.State);
            cmd.Parameters.AddWithValue("@OldZipCode", oldFarm.ZipCode);

            cmd.Parameters.AddWithValue("@NewUserID", newFarm.UserID);
            cmd.Parameters.AddWithValue("@NewAddress", newFarm.Address);
            cmd.Parameters.AddWithValue("@NewCity", newFarm.City);
            cmd.Parameters.AddWithValue("@NewState", newFarm.State);
            cmd.Parameters.AddWithValue("@NewZipCode", newFarm.ZipCode);

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

        public List<Field> SelectFieldsByFarmID(string farmID)
        {
            List<Field> fields = new List<Field>();

            var conn = DBConn.GetConnection();

            var cmd = new SqlCommand("sp_select_fields_by_farmID", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FarmID", SqlDbType.NChar);

            cmd.Parameters["@FarmID"].Value = farmID;

            

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                

                
                while (reader.Read())
                {
                    Field field = new Field();
                    field.FarmID = farmID;

                    field.FarmFieldID = reader.GetString(0);

                    if (!reader.IsDBNull(1)) { field.CropID = reader.GetString(1); }                    
                    
                    field.Acres = reader.GetInt32(2);
                    if (!reader.IsDBNull(3)) { field.PastYield = reader.GetInt32(3); }

                    if (!reader.IsDBNull(4)) { field.CurrentYield = reader.GetInt32(4); }

                    if (!reader.IsDBNull(5)) { field.PlantOnDate = reader.GetDateTime(5); }

                    if (!reader.IsDBNull(6)) { field.HarvestDate = reader.GetDateTime(6); }

                    if (!reader.IsDBNull(7)) { field.LastSprayedOn = reader.GetDateTime(7); }
                    
                    
                    

                    fields.Add(field);

                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return fields;
        }

        public Farm SelectFarmByFarmID(string FarmID)
        {
            Farm theFarm = new Farm();

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_select_farms_by_FarmID");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FarmID", SqlDbType.NVarChar);
            cmd.Parameters["@FarmID"].Value = FarmID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var farm = new Farm();
                        farm.FarmID = reader.GetString(0);
                        farm.UserID = reader.GetInt32(1);
                        farm.Address = reader.GetString(2);
                        farm.City = reader.GetString(3);
                        farm.State = reader.GetString(4);
                        farm.ZipCode = reader.GetString(5);
                        farm.Active = reader.GetBoolean(6);

                        theFarm= farm;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return theFarm;
        }
    }
}
