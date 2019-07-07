using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Oracle.ManagedDataAccess.Client;

namespace TongJiBBS.Models
{
    public class User
    {
        private string user_ID;
        private string user_name;
        private int credit;//信誉积分
        private string password;
        private string potrall;
        private string school;
        private string identity;

        public User() { }
        public User(string ID, string name, string _password)
        {
            user_ID = ID;
            user_name = name;
            password = common.md5(_password, ID);
            credit = 20;
            //school = _school;
            //identity = _identity;
        }

        public Hashtable signUp(string verficode)
        {
            Hashtable ht = new Hashtable();
            string send_code = "00";
            //验证验证码是否正确
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
                        cmd.CommandText = "select CODE from VERIFICATION where USER_ID = :id order by TIME desc";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", user_ID);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            send_code = reader.GetString(0);
                        }
                        Console.Write(send_code);
                        //reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        //await context.Response.WriteAsync(ex.Message);
                        ht.Add("error", ex.Message);
                    }

                }

            }

            if (send_code == verficode)
            {
                //插入用户信息
                Console.Write("sucess verfi");
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
                            cmd.CommandText = "INSERT INTO USER_1( USER_ID, USER_NAME, CREDIT, PASSWORD_1) VALUES ( :_1, :_2, :_3, :_4)";

                            // Assign id to the department number 50 

                            Console.Write("sucess iinsert");

                            cmd.Parameters.Add(new OracleParameter(":_1", user_ID));
                            cmd.Parameters.Add(new OracleParameter(":_2", user_name));
                            cmd.Parameters.Add(new OracleParameter(":_3", credit));
                            cmd.Parameters.Add(new OracleParameter(":_4", password));

                            cmd.ExecuteNonQuery();

                            ht.Add("result", "success");
                        }
                        catch (Exception ex)
                        {
                            //await context.Response.WriteAsync(ex.Message);
                            ht.Add("result", "fail");
                            ht.Add("error2", ex.Message);
                        }

                    }

                }
            }
            else
            {
                ht.Add("result", "fail");
                ht.Add("reason", "verficode error");
            }
            return ht;
        }
    }
}
