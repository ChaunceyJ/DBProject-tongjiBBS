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
        private string _send_id;
        private string _receiver_id;
        private string _content;
        private string _time;
        private string _letter_id;

        public private_letter(string send_id, string receiver_id, string content)
        {
            _send_id = send_id;
            _receiver_id = receiver_id;
            _content = content;
        }

        public private_letter() { }
        public Hashtable send(string send_id, string receiver_id, string content)
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
                        "insert into private_letter values(:letter_id,:send_id, :receiver_id, " +
                        "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss'), " +
                        ":content)";

                        Random ran = new Random();
                        string l_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
                        OracleParameter lid = new OracleParameter("letter_id", l_id);
                        OracleParameter sid = new OracleParameter("send_id", send_id);
                        OracleParameter rid = new OracleParameter("receiver_id", receiver_id);
                        OracleParameter c = new OracleParameter("content", content);
                        cmd.Parameters.Add(lid);
                        cmd.Parameters.Add(sid);
                        cmd.Parameters.Add(rid);
                        cmd.Parameters.Add(c);
                        cmd.ExecuteNonQuery();
                        ht.Add("status", "success");
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                        ht.Add("status", "failed");
                    }
                }
            }
            return ht;
        }

        public List<Hashtable> getNewChat(string receiver_id, string subId, string time)
        {
            Hashtable ht = new Hashtable();
            List<Hashtable> ls = new List<Hashtable>();
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText =
                        "select sender_id, content_1, time_1 from " +
                            "private_letter where receiver_id=:receiver_id and sender_id=:subId " +
                            "and time_1 >= to_date(:time,'yyyy-mm-dd hh24:mi:ss')" +
                            "order by time_1 asc";

                        OracleParameter rid = new OracleParameter("receiver_id", receiver_id);
                        cmd.Parameters.Add(rid);
                        OracleParameter tid = new OracleParameter("time", time);
                        cmd.Parameters.Add(tid);
                        OracleParameter sid = new OracleParameter("subId", subId);
                        cmd.Parameters.Add(sid);
                        OracleDataReader reader = cmd.ExecuteReader();

                        
                        while (reader.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("sender_id", reader.GetString(0));
                            temp.Add("content", reader.GetString(1));
                            temp.Add("time", reader.GetDateTime(2).ToString());
                            ls.Add(temp);
                        }
                        ht.Add("status", "success");
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                        ht.Add("status", "failed");
                        ls.Add(ht);
                    }
                }
            }
            return ls;
        }

        public List<Hashtable> getChatInfo(string mainId, string sub_Id)
        {
            Hashtable ht = new Hashtable();
            List<Hashtable> ls = new List<Hashtable>();
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        cmd.CommandText =
                        "select sender_id, receiver_id, content_1, time_1 from " +
                            "private_letter where (receiver_id=:mainId and sender_id=:sub_Id)" +
                            "or (receiver_id = :sub_Id and sender_id = :mainId)" +
                            "order by time_1 asc";

                        OracleParameter rid = new OracleParameter("mainId", mainId);
                        cmd.Parameters.Add(rid);
                        OracleParameter pid = new OracleParameter("sub_Id", sub_Id);
                        cmd.Parameters.Add(pid);
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("sender_id", reader.GetString(0));
                            temp.Add("receiver_id", reader.GetString(1));
                            temp.Add("content", reader.GetString(2));
                            temp.Add("time", reader.GetDateTime(3).ToString());
                            ls.Add(temp);
                        }
                        ht.Add("status", "success");
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                        ht.Add("status", "failed");
                    }
                }
            }
            return ls;
        }

        public List<Hashtable> getContacts(string id)
        {
            Hashtable ht = new Hashtable();
            List<Hashtable> ls = new List<Hashtable>();
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        
                        Hashtable temp = new Hashtable();
                        temp.Add("id", "1");
                        temp.Add("name", "张小龙");
                        ls.Add(temp);

                        Hashtable temp1 = new Hashtable();
                        temp1.Add("id", "2");
                        temp1.Add("name", "李明");
                        ls.Add(temp1);

                        Hashtable temp2 = new Hashtable();
                        temp2.Add("id", "3");
                        temp2.Add("name", "张三");
                        ls.Add(temp2);

                        Hashtable temp3 = new Hashtable();
                        temp3.Add("id", "4");
                        temp3.Add("name", "小丽");
                        ls.Add(temp3);

                        Hashtable temp4 = new Hashtable();
                        temp4.Add("id", "5");
                        temp4.Add("name", "孙俪");
                        ls.Add(temp4);

                        Hashtable temp5 = new Hashtable();
                        temp5.Add("id", "6");
                        temp5.Add("name", "李爱米");
                        ls.Add(temp5);

                        Hashtable temp6 = new Hashtable();
                        temp6.Add("id", "7");
                        temp6.Add("name", "卓玛");
                        ls.Add(temp6);

                        Hashtable temp7 = new Hashtable();
                        temp7.Add("id", "8");
                        temp7.Add("name", "小李");
                        ls.Add(temp7);

                        Hashtable temp8 = new Hashtable();
                        temp8.Add("id", "9");
                        temp8.Add("name", "韩梅梅");
                        ls.Add(temp8);

                        Hashtable temp9 = new Hashtable();
                        temp9.Add("id", "10");
                        temp9.Add("name", "李雷");
                        ls.Add(temp9);

                        Hashtable temp0 = new Hashtable();
                        temp0.Add("id", "11");
                        temp0.Add("name", "小华");
                        ls.Add(temp0);

                        ht.Add("status", "success");
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                        ht.Add("status", "failed");
                    }
                }
            }
            return ls;
        }

        //public Hashtable insert_letter(string sender_id, string receiver_id, string content)
        //{
        //    Hashtable ht = new Hashtable();
        //    using (OracleConnection con = new OracleConnection(common.conString))
        //    {
        //        using (OracleCommand cmd = con.CreateCommand())
        //        {
        //            try
        //            {
        //                con.Open();
        //                ht.Add("enter0", "insert0");
        //                cmd.BindByName = true;


        //                cmd.CommandText =
        //                "insert into private_letter values(:letter_id,:sender_id, :receiver_id, " +
        //                "to_date(to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'),'yyyy-mm-dd hh24:mi:ss'), " +
        //                ":content, 0)";

        //                Random ran= new Random();

        //                string l_id = DateTime.Now.ToString("yyMMddHHmmss") + ran.Next(0, 999).ToString();
        //                OracleParameter lid = new OracleParameter("letter_id", l_id);
        //                OracleParameter sid = new OracleParameter("sender_id", sender_id);
        //                OracleParameter rid = new OracleParameter("receiver_id", receiver_id);
        //                OracleParameter c = new OracleParameter("content", content);
        //                cmd.Parameters.Add(lid);
        //                cmd.Parameters.Add(sid);
        //                cmd.Parameters.Add(rid);
        //                cmd.Parameters.Add(c);


        //                cmd.ExecuteNonQuery();
        //                ht.Add("enter1", "insert1");

        //            }
        //            catch (Exception ex)
        //            {
        //                ht.Add("error", ex.Message);
        //            }

        //        }
        //    }
        //    return ht;
        //}

        //public Hashtable get_letter(string user_id, string target_id)
        //{
        //    Hashtable ht = new Hashtable();
        //    using (OracleConnection con = new OracleConnection(common.conString))
        //    {
        //        using (OracleCommand cmd = con.CreateCommand())
        //        {
        //            try
        //            {
        //                con.Open();
        //                cmd.BindByName = true;

        //                cmd.CommandText = "select letter_id, time_1, content_1 from " +
        //                    "private_letter where receiver_id=:user_id " +
        //                    "and sender_id = :target_id and read_flag = 0";

        //                cmd.Parameters.Add(new OracleParameter("user_id",user_id));
        //                cmd.Parameters.Add(new OracleParameter("target_id", target_id));

        //                OracleDataReader reader = cmd.ExecuteReader();
        //                //string letter_read_id="0";
        //                List<Hashtable> ls = new List<Hashtable>();
        //                while (reader.Read())
        //                {
        //                    Hashtable temp = new Hashtable();
        //                    string letter_read_id = reader.GetString(0);
        //                    //标记已读
        //                    setReadFlag(letter_read_id);

        //                    temp.Add("time", reader.GetDateTime(1).ToString());
        //                    temp.Add("content", reader.GetString(2));
        //                    ls.Add(temp);
        //                }
        //                ht.Add("letters", ls);
        //            }
        //            catch (Exception ex)
        //            {
        //                ht.Add("error", ex.Message);
        //            }
        //        }
        //    }
        //    return ht;
        //}

        //public Hashtable setReadFlag(string letter_id)
        //{
        //    Hashtable ht = new Hashtable();
        //    using (OracleConnection con = new OracleConnection(common.conString))
        //    {
        //        using (OracleCommand cmd = con.CreateCommand())
        //        {
        //            try
        //            {
        //                con.Open();
        //                cmd.BindByName = true;

        //                cmd.CommandText = "update private_letter set read_flag=1 where letter_id = :letter_id";
        //                cmd.Parameters.Add(new OracleParameter("letter_id", letter_id));
        //                cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //                ht.Add("error", ex.Message);
        //            }
        //        }
        //    }
        //    return ht;
        //}

    }
}
