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
    public class CropAccessor : ICropAccessor
    {
        public int DeleteCrop(string CropID)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_delete_crop", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CropID", CropID);

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

        public int InsertCrop(Crop crop)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_insert_crop", conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CropID", crop.CropID);
            cmd.Parameters.AddWithValue("@SeedNumber", crop.SeedNum);
            cmd.Parameters.AddWithValue("@Description", crop.Description);
            cmd.Parameters.AddWithValue("@PricePerBag", crop.PricePerBag);

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

        public List<Crop> SelectCrops()
        {
            List<Crop> crops = new List<Crop>();

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_select_crops");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var crop = new Crop();
                        crop.CropID = reader.GetString(0);
                        crop.SeedNum = reader.GetString(1);
                        crop.Description = reader.GetString(2);
                        crop.PricePerBag = reader.GetDecimal(3);
                        

                        crops.Add(crop);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return crops;
        }

        public int UpdateCrop(Crop oldCrop, Crop newCrop)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_update_crop", conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CropID", oldCrop.CropID);

            cmd.Parameters.AddWithValue("@NewSeedNumber", newCrop.SeedNum);
            cmd.Parameters.AddWithValue("@NewDescription", newCrop.Description);
            cmd.Parameters.AddWithValue("@NewPricePerBag", newCrop.PricePerBag);

            cmd.Parameters.AddWithValue("@OldSeedNumber", oldCrop.SeedNum);
            cmd.Parameters.AddWithValue("@OldDescription", oldCrop.Description);
            cmd.Parameters.AddWithValue("@OldPricePerBag", oldCrop.PricePerBag);

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
