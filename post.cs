using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace TongJiBBS.Models
{
    public class post
    {
        private string post_id;
        private string user_id;
        private string section_id;
        private string time_1;
        private string title;
        private int delete_flag;
        private string content_1;
        private string forward_from_id;
        public int credit = 0;
        public string credit1;
        public string author;
        int num1;
        int num2;
        int num3;
        int num4;
        int num5;
        int num6;
        string actor_id;
        int attitude;
        string name1;
        string name2;
        string name3;
        public Hashtable Post(string user_id, string section_id, string title, string content_1)
        {
            Hashtable ht = new Hashtable();

            //section_id = section_id1;
            //title = title1;
            //content_1 = content;


            Random ran = new Random();
            post_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();

            delete_flag = 0;
            forward_from_id = "1";
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "INSERT INTO POST " +
                            "values(:a1, :a2, :a3, " +
                            "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss')," +
                            ":a5, :a6, :a7, :a8)";

                        cmd.Parameters.Add(new OracleParameter("a1", post_id));
                        cmd.Parameters.Add(new OracleParameter("a2", user_id));
                        cmd.Parameters.Add(new OracleParameter("a3", section_id));
                        cmd.Parameters.Add(new OracleParameter("a5", title));
                        cmd.Parameters.Add(new OracleParameter("a6", delete_flag));
                        cmd.Parameters.Add(new OracleParameter("a7", content_1));
                        cmd.Parameters.Add(new OracleParameter("a8", forward_from_id));
                 
                        cmd.ExecuteNonQuery();
                        ht.Add("result", "success");
                        credit = credit + 5;
                        credit1 = credit.ToString();
                        ht.Add("credit", credit1);
                    }
                    catch (Exception ex)
                    {
                        ht.Add("err", ex.Message);
                    }
                }

            }
            return ht;
        }
        public Hashtable Del_post(string user_id, string post_id)
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
                        cmd.CommandText = "select USER_ID from POST where POST_ID = :post_id";
                        OracleParameter id = new OracleParameter("post_id", post_id);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader2 = cmd.ExecuteReader();
                        string sign="";
                        while (reader2.Read())
                        {
                            sign = reader2.GetString(0);
                        }
                        if (  sign == user_id)
                        {
                            cmd.CommandText = "Update post SET delete_flag = 1 where post_id  = :post_id";
                            cmd.Parameters.Add(new OracleParameter(":post_id",post_id));
                            cmd.ExecuteNonQuery();
                            ht.Add("result", "success");
                         }
                        else
                        {
                            ht.Add("result", "fail");
                         
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



        public Hashtable Forward(string user_id, string post_id)
        {
            Hashtable ht = new Hashtable();
            delete_flag = 0;
            forward_from_id = post_id;
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                      
                        cmd.CommandText = " SELECT  section_id, title,content_1 FROM POST WHERE POST_ID = :post_id";
                        cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                        OracleDataReader reader2 = cmd.ExecuteReader();
                        while (reader2.Read())
                        {
                             section_id= reader2.GetString(0);
                             title =  reader2.GetString(1);
                             content_1 = reader2.GetString(2);
                        }
                        Random ran = new Random();
                        post_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
                        cmd.CommandText = "INSERT INTO POST (post_id,user_id,section_id,time_1,title,delete_flag,content_1,forward_from_id)" +
                      "values(:a1, :a2, :a3, " +
                      "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss')," +
                      ":a5, :a6, :a7, :a8)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("a1", post_id ));
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("a2", user_id));
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("a3", section_id));
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("a5", title));
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("a6", 0));
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("a7", content_1));
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("a8", forward_from_id));
                        ht.Add("result0", "success0");
                        cmd.ExecuteNonQuery();
                        ht.Add("result", "success");



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

        public Hashtable All(string post_id)
        {
            Hashtable ht = new Hashtable();
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = " SELECT  user_id, section_id, time_1 , title, content_1 FROM POST WHERE POST_ID = :post_id";
                        cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                        OracleDataReader reader2 = cmd.ExecuteReader();
                        while (reader2.Read())
                        {
                            user_id = reader2.GetString(0);
                            section_id = reader2.GetString(1);
                            time_1 = reader2.GetDateTime(2).ToString();
                            title = reader2.GetString(3);
                            content_1 = reader2.GetString(4);
                        }
                        ht.Add("USER_ID", user_id);
                        ht.Add("SECTION_ID", section_id);
                        ht.Add("TIME_1", time_1);
                        ht.Add("TITLE", title);
                        ht.Add("CONTENT_1", content_1);
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST_ATTITUDE WHERE post_id= :post_id and attitude_type = 1";
                        //cmd.Parameters.Add(new OracleParameter(":post_id", post_id));
                        OracleDataReader reader3 = cmd.ExecuteReader();
                        while (reader3.Read())
                        {
                            num1 = reader3.GetInt32(0);
                        }
                        ht.Add("踩", num1);
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST_ATTITUDE WHERE post_id= :post_id and attitude_type = 2";
                        //cmd.Parameters.Add(new OracleParameter(":post_id", post_id));
                        OracleDataReader reader4 = cmd.ExecuteReader();
                        while (reader4.Read())
                        {
                            num2 = reader4.GetInt32(0);
                        }
                        ht.Add("赞", num2);
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST_ATTITUDE WHERE post_id= :post_id and attitude_type = 5";
                        //cmd.Parameters.Add(new OracleParameter(":post_id", post_id));
                        OracleDataReader reader5 = cmd.ExecuteReader();
                        while (reader5.Read())
                        {
                            num3 = reader5.GetInt32(0);
                        }
                        ht.Add("收藏", num3);
                        //作者发的帖子数
                        cmd.CommandText = " SELECT user_id   FROM POST WHERE post_id= :post_id ";
                        OracleDataReader reader6 = cmd.ExecuteReader();
                        while (reader6.Read())
                        {
                            author = reader6.GetString(0);
                        }
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST WHERE user_id = :author";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("author", author));
                        OracleDataReader reader7 = cmd.ExecuteReader();
                        while (reader7.Read())
                        {
                            num4 = reader7.GetInt32(0);
                        }
                        ht.Add("发的帖子数", num4);
                        cmd.CommandText = " SELECT  COUNT(*) FROM   USER_RELATION WHERE object_id = :author and relation_type = 1";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("author", author));
                        OracleDataReader reader8 = cmd.ExecuteReader();
                        while (reader8.Read())
                        {
                            num5 = reader8.GetInt32(0);
                        }
                        ht.Add("作者的粉丝数", num5);
                        cmd.CommandText = " SELECT  COUNT(*) FROM   USER_RELATION WHERE actort_id = :author and relation_type = 1";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("author", author));
                        OracleDataReader reader9 = cmd.ExecuteReader();
                        while (reader9.Read())
                        {
                            num6 = reader9.GetInt32(0);
                        }
                        ht.Add("作者关注的人数", num6);
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
    