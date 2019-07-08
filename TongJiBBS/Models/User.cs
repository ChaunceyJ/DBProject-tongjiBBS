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

        public User(string ID, string _password)
        {
            user_ID = ID;
            password = common.md5(_password, ID);

        }

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
                            cmd.CommandText = "INSERT INTO USER_1( USER_ID, USER_NAME, CREDIT, PASSWORD_1) VALUES( :ID, :name, :credit, :password)";

                            // Assign id to the department number 50 


                            cmd.Parameters.Add(new OracleParameter(":ID", this.user_ID));
                            cmd.Parameters.Add(new OracleParameter(":name", this.user_name));
                            cmd.Parameters.Add(new OracleParameter(":credit", this.credit));
                            cmd.Parameters.Add(new OracleParameter(":password", this.password));

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

        public Hashtable login()
        {
            Hashtable ht = new Hashtable();
            //验证密码是否正确
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
                        cmd.CommandText = "select PASSWORD_1 from USER_1 where USER_ID = :id";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", user_ID);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            Console.Write('\n'+reader.GetString(0)+'\n');
                            if(reader.GetString(0) != this.password)
                            {
                                ht.Add("result", "fail");
                                ht.Add("reason", "password error");
                            }
                            else
                            {
                                //这里有问题
                                cmd.CommandText = "select freeze_start_time, freeze_en_time from freeze where USER_ID = :ID and freeze_en_time > sysdate";
                                OracleDataReader reader2 = cmd.ExecuteReader();
                                if (reader2.Read())
                                {
                                    Console.Write('\n' + reader2.GetDateTime(0).ToString("dd/MM/yy") + '\n');
                                    ht.Add("result", "fail");
                                    ht.Add("reason", "freeze account");
                                    ht.Add("start_time", reader2.GetDateTime(0).ToString("dd/MM/yy"));
                                    ht.Add("end_time", reader2.GetDateTime(1).ToString("dd/MM/yy"));
                                }
                                else
                                {
                                    ht.Add("result", "success");
                                    ht.Add("ID", this.user_ID);
                                }
                            }
                        }
                        else
                        {
                            ht.Add("result", "fail");
                            ht.Add("reason", "ID not exist");
                        }
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

        public Hashtable modifyByAdmin(string user_id, string school)
        {
            Hashtable ht = new Hashtable();

            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        ht.Add("enter0", "insert0");
                        cmd.BindByName = true;

                        cmd.CommandText =
                            "update user_1 set school = :school where user_id = :user_id";

                        cmd.Parameters.Add(new OracleParameter("user_id", user_id));
                        cmd.Parameters.Add(new OracleParameter("school", school));

                        cmd.ExecuteNonQuery();
                        ht.Add("asd", "dfdf");
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }

                }
            }
            return ht;

        }
    }
}
