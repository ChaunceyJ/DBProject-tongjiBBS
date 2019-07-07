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
                        ":content)";

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
    }
}
