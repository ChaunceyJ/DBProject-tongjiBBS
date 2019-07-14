using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
using System.Collections.Generic;

namespace TongJiBBS.Models
{
    public class user_realation
    {

        public Hashtable block(string actor_id, string object_id)
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
                        cmd.CommandText = "select count(*) from user_relation where actor_id=:id1 and object_id=:id2 and relation_type=0";

                        cmd.Parameters.Add(new OracleParameter("id1", actor_id));
                        cmd.Parameters.Add(new OracleParameter("id2", object_id));
                        OracleDataReader reader1 = cmd.ExecuteReader();
                        int a = 0;
                        while (reader1.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            a = reader1.GetInt32(0);
                        }
                        //ht.Add("er", "2222");
                        if (a == 0)
                        {
                            string r_id;
                            //ht.Add("er", "1111");
                            cmd.CommandText = "insert into user_relation values (:relation_id,:id_1,:id_2,0)";
                            Random ran = new Random();
                            r_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
                            cmd.Parameters.Add(new OracleParameter("relation_id", r_id));
                            cmd.Parameters.Add(new OracleParameter("id_1", actor_id));
                            cmd.Parameters.Add(new OracleParameter("id_2", object_id));
                            cmd.ExecuteNonQuery();
                            ht.Add("success", "success");
                        }
                        else
                        {
                            //ht.Add("er2", "222");
                            cmd.CommandText = "delete from user_relation where actor_id=:id_1 and object_id=:id_2 and relation_type=0";
                            cmd.Parameters.Add(new OracleParameter("id_1", actor_id));
                            cmd.Parameters.Add(new OracleParameter("id_2", object_id));
                            cmd.ExecuteNonQuery();
                            ht.Add("success", "success");
                        }
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }
                }
            }
            return ht;
        }
        public Hashtable follow(string actor_id, string object_id)
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
                        cmd.CommandText = "select count(*) from user_relation where actor_id=:id1 and object_id=:id2 and relation_type=1";

                        cmd.Parameters.Add(new OracleParameter("id1", actor_id));
                        cmd.Parameters.Add(new OracleParameter("id2", object_id));
                        OracleDataReader reader1 = cmd.ExecuteReader();
                        int a = 0;
                        while (reader1.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            a = reader1.GetInt32(0);
                        }
                        //ht.Add("er", "2222");
                        if (a == 0)
                        {
                            string r_id;
                            //ht.Add("er", "1111");
                            cmd.CommandText = "insert into user_relation values (:relation_id,:id_1,:id_2,1)";
                            Random ran = new Random();
                            r_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
                            cmd.Parameters.Add(new OracleParameter("relation_id", r_id));
                            cmd.Parameters.Add(new OracleParameter("id_1", actor_id));
                            cmd.Parameters.Add(new OracleParameter("id_2", object_id));
                            cmd.ExecuteNonQuery();
                            ht.Add("success", "success");
                        }
                        else
                        {
                            //ht.Add("er2", "222");
                            cmd.CommandText = "delete from user_relation where actor_id=:id_1 and object_id=:id_2 and relation_type=1";
                            cmd.Parameters.Add(new OracleParameter("id_1", actor_id));
                            cmd.Parameters.Add(new OracleParameter("id_2", object_id));
                            cmd.ExecuteNonQuery();
                            ht.Add("success", "success");
                        }
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }
                }
            }

            return ht;
        }
        public List<Hashtable> show_user_relation_list(string relation_type, string user_id, string target_id)
        {
            List<Hashtable> lists = new List<Hashtable>();
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {

                        con.Open();
                        cmd.BindByName = true;
                        if (user_id == target_id)
                        {
                            int relation_t = Int16.Parse(relation_type);
                            if (relation_t == 3)
                            {
                                cmd.CommandText = "select USER_ID,USER_NAME, CREDIT, portrait, SCHOOL, IDENTITY_1, counts from(select A.actor_id as SS, (select count(*) from user_relation B where B.actor_id =:id_1 and B.object_id = A.actor_id and B.relation_type = 1) as counts from user_relation A where A.object_id =:id_1 and A.relation_type = 1) join user_1 on SS = user_1.user_id";
                                cmd.Parameters.Add(new OracleParameter("id_1", target_id));
                                cmd.Parameters.Add(new OracleParameter("type", 1));
                                OracleDataReader reader1 = cmd.ExecuteReader();
                                while (reader1.Read())
                                {
                                    //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                                    //User me = new User();
                                    //me.user_id = id;
                                    //Hashtable h = me.get_info(reader.GetString(0));///////////////////////////////////////////////////////////////////
                                    Hashtable ht = new Hashtable();
                                    ht.Add("user_id", reader1.GetString(0));
                                    ht.Add("name", reader1.GetString(1));
                                    ht.Add("credit", reader1.GetInt16(2));
                                    ht.Add("portrait", common.url_portrait(reader1.GetString(3)));
                                    ht.Add("school", reader1.GetString(4));
                                    ht.Add("identity", reader1.GetString(5));
                                    if (reader1.GetInt16(6) == 0)
                                    {
                                        ht.Add("is_following", 0);
                                    }
                                    else
                                    {
                                        ht.Add("is_following", 1);
                                    }
                                    lists.Add(ht);

                                }
                            }
                            else
                            {
                                cmd.CommandText = "select USER_ID,USER_NAME, CREDIT, portrait, SCHOOL, IDENTITY_1, counts from(select A.actor_id as SS, (select count(*) from user_relation B where B.object_id =:id_1 and B.actor_id = A.actor_id and B.relation_type = 1) as counts from user_relation A where A.object_id =:id_1 and A.relation_type = 1) join user_1 on SS = user_1.user_id";
                                cmd.Parameters.Add(new OracleParameter("id_1", target_id));
                                cmd.Parameters.Add(new OracleParameter("type", 1));

                                OracleDataReader reader2 = cmd.ExecuteReader();
                                while (reader2.Read())
                                {
                                    //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                                    //User me = new User();
                                    //me.user_id = id;
                                    //Hashtable h = me.get_info(reader.GetString(0));///////////////////////////////////////////////////////////////////
                                    Hashtable ht = new Hashtable();
                                    ht.Add("user_id", reader2.GetString(0));
                                    ht.Add("name", reader2.GetString(1));
                                    ht.Add("credit", reader2.GetInt16(2));
                                    ht.Add("portrait", common.url_portrait(reader2.GetString(3)));
                                    ht.Add("school", reader2.GetString(4));
                                    ht.Add("identity", reader2.GetString(5));
                                    if (reader2.GetInt16(6) == 0)
                                    {
                                        ht.Add("is_following", 0);
                                    }
                                    else
                                    {
                                        ht.Add("is_following", 1);
                                    }
                                    lists.Add(ht);
                                }
                            }
                        }

                        else
                        {
                            int relation_t2 = Int16.Parse(relation_type);
                            if (relation_t2 == 3)
                            {
                                cmd.CommandText = "select USER_ID,USER_NAME, CREDIT, portrait, SCHOOL, IDENTITY_1, counts from(select A.actor_id as SS, (select count(*) from user_relation B where B.object_id =A.actor_id and B.actor_id = :id_1 and B.relation_type = 1) as counts from user_relation A where A.object_id =:id_2 and A.relation_type = 1) join user_1 on SS = user_1.user_id";
                                cmd.Parameters.Add(new OracleParameter("id_2", target_id));
                                cmd.Parameters.Add(new OracleParameter("id_1", user_id));
                                cmd.Parameters.Add(new OracleParameter("type", 1));
                                OracleDataReader reader1 = cmd.ExecuteReader();
                                while (reader1.Read())
                                {
                                    //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                                    //User me = new User();
                                    //me.user_id = id;
                                    //Hashtable h = me.get_info(reader.GetString(0));///////////////////////////////////////////////////////////////////
                                    Hashtable ht = new Hashtable();
                                    ht.Add("user_id", reader1.GetString(0));
                                    ht.Add("name", reader1.GetString(1));
                                    ht.Add("credit", reader1.GetInt16(2));
                                    ht.Add("portrait", common.url_portrait(reader1.GetString(3)));
                                    ht.Add("school", reader1.GetString(4));
                                    ht.Add("identity", reader1.GetString(5));
                                    if (reader1.GetInt16(6) == 0)
                                    {
                                        ht.Add("is_following", 0);
                                    }
                                    else
                                    {
                                        ht.Add("is_following", 1);
                                    }
                                    lists.Add(ht);
                                }
                            }
                            else
                            {
                                cmd.CommandText = "select USER_ID,USER_NAME, CREDIT, portrait, SCHOOL, IDENTITY_1, counts from(select A.object_id as SS, (select count(*) from user_relation B where B.object_id =A.object_id and B.actor_id = :id_1 and B.relation_type = 1) as counts from user_relation A where A.actor_id =:id_2 and A.relation_type = 1) join user_1 on SS = user_1.user_id";
                                cmd.Parameters.Add(new OracleParameter("id_2", target_id));
                                cmd.Parameters.Add(new OracleParameter("id_1", user_id));
                                cmd.Parameters.Add(new OracleParameter("type", 1));

                                OracleDataReader reader2 = cmd.ExecuteReader();
                                while (reader2.Read())
                                {
                                    //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                                    //User me = new User();
                                    //me.user_id = id;
                                    //Hashtable h = me.get_info(reader.GetString(0));///////////////////////////////////////////////////////////////////
                                    Hashtable ht = new Hashtable();
                                    ht.Add("user_id", reader2.GetString(0));
                                    ht.Add("name", reader2.GetString(1));
                                    ht.Add("credit", reader2.GetInt16(2));
                                    ht.Add("portrait", common.url_portrait(reader2.GetString(3)));
                                    ht.Add("school", reader2.GetString(4));
                                    ht.Add("identity", reader2.GetString(5));
                                    if (reader2.GetInt16(6) == 0)
                                    {
                                        ht.Add("is_following", 0);
                                    }
                                    else
                                    {
                                        ht.Add("is_following", 1);
                                    }
                                    lists.Add(ht);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //await context.Response.WriteAsync(ex.Message);
                        Hashtable hd=new Hashtable();
                        hd.Add("error", ex.Message);
                        lists.Add(hd);
                    }
                }
            }
            return lists;
        }
    }
}