using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace TongJiBBS.Models
{
    public class Report
    {
        private string report_id;
        private string post_id;
        private string actor_id;
        private string reason;

        public Hashtable getReportList(string admin_id)
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

                        //查report
                        cmd.CommandText = "select user_id, section_id, time_1, content_1, reason," +
                            " title, report_time, post_id, actor_id " +
                            " from report natural join post where delete_flag = 0 and section_id in" +
                            "(select section_id from section where admin_id=:admin_id)";
                        cmd.Parameters.Add(new OracleParameter("admin_id", admin_id));
                        OracleDataReader reader1 = cmd.ExecuteReader();
                        ht.Add("enter1", "insert1");
                        List<Hashtable> reports = new List<Hashtable>();
                        while (reader1.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("user_id", reader1.GetString(0));
                            temp.Add("section_id", reader1.GetString(1));
                            temp.Add("time_1", reader1.GetDateTime(2).ToString());
                           
                            temp.Add("content_1", reader1.GetString(3));
                            temp.Add("reason", reader1.GetString(4));
                            temp.Add("title", reader1.GetString(5));
                            temp.Add("report_time", reader1.GetDateTime(6));
                            temp.Add("post_id", reader1.GetString(7));
                            temp.Add("actor_id", reader1.GetString(8));
                            reports.Add(temp);
                        }
                        ht.Add("status", "true");
                        ht.Add("report_list", reports);
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }
                }
            }
            return ht;
        }

        public Hashtable sendReport(string post_id, string actor_id, string reason)
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

                        cmd.CommandText =
                            "insert into report values(:report_id,:post_id,:actor_id,:reason)";
                        Random ran = new Random();
                        string l_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
                        cmd.Parameters.Add(new OracleParameter("report_id", l_id));
                        cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                        cmd.Parameters.Add(new OracleParameter("actor_id", actor_id));
                        cmd.Parameters.Add(new OracleParameter("reason", reason));

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }
                }
            }
            return ht;
        }

        public void AddToNotification(string post_id, string reason)
        {
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = "select count(*) from report where post_id=:post_id";
                    cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) == 5)
                        {

                            cmd.CommandText = "insert into admin_notification values(:notification_id," +
                                ":receiver_id," +
                                " to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss')," +
                                "3,:post_id)";
                            Random ran = new Random();
                            string n_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
                            cmd.Parameters.Add(new OracleParameter("notification_id", n_id));
                            cmd.Parameters.Add(new OracleParameter("receiver_id", getSectionAdmin(post_id)));
                            cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public string getSectionAdmin(string post_id)
        {
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = "select admin_id from post natural join section " +
                        "where post_id=:post_id";
                    cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                    OracleDataReader reader = cmd.ExecuteReader();
                    string admin_id;
                    reader.Read();
                    admin_id = reader.GetString(0);
                    return admin_id;
                }
            }
        }
    }
}

