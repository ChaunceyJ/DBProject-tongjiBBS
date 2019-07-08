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
        /*
        public ArrayList getUserNotice(int _read = 1)
        {
            ArrayList notices = new ArrayList();
            Hashtable detail = new Hashtable();
            detail.Add("notification_id", "");
            detail.Add("time", "");
            detail.Add("notification_type", "");
            detail.Add("attitude_comment_ID", "");
            detail.Add("detail", "");

            if(read == 1)
            {
                //get all notification
                using (OracleConnection con = new OracleConnection(common.conString))
                {
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        try
                        {
                            con.Open();

                            //Use the command to display employee names from 
                            // the EMPLOYEES table
                            cmd.CommandText = "select CODE from VERIFICATION where USER_ID = :id order by TIME desc";

                            // Assign id to the department number 50 
                            OracleParameter id = new OracleParameter("id", user_ID);
                            cmd.Parameters.Add(id);

                            //Execute the command and use DataReader to display the data
                            OracleDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                send_code = reader.GetString(0);
                            }
                            Console.Write(send_code);
                            //reader.Dispose();
                        }
                        catch (Exception ex)
                        {
                            //await context.Response.WriteAsync(ex.Message);
                            ht.Add("error", ex.Message);
                        }

                    }

                }
            }

        }
        */
    }

}

