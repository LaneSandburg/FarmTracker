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
    public class AssignmentAccessor : IAssignmentAccessor
    {
        public int CompleteAssignment(int machineFieldUseID)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_complete_field_use", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MachineFieldUseID", machineFieldUseID);

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

        public int ReOpenAssignment(int machineFieldUseID)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_reopen_field_use", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MachineFieldUseID", machineFieldUseID);

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

        public int InsertAssignment(Assignment assignment)
        {
            int rows = 0;

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_insert_machine_field_use", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FarmFieldID", assignment.FarmFieldID);
            cmd.Parameters.AddWithValue("@UsageTypeID", assignment.UsageTypeID);
            cmd.Parameters.AddWithValue("@MachineID", assignment.MachineID);
            cmd.Parameters.AddWithValue("@UserID", assignment.UserID);
            cmd.Parameters.AddWithValue("@Description", assignment.Description);
            
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

        public List<Assignment> SelectAssignmentByCompleted(bool completed = false)
        {
            List<Assignment> assignments = new List<Assignment>();

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_select_machine_field_use_by_complete");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Completed", SqlDbType.Bit);
            cmd.Parameters["@Completed"].Value = completed;

            try
            {
                conn.Open();
                var Reader = cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        var assignment = new Assignment();
                        assignment.MachineFieldUseID = Reader.GetInt32(0);
                        assignment.FarmFieldID = Reader.GetString(1);
                        assignment.UsageTypeID = Reader.GetString(2);
                        assignment.MachineID = Reader.GetString(3);
                        assignment.UserID = Reader.GetInt32(4);
                        assignment.Description = Reader.GetString(5);
                        assignment.Completed = completed;

                        assignments.Add(assignment);

                    }
                }
                Reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return assignments;
        }

        public int UpdateAssignment(Assignment oldAssignment, Assignment newAssignment)
        {
            int rows = 0;

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_update_machine_field_use", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MachineFieldUseID", oldAssignment.MachineFieldUseID);


            cmd.Parameters.AddWithValue("@NewFarmFieldID", newAssignment.FarmFieldID);
            cmd.Parameters.AddWithValue("@NewUsageTypeID", newAssignment.UsageTypeID);
            cmd.Parameters.AddWithValue("@NewMachineID", newAssignment.MachineID);
            cmd.Parameters.AddWithValue("@NewUserID", newAssignment.UserID);
            cmd.Parameters.AddWithValue("@NewDescription", newAssignment.Description);


            cmd.Parameters.AddWithValue("@OldFarmFieldID", oldAssignment.FarmFieldID);
            cmd.Parameters.AddWithValue("@OldUsageTypeID", oldAssignment.UsageTypeID);
            cmd.Parameters.AddWithValue("@OldMachineID", oldAssignment.MachineID);
            cmd.Parameters.AddWithValue("@OldUserID", oldAssignment.UserID);
            cmd.Parameters.AddWithValue("@OldDescription", oldAssignment.Description);

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

        public List<string> SelectAllUsageTypes()
        {
            List<string> usages = new List<string>();

            var conn = DBConn.GetConnection();

            var cmd = new SqlCommand("sp_select_all_usages", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    usages.Add(Reader.GetString(0));
                }
                Reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return usages;
        }

        public Assignment SelectAssignmentByID(int id)
        {
            var assignment = new Assignment();

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_select_machine_field_use_by_id");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MachineFieldUseID", id);

            try
            {
                conn.Open();
                var Reader = cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        
                        assignment.MachineFieldUseID = Reader.GetInt32(0);
                        assignment.FarmFieldID = Reader.GetString(1);
                        assignment.UsageTypeID = Reader.GetString(2);
                        assignment.MachineID = Reader.GetString(3);
                        assignment.UserID = Reader.GetInt32(4);
                        assignment.Description = Reader.GetString(5);
                        assignment.Completed = Reader.GetBoolean(6);                       

                    }
                }
                Reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return assignment;
        }
    }
}
