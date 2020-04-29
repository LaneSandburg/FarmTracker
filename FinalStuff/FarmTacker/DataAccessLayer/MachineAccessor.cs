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
    public class MachineAccessor : IMachineAccessor
    {
        public int ActivateMachine(string machineID)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_reactivate_machine", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MachineID", machineID);

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

        public int DeactivateMachine(string machineID)
        {
            int rows = 0;
            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_machine", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MachineID", machineID);

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

        public int InsertMachine(Machine machine)
        {
            int rows = 0;

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_insert_machine", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MachineID", machine.MachineID);
            cmd.Parameters.AddWithValue("@Make", machine.Make);
            cmd.Parameters.AddWithValue("@Model", machine.Model);
            cmd.Parameters.AddWithValue("@MachineTypeID", machine.MachineTypeID);
            cmd.Parameters.AddWithValue("@MachineStatusID", machine.MachineStatusID);
            cmd.Parameters.AddWithValue("@Hours", machine.Hours);

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

        public List<string> SelectAllMachineStatus()
        {
            List<string> statuss = new List<string>();

            var conn = DBConn.GetConnection();

            var cmd = new SqlCommand("sp_select_all_machine_status", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    statuss.Add(Reader.GetString(0));
                }
                Reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return statuss;
        }

        public List<string> SelectAllMachineTypes()
        {
            List<string> usages = new List<string>();

            var conn = DBConn.GetConnection();

            var cmd = new SqlCommand("sp_select_all_machine_types", conn);
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

        public List<Machine> SelectMachineByActive(bool active = true)
        {
            List<Machine> machines = new List<Machine>();

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_select_machine_by_active");
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
                        var machine = new Machine();
                        machine.MachineID = reader.GetString(0);
                        machine.Make = reader.GetString(1);
                        machine.Model = reader.GetString(2);
                        machine.MachineTypeID = reader.GetString(3);
                        machine.MachineStatusID = reader.GetString(4);
                        machine.Hours = reader.GetInt32(5);
                        machine.Active = reader.GetBoolean(6);
                        
                        machines.Add(machine);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return machines;
        }

        public Machine SelectMachineByID(string id)
        {
            var machine = new Machine();

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_select_machine_by_ID");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MachineID", SqlDbType.NVarChar);
            cmd.Parameters["@MachineID"].Value = id;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {


                        machine.MachineID = reader.GetString(0);
                        machine.Make = reader.GetString(1);
                        machine.Model = reader.GetString(2);
                        machine.MachineTypeID = reader.GetString(3);
                        machine.MachineStatusID = reader.GetString(4);
                        machine.Hours = reader.GetInt32(5);
                        machine.Active = reader.GetBoolean(6);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return machine;
        }

        public int UpdateMachine(Machine oldMachine, Machine newMachine)
        {
            int rows = 0;

            var conn = DBConn.GetConnection();
            var cmd = new SqlCommand("sp_update_machine", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MachineID", oldMachine.MachineID);

            
            cmd.Parameters.AddWithValue("@NewMake", newMachine.Make);
            cmd.Parameters.AddWithValue("@NewModel", newMachine.Model);
            cmd.Parameters.AddWithValue("@NewMachineTypeID", newMachine.MachineTypeID);
            cmd.Parameters.AddWithValue("@NewMachineStatusID", newMachine.MachineStatusID);
            cmd.Parameters.AddWithValue("@NewHours", newMachine.Hours);

            
            cmd.Parameters.AddWithValue("@OldMake", oldMachine.Make);
            cmd.Parameters.AddWithValue("@OldModel", oldMachine.Model);
            cmd.Parameters.AddWithValue("@OldMachineTypeID", oldMachine.MachineTypeID);
            cmd.Parameters.AddWithValue("@OldMachineStatusID", oldMachine.MachineStatusID);
            cmd.Parameters.AddWithValue("@OldHours", oldMachine.Hours);

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
