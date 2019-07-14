using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Oracle.ManagedDataAccess.Client;

namespace TongJiBBS.Models
{
    public class Notification
    {
        private string notification_id;
        private string receiver_id;
        private string time;
        private string notification_type;
        private string post_or_user_id;
        private int read;

        public Notification() { }
        public Notification(string _receiver_id)
        {
            receiver_id = _receiver_id;
        }
        //type == 1 -> attitude; type == 2 -> comment
        public List<Hashtable> getUserNotice(int _notification_type, int _read = 1)
        {
            
            List<Hashtable> notices = new List<Hashtable>();

            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();

                        string txt1 = "select NOTIFICATION_ID, ATTITUDE_COMMENT_ID, POST_ID, ACTOR_ID, ATTITUDE_TYPE, post_attitude.time_1" +
                                      "from user_notification, post_attitude" +
                                      "where post_attitude.attitude_id = user_notification.attitude_comment_id and receiver_id = '" + this.receiver_id +
                                      "' and user_notification.notification_type = 1 order by post_attitude.time_1 desc";//and user_notification.read = " + _read;

                        string txt2 = "select NOTIFICATION_ID, ATTITUDE_COMMENT_ID, original_id, ACTOR_ID, post_comment.time_1" +
                                       "from user_notification, post_comment" +
                                       "where post_comment.comment_id = user_notification.attitude_comment_id and user_notification.receiver_id = '" + this.receiver_id + "' " +
                                       "and user_notification.notification_type = 2  and post_comment.delete_flag = 0 order by post_comment.time_1 desc";//and user_notification.read = " + _read + ;


                        if (_notification_type == 1)
                        {
                            cmd.CommandText = txt1;

                            OracleDataReader reader = cmd.ExecuteReader();

                            while (reader.Read() && notices.Count <= 30)
                            {
                                notices.Add(new Hashtable());
                                notices[notices.Count - 1].Add("notification_id", reader.GetString(0));
                                notices[notices.Count - 1].Add("attitude_comment_id", reader.GetString(1));
                                notices[notices.Count - 1].Add("post_id", reader.GetString(2));
                                notices[notices.Count - 1].Add("actor_id", reader.GetString(3));
                                notices[notices.Count - 1].Add("attitude_id", reader.GetString(4));
                                notices[notices.Count - 1].Add("time", reader.GetDateTime(5).ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        else
                        {
                            cmd.CommandText = txt2;

                            OracleDataReader reader = cmd.ExecuteReader();

                            while (reader.Read() && notices.Count <= 30)
                            {
                                notices.Add(new Hashtable());
                                notices[notices.Count - 1].Add("notification_id", reader.GetString(0));
                                notices[notices.Count - 1].Add("attitude_comment_id", reader.GetString(1));
                                notices[notices.Count - 1].Add("post_id", reader.GetString(2));
                                notices[notices.Count - 1].Add("actor_id", reader.GetString(3));
                                notices[notices.Count - 1].Add("comment", reader.GetString(4));
                                notices[notices.Count - 1].Add("time", reader.GetDateTime(5).ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        foreach (Hashtable i in notices)
                        {

                            string txt7 = "select USER_NAME from USER_1 where USER_ID = '" + i["actor_id"] + "'";

                            cmd.CommandText = txt7;
                            OracleDataReader reader7 = cmd.ExecuteReader();
                            //List<string> pics = new List<string>();
                            if (reader7.Read())
                            {
                                i.Add("actor_name", reader7.GetString(0));
                            }
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        //await context.Response.WriteAsync(ex.Message);
                        Hashtable hs = new Hashtable();
                        hs.Add("error", ex.Message);
                        notices.Add(hs);
                    }

                }
            }

            return notices;
        }

        public List<Hashtable> getAdminNotice()
        {
            List<Hashtable> notices = new List<Hashtable>();

            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();

                        string txt1 = "select * from admin_notification where receiver_id = '" + this.receiver_id + "'order by time_1 desc";//and user_notification.read = " + _read;

                        cmd.CommandText = txt1;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read() && notices.Count <= 30)
                        {
                            notices.Add(new Hashtable());
                            notices[notices.Count - 1].Add("notification_id", reader.GetString(0));
                            notices[notices.Count - 1].Add("time", reader.GetDateTime(2).ToString("yyyy-MM-dd HH:mm:ss"));
                            notices[notices.Count - 1].Add("post_id", reader.GetString(4));
                              
                        }



                        foreach (Hashtable i in notices)
                        {

                            string txt7 = "select USER_ID, POST_ID, TIME_1, TITLE, CONTENT_1, DELETE_FLAG, USER_NAME, SCHOOL, CREDIT "+
                                         "from POST natural join USER_1 where POST_ID = '" + i["post_id"] + "'";

                            cmd.CommandText = txt7;
                            OracleDataReader reader7 = cmd.ExecuteReader();
                            //List<string> pics = new List<string>();
                            if (reader7.Read())
                            {
                                i.Add("user_id", reader7.GetString(0));
                                i.Add("post_id", reader7.GetString(1));
                                i.Add("time", reader7.GetDateTime(2).ToString("yyyy-MM-dd HH:mm:ss"));
                                i.Add("title", reader7.GetString(3));
                                i.Add("content", reader7.GetString(4));
                                i.Add("delete", reader7.GetInt32(5));
                                i.Add("user_name", reader7.GetString(6));
                                i.Add("school", reader7.GetString(7));
                                i.Add("credit", reader7.GetInt32(8));
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        //await context.Response.WriteAsync(ex.Message);
                        Hashtable hs = new Hashtable();
                        hs.Add("error", ex.Message);
                        notices.Add(hs);
                    }

                }
            }

            return notices;
        }


    }

}

