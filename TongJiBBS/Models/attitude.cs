using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace TongJiBBS.Models
{
    public class attitude
    {

        string attitude_id;
        string post_id;
        string actor_id;
        int attitude_type;
        int count;
        public Hashtable Attitude(string post_id, string actor_id, int attitude_type)
        {
            Hashtable ht = new Hashtable();
            Random ran = new Random();
            attitude_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();

            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = " SELECT  COUNT(*)  from post_attitude where post_id = :post_id and actor_id = :actor_id and attitude_type = :attitude_type";
                        cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                        cmd.Parameters.Add(new OracleParameter("actor_id",actor_id));
                        cmd.Parameters.Add(new OracleParameter("attitude_type", attitude_type));
                        OracleDataReader reader7 = cmd.ExecuteReader();
                        while (reader7.Read())
                        {
                            count = reader7.GetInt32(0);
                        }
                        ht.Add("count", count);
                        if(count==0)
                        {

                            cmd.CommandText = "INSERT INTO POST_ATTITUDE(attitude_id, post_id, actor_id, attitude_type,time_1) " +
                            "values(:a1, :a2, :a3, :a4, " +
                            "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss'))";
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new OracleParameter("a1", attitude_id));
                            
                            cmd.Parameters.Add(new OracleParameter("a2", post_id));
                            
                            cmd.Parameters.Add(new OracleParameter("a3", actor_id));
                            cmd.Parameters.Add(new OracleParameter("a4", attitude_type));
                            cmd.ExecuteNonQuery();
                            ht.Add("result", "success");
                        }

                        if (count!=0)
                        {
                            cmd.CommandText = "DELETE from post_attitude where post_id = :post_id AND actor_id = :actor_id AND attitude_type = :attitude_type";

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                            cmd.Parameters.Add(new OracleParameter("actor_id", actor_id));
                            cmd.Parameters.Add(new OracleParameter("attitude_type",attitude_type));
                            cmd.ExecuteNonQuery();
                            ht.Add("delete", "success");
                        }

                    }
                    catch (Exception ex)
                    {
                        ht.Add("result", "fail");
                        ht.Add("error2", ex.Message);
                    }
                }
                }
            return ht;
        }
            
        }
    }

