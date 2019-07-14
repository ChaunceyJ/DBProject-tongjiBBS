using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace TongJiBBS.Models
{
    public class sendEmail
    {
        private SmtpClient client;
        private string sender = "714523356@qq.com";
        private string receiver;
        private string SMPT_Code = "chhmraczcjhpbdah";

        public Hashtable send(string ID)
        {
            client = new SmtpClient("smtp.qq.com", 587);
            receiver = ID + "@tongji.edu.cn";
            Random Rdm = new Random();
            //产生随机数验证码
            int iRdm = Rdm.Next(100000, 999999);
            string verfi_code = iRdm.ToString();

            Hashtable ht = new Hashtable();
            ht.Add("code", verfi_code);
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "insert into verification(user_id, code, time) values(:ID, :code, current_timestamp)";

                        // Assign id to the department number 50 


                        cmd.Parameters.Add(new OracleParameter(":ID", ID));
                        cmd.Parameters.Add(new OracleParameter(":code", verfi_code));

                        cmd.ExecuteNonQuery();

                        MailMessage msg = new MailMessage(sender, receiver, "TongJiBBS注册验证码", "您的验证码为：" + verfi_code);
                        client.UseDefaultCredentials = false;
                        System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(sender, SMPT_Code);
                        client.Credentials = basicAuthenticationInfo;
                        client.EnableSsl = true;
                        try
                        {
                            client.Send(msg);
                        }
                        catch (Exception ex)
                        {
                            ht.Add("error_send", ex.ToString());
                            Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                                        ex.ToString());
                        }
                        ht.Add("result", "success");
                    }
                    catch (Exception ex)
                    {
                        //await context.Response.WriteAsync(ex.Message);
                        ht.Add("result", "fail");
                        ht.Add("error2", ex.Message);
                    }

                }


            }

            return ht;
        }
    }
}
