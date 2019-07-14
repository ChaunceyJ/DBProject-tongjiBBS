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
        public string post_id;
        public string section_id;
        public string time_1;
        public string title;
        public int delete_flag;
        public string content_1;
        public string forward_from_id;
        public string credit1;
        public string author;
        string actor;
        string content;
        string POST;
        string author_name;
        string actorname;
        string actorhead;
        string head;
        string picture;
        int num1;
        int num2;
        int num3;
        int num4;
        int num5;
        int num6;
        int comment_number;
        int num7;
        int num8;
        int num9;
        string actor_id;
        int attitude;
        string name1;
        string name2;
        string name3;
        public Hashtable Post(string user_id, string section_id, string title, string content)
        {
            int credit;
            Hashtable ht = new Hashtable();
            //section_id = section_id1;
            //title = title1;
            //content_1 = content;
            Random ran = new Random();
            post_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();

            delete_flag = 0;
            forward_from_id = "";
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "INSERT INTO POST (post_id, user_id, section_id, time_1, title, delete_flag, content_1)" +
                            "values(:a1, :a2, :a3, " +
                            "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss')," +
                            ":a5, :a6, :a7)";
                        cmd.Parameters.Add(new OracleParameter("a1", post_id));
                        cmd.Parameters.Add(new OracleParameter("a2", user_id));
                        cmd.Parameters.Add(new OracleParameter("a3", section_id));
                        cmd.Parameters.Add(new OracleParameter("a5", title));
                        cmd.Parameters.Add(new OracleParameter("a6", delete_flag));
                        cmd.Parameters.Add(new OracleParameter("a7", content));
                        // cmd.Parameters.Add(new OracleParameter("a8", forward_from_id));
                        cmd.ExecuteNonQuery();
                        ht.Add("result", "success");
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST WHERE user_id= :user_id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter(":user_id", user_id));
                        OracleDataReader reader8 = cmd.ExecuteReader();
                        while (reader8.Read())
                        {
                            num8 = reader8.GetInt32(0);
                        }
                        credit = 20 + 5 * num8;
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
                        string sign = "";
                        while (reader2.Read())
                        {
                            sign = reader2.GetString(0);
                        }
                        if (sign == user_id)
                        {
                            cmd.CommandText = "Update post SET delete_flag = 1 where post_id  = :post_id";
                            cmd.Parameters.Add(new OracleParameter(":post_id", post_id));
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
                            section_id = reader2.GetString(0);
                            title = reader2.GetString(1);
                            content_1 = reader2.GetString(2);
                        }
                        Random ran = new Random();
                        post_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
                        cmd.CommandText = "INSERT INTO POST " +
                      "values(:a1, :a2, :a3, " +
                      "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss')," +
                      ":a5, :a6, :a7, :a8)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("a1", post_id));
                        cmd.Parameters.Add(new OracleParameter("a2", user_id));
                        cmd.Parameters.Add(new OracleParameter("a3", section_id));
                        cmd.Parameters.Add(new OracleParameter("a5", title));
                        cmd.Parameters.Add(new OracleParameter("a6", delete_flag));
                        cmd.Parameters.Add(new OracleParameter("a7", content_1));
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
            string user_id = "";
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
                        POST = post_id;
                        ht.Add("post_id", POST);
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST_ATTITUDE WHERE post_id= :post_id and attitude_type = 1";
                        //cmd.Parameters.Add(new OracleParameter(":post_id", post_id));
                        OracleDataReader reader3 = cmd.ExecuteReader();
                        while (reader3.Read())
                        {
                            num1 = reader3.GetInt32(0);
                        }
                        ht.Add("Bellitle", num1);
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST_ATTITUDE WHERE post_id= :post_id and attitude_type = 2";
                        //cmd.Parameters.Add(new OracleParameter(":post_id", post_id));
                        OracleDataReader reader4 = cmd.ExecuteReader();
                        while (reader4.Read())
                        {
                            num2 = reader4.GetInt32(0);
                        }
                        ht.Add("Praise", num2);
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST_ATTITUDE WHERE post_id= :post_id and attitude_type = 3";
                        //cmd.Parameters.Add(new OracleParameter(":post_id", post_id));
                        OracleDataReader reader5 = cmd.ExecuteReader();
                        while (reader5.Read())
                        {
                            num3 = reader5.GetInt32(0);
                        }
                        ht.Add("Collect", num3);
                        //作者发的帖子数
                        cmd.CommandText = " SELECT user_id   FROM POST WHERE post_id= :post_id ";
                        OracleDataReader reader6 = cmd.ExecuteReader();
                        while (reader6.Read())
                        {
                            author = reader6.GetString(0);
                        }
                        cmd.CommandText = " SELECT user_name   FROM user_1 WHERE user_id= :author ";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("author", author));
                        OracleDataReader reader13 = cmd.ExecuteReader();
                        while (reader13.Read())
                        {
                            author_name = reader13.GetString(0);
                        }
                        ht.Add("author_name", author_name);
                        cmd.CommandText = " SELECT  COUNT(*) FROM POST WHERE user_id = :author";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("author", author));
                        OracleDataReader reader7 = cmd.ExecuteReader();
                        while (reader7.Read())
                        {
                            num4 = reader7.GetInt32(0);
                        }
                        ht.Add("Publish", num4);

                        cmd.CommandText = " SELECT  COUNT(*) FROM POST_comment WHERE original_id = :post_id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                        OracleDataReader reader14 = cmd.ExecuteReader();
                        while (reader14.Read())
                        {
                            comment_number = reader14.GetInt32(0);
                        }
                        ht.Add("comment_number", comment_number);


                        cmd.CommandText = " SELECT  portrait FROM user_1 WHERE user_id = :author";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("author", author));
                        OracleDataReader reader12 = cmd.ExecuteReader();
                        while (reader12.Read())
                        {
                            head = reader12.GetString(0);
                        }

                        ht.Add("Portrait", common.url_portrait(head));




                        cmd.CommandText = " SELECT  COUNT(*) FROM   USER_RELATION WHERE object_id = :author and relation_type = 1";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("author", author));
                        OracleDataReader reader8 = cmd.ExecuteReader();
                        while (reader8.Read())
                        {
                            num5 = reader8.GetInt32(0);
                        }
                        ht.Add("Fans", num5);
                        cmd.CommandText = " SELECT  COUNT(*) FROM   USER_RELATION WHERE actor_id = :author and relation_type = 1";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("author", author));
                        OracleDataReader reader9 = cmd.ExecuteReader();
                        while (reader9.Read())
                        {
                            num6 = reader9.GetInt32(0);
                        }
                        ht.Add("Attention", num6);
                        cmd.CommandText = " SELECT actor_id, content_1 from post_comment where original_id = :post_id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                        OracleDataReader reader10 = cmd.ExecuteReader();
                        List<Hashtable> ls = new List<Hashtable>();
                        while (reader10.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("actor_id", reader10.GetString(0));
                            temp.Add("content", reader10.GetString(1));
                            string actor_id = reader10.GetString(0);
                            cmd.CommandText = "select portrait from user_1 where user_id=:user_id";
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("user_id", actor_id);
                            OracleDataReader reader111 = cmd.ExecuteReader();
                            string filename;
                            while (reader111.Read())
                            {
                                filename = reader111.GetString(0);
                                temp.Add("commenter_portrait", common.url_portrait(filename));
                            }
                            
                            ls.Add(temp);
                        }
                        ht.Add("Comment", ls);
                        //foreach(Hashtable i in ls)
                        //{

                        //    cmd.CommandText = " SELECT  user_name, portrait from user_1 where user_id = :i[actor_id]";
                        //    cmd.Parameters.Clear();
                        //    cmd.Parameters.Add(new OracleParameter("i[actor_id]", i[actor_id]));
                        //    OracleDataReader reader30 = cmd.ExecuteReader();
                        //    while(reader30.Read())
                        //    {
                        //        Hashtable temp1 = new Hashtable();
                        //        temp1.Add("actor_name", reader30.GetString(0));
                        //        temp1.Add("content", reader30.GetString(1));

                        //        ls.Add(temp);
                        //    }




                        //}


                   

                    




                        cmd.CommandText = " SELECT picture_id  FROM picture WHERE post_id = :post_id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                    OracleDataReader reader11 = cmd.ExecuteReader();
                    List<string> picture1 = new List<string>();
                    while (reader11.Read())
                    {
                        picture = reader11.GetString(0);
                        picture1.Add(common.url_post_pic(picture));

                    }
                    ht.Add("Picture", picture1);
                }
                    catch (Exception ex)
                {
                    //await context.Response.WriteAsync(ex.Message);
                    ht.Add("error", ex.Message);
                }
            }
            return ht;
        }
           
            
        }



    }

}
    