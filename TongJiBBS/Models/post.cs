﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Oracle.ManagedDataAccess.Client;

namespace TongJiBBS.Models
{
    public class Post
    {
        private string section_id;
        private string user_id;
        
        public Post() { }
        public Post(string _section = "00", string _user_id = "00")
        {
            section_id = _section;
            user_id = _user_id;
        }

        public List<Hashtable> getAllPost_30()
        {
            List<Hashtable> posts = new List<Hashtable>();

            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();

                        string txt1 = "select post_id, user_id, time_1, title, content_1, forward_from_id from post where delete_flag = 0 " +
                            "and user_id not in" +
                            "(select user_relation.object_id from user_relation where user_relation.actort_id = '" + user_id + "' and user_relation.relation_type = 2) " +
                            "and post.section_id = '" + section_id + "'";

                        string txt2 = "select post_id, user_id, time_1, title, content_1, forward_from_id from post where delete_flag = 0 " +
                            "and post.section_id = '" + section_id + "'";

                        string txt3 = "select post_id, user_id, time_1, title, content_1, forward_from_id from post where delete_flag = 0 " +
                            "and user_id not in" +
                            "(select user_relation.object_id from user_relation where user_relation.actort_id = '" + user_id + "' and user_relation.relation_type = 2) ";

                        string txt4 = "select post_id, user_id, time_1, title, content_1, forward_from_id from post where delete_flag = 0 ";

                        if (user_id == "00" && section_id == "00")
                        {
                            cmd.CommandText = txt4;
                        }
                        else if (user_id == "00" && section_id != "00") {
                            cmd.CommandText = txt2;
                        }
                        else if (user_id != "00" && section_id == "00") {
                            cmd.CommandText = txt3;
                        }
                        else
                        {
                            cmd.CommandText = txt1;
                        }

                        OracleDataReader reader = cmd.ExecuteReader();


                        while (reader.Read() && posts.Count <= 30)
                        {
                            posts.Add(new Hashtable());
                            posts[posts.Count-1].Add("post_id", reader.GetString(0));
                            posts[posts.Count-1].Add("user_id", reader.GetString(1));//who post
                            posts[posts.Count-1].Add("time", reader.GetDateTime(2));//.ToString("YYYY-MM-DD HH24:MI:SS"));
                            posts[posts.Count-1].Add("title", reader.GetString(3));
                            posts[posts.Count-1].Add("content", reader.GetString(4));
                            posts[posts.Count-1].Add("num_of_like", 0);
                            posts[posts.Count-1].Add("num_of_dislike", 0);
                            posts[posts.Count-1].Add("num_of_favor", 0);
                            //posts[posts.Count-1].Add("num_of_comment", 0);
                        }

                        foreach (Hashtable i in posts)
                        {
                            string txt5 = "select attitude_type, count(*) from post_attitude where post_id = '" + i["post_id"] + "' group by attitude_type";
                            cmd.CommandText = txt5;
                            OracleDataReader reader5 = cmd.ExecuteReader();
                            while (reader5.Read())
                            {
                                switch (reader5.GetInt32(0)) {
                                    case 1:
                                        i["num_of_like"] = reader5.GetInt32(1);
                                        break;
                                    case 2:
                                        i["num_of_dislike"] = reader5.GetInt32(1);
                                        break;
                                    case 3:
                                        i["num_of_favor"] = reader5.GetInt32(1);
                                        break;

                                }

                            }
                            string txt6 = "select count(*) from post_comment where original_id = '" + i["post_id"] + "' and delete_flag = 0";
                            Console.Write("txt6youwentu");

                            cmd.CommandText = txt6;
                            OracleDataReader reader6 = cmd.ExecuteReader();
                            Console.Write("txt7youwentu");
                            i.Add("num_of_comment", reader6.GetInt32(0));
                            //i["num_of_comment"] = reader6.GetInt32(0);

                            Console.Write("txt7youwentu");
                            //string txt7 = "select * from picture where post_id = '" + i["post_id"] + "'";
                            string txt7 = "select * from picture where post_id = '" + "p1" + "'";
                            cmd.CommandText = txt7;
                            OracleDataReader reader7 = cmd.ExecuteReader();
                            //List<string> pics = new List<string>();
                            if (reader7.Read())
                            {
                                i.Add("picture",reader7.GetString(1));
                            }
                            //i.Add("picture", pics);
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        //await context.Response.WriteAsync(ex.Message);
                        Hashtable hs = new Hashtable();
                        hs.Add("error", ex.Message);
                        posts.Add(hs);
                    }

                }
            }

            return posts;
        }
    }
}