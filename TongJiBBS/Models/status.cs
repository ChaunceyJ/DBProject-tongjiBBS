using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
using System.Collections;
namespace TongJiBBS.Models
{
    public class status
    {
        int type;
        public Hashtable Status(string actor_id, string post_id)
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
                        cmd.CommandText = " SELECT attitude_type  FROM post_attitude WHERE post_id = :post_id and actor_id = :user_id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OracleParameter("post_id", post_id));
                        cmd.Parameters.Add(new OracleParameter("user_id", actor_id));
                        OracleDataReader reader11 = cmd.ExecuteReader();
                        List<int> ls1 = new List<int>();
                        while (reader11.Read())
                        {
                            type = reader11.GetInt32(0);
                            ls1.Add(type);
                        }
                        ht.Add("Status", ls1);
                    }
                    catch (Exception ex)
                    {
                        ht.Add("err", ex.Message);
                    }
                }

            }
            return ht;

        }




    }
}
