using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace TongJiBBS.Models
{

    public class private_letter
    {
        private string sender_id;
        private string receiver_id;
        private string content;
        private string time;
        private string id;

        public Hashtable insert_letter(string sender_id, string receiver_id, string content)
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
                        "insert into private_letter values(:letter_id,:sender_id, :receiver_id, " +
                        "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss'), " +
                        ":content, 0)";

                        Random ran= new Random();
                        
                        string l_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
                        OracleParameter lid = new OracleParameter("letter_id", l_id);
                        OracleParameter sid = new OracleParameter("sender_id", sender_id);
                        OracleParameter rid = new OracleParameter("receiver_id", receiver_id);
                        OracleParameter c = new OracleParameter("content", content);
                        cmd.Parameters.Add(lid);
                        cmd.Parameters.Add(sid);
                        cmd.Parameters.Add(rid);
                        cmd.Parameters.Add(c);

                        
                        cmd.ExecuteNonQuery();
                        ht.Add("enter1", "insert1");

                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }
                    
                }
            }
            return ht;
        }

        public Hashtable get_letter(string user_id, string target_id)
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

                        cmd.CommandText = "select letter_id, time_1, content_1 from " +
                            "private_letter where receiver_id=:user_id " +
                            "and sender_id = :target_id and read_flag = 0";

                        cmd.Parameters.Add(new OracleParameter("user_id",user_id));
                        cmd.Parameters.Add(new OracleParameter("target_id", target_id));

                        OracleDataReader reader = cmd.ExecuteReader();
                        string letter_read_id="0";
                        while (reader.Read())
                        {
                            letter_read_id = reader.GetString(0);
                            ht.Add("time", reader.GetDateTime(1).ToString());
                            ht.Add("content", reader.GetString(2));
                        }
                        //标记已读
                        cmd.CommandText = "update private_letter set read_flag=1 where letter_id = :letter_id";

                        cmd.Parameters.Add(new OracleParameter("letter_id", letter_read_id));

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
    }
}
