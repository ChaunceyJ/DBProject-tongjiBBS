using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections;


namespace TongJiBBS.Models
{
    public class Demo
    {
        private string user_id = "1752058";
        private string name;
        
        public Hashtable get_name()
        {
            Hashtable ht = new Hashtable();
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select USER_NAME from USER_1 where USER_ID = :id";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", user_id);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("name", reader.GetString(0));
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        //await context.Response.WriteAsync(ex.Message);
                        ht.Add("error", ex.Message);
                    }
                }

            }

            return ht;
        }
    }
}
