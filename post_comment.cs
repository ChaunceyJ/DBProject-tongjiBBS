using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
namespace TongJiBBS.Models
{
        public class post_comment
        {
             string comment_id;
             string content_1;
             string original_id;
             string time_1;
             string actor_id;
             int delete_flag;
             string at_id;
       
             public Hashtable Comment(string content_1, string original_id, string actor_id, string at_id)
          {
            Hashtable ht = new Hashtable();

            //section_id = section_id1;
            //title = title1;
            //content_1 = content;


            Random ran = new Random();
            comment_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();

            delete_flag = 0;
            
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = "INSERT INTO POST_COMMENT " +
                            "values(:a1, :a2, :a3, " +
                            "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss')," +
                            ":a5, :a6, :a7)";

                        cmd.Parameters.Add(new OracleParameter("a1", comment_id));
                        cmd.Parameters.Add(new OracleParameter("a2", content_1));
                        cmd.Parameters.Add(new OracleParameter("a3", original_id));

                        cmd.Parameters.Add(new OracleParameter("a5", actor_id));
                        cmd.Parameters.Add(new OracleParameter("a6", delete_flag));
                        cmd.Parameters.Add(new OracleParameter("a7", at_id));
                      
                   
                        cmd.ExecuteNonQuery();
                        ht.Add("result", "success");
                    }
                    catch (Exception ex)
                    {
                        ht.Add("err", ex.Message);
                    }
                }

            }
            return ht;
          }
            public Hashtable del_comment(string user_id, string comment_id)
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
                        cmd.CommandText = "select AT_ID from POST_COMMENT where COMMENT_ID = :comment_id";
                        OracleParameter id = new OracleParameter("comment_id", comment_id);
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
                            cmd.CommandText = "Update post_comment SET delete_flag = 1 where comment_id  = :comment_id";
                            cmd.Parameters.Add(new OracleParameter(":coment_id", comment_id));
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
   

        }
    
}
