using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace TongJiBBS.Models
{

    public class Admin
    {

        private string admin_id;
        private string password;
        private string identity;
        private string portrait;
        private string name;

        public Hashtable appointNewAdmin(string admin_id, string selected_id)
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

                        //检查是否存在用户
                        cmd.CommandText = "select count(*) from user_1 where user_id = :selected_id";
                        cmd.Parameters.Add(new OracleParameter("selected_id", selected_id));
                        OracleDataReader reader1 = cmd.ExecuteReader();
                        ht.Add("enter1", "insert1");
                        while (reader1.Read())
                        {
                            if (reader1.GetInt32(0)==0)
                            {
                                ht.Add("status", "0");
                                return ht;
                            }
                        }
                        ht.Add("enter2", "insert2");

                        //获取原用户信息
                        cmd.CommandText = "select user_name, password_1, portrait,user_id from user_1 where user_id = :selected_id";
                        cmd.Parameters.Add(new OracleParameter("selected_id", selected_id));
                        OracleDataReader reader2 = cmd.ExecuteReader();
                        string pre_id;
                        while (reader2.Read())
                        {
                            name = reader2.GetString(0);
                            password = reader2.GetString(1);
                            portrait = reader2.GetString(2);
                            pre_id = reader2.GetString(3);
                            //转移信息 加入admin
                            cmd.CommandText = "insert into admin_1 values(:id, :password,'contentAdmin', :portrait, :name)";
                            cmd.Parameters.Add(new OracleParameter("password", password));
                            cmd.Parameters.Add(new OracleParameter("portrait", portrait));
                            cmd.Parameters.Add(new OracleParameter("name", name));
                            cmd.Parameters.Add(new OracleParameter("id", "ad" + pre_id));
                            cmd.ExecuteNonQuery();
                        }

                        

                        ht.Add("sucess", "dfdsfefe");


                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }

                }
            }
            return ht;
        }

        public Hashtable show_contentAdmin_list()//type为 contentAdmin
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

                        cmd.CommandText = "select admin_id, portrait, name_1, section_id" +
                            " from admin_1 natural join section where identity_1 = 'contentAdmin'";
                        OracleDataReader reader1 = cmd.ExecuteReader();

                        List<Hashtable> ls = new List<Hashtable>();
                        while (reader1.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("admin_id",reader1.GetString(0));
                            temp.Add("portrait", reader1.GetString(1));
                            temp.Add("name", reader1.GetString(0).Remove(0,2));
                            temp.Add("user_id", reader1.GetString(0));//成为管理员前的用户id
                            temp.Add("section_id", reader1.GetString(3));//所管板块
                            ls.Add(temp);
                        }
                        ht.Add("admins", ls);
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
