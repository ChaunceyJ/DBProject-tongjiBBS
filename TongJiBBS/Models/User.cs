using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Oracle.ManagedDataAccess.Client;

namespace TongJiBBS.Models
{
    public class User
    {
        private string user_ID;
        private string user_name;
        private int credit;//信誉积分
        private string password;
        private string portrait;
        private string school;
        private string identity;

        public User() { }

        public User(string ID, string _password)
        {
            user_ID = ID;
            password = common.md5(_password, ID);

        }

        public User(string ID, string name, string _password)
        {
            user_ID = ID;
            user_name = name;
            password = common.md5(_password, ID);
            credit = 20;
            //school = _school;
            //identity = _identity;
        }

        private bool getUserinfo()
        {

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
                        cmd.CommandText = "select user_name, credit, portrait from USER_1 where USER_ID = '" + this.user_ID + "'";

                        OracleDataReader reader0 = cmd.ExecuteReader();
                        if (reader0.Read())
                        {
                            this.user_name = reader0.GetString(0);
                            this.credit = reader0.GetInt32(1);
                            this.portrait = reader0.GetString(2);

                        }

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        return false;
                    }

                }

            }
            return true;
        }

    

        public Hashtable signUp(string verficode)
        {
            Hashtable ht = new Hashtable();
            string send_code = "00";
            //验证验证码是否正确
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

            if (send_code == verficode)
            {
                //插入用户信息
                Console.Write("sucess verfi");
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
                            cmd.CommandText = "INSERT INTO USER_1( USER_ID, USER_NAME, CREDIT, PASSWORD_1) VALUES( :ID, :name, :credit, :password)";

                            // Assign id to the department number 50 


                            cmd.Parameters.Add(new OracleParameter(":ID", this.user_ID));
                            cmd.Parameters.Add(new OracleParameter(":name", this.user_name));
                            cmd.Parameters.Add(new OracleParameter(":credit", this.credit));
                            cmd.Parameters.Add(new OracleParameter(":password", this.password));

                            cmd.ExecuteNonQuery();

                            
                        }
                        catch (Exception ex)
                        {
                            //await context.Response.WriteAsync(ex.Message);
                            ht.Add("result", "fail");
                            ht.Add("error2", ex.Message);
                        }

                    }

                }

                this.getUserinfo();
                ht.Add("result", "success");
                ht.Add("portrait", common.url_portrait(this.portrait));
                ht.Add("name", this.user_name);
                ht.Add("id", this.user_ID);

            }
            else
            {
                ht.Add("result", "fail");
                ht.Add("reason", "verficode error");
            }
            return ht;
        }

        public Hashtable login()
        {
            Hashtable ht = new Hashtable();
            //验证密码是否正确
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
                        cmd.CommandText = "select PASSWORD_1 from USER_1 where USER_ID = :id";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", user_ID);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            Console.Write('\n'+reader.GetString(0)+'\n');
                            if(reader.GetString(0) != this.password)
                            {
                                ht.Add("result", "fail");
                                ht.Add("reason", "password error");
                            }
                            else
                            {
                                //这里有问题
                                cmd.CommandText = "select freeze_start_time, freeze_en_time from freeze where USER_ID = :ID and freeze_en_time > sysdate";
                                OracleDataReader reader2 = cmd.ExecuteReader();
                                if (reader2.Read())
                                {
                                    Console.Write('\n' + reader2.GetDateTime(0).ToString("dd/MM/yy") + '\n');
                                    ht.Add("result", "fail");
                                    ht.Add("reason", "freeze account");
                                    ht.Add("start_time", reader2.GetDateTime(0).ToString("dd/MM/yy"));
                                    ht.Add("end_time", reader2.GetDateTime(1).ToString("dd/MM/yy"));
                                }
                                else
                                {
                                    this.getUserinfo();
                                    ht.Add("result", "success");
                                    ht.Add("portrait", common.url_portrait(this.portrait));
                                    ht.Add("name", this.user_name);
                                    ht.Add("id", this.user_ID);

                                }
                            }
                        }
                        else
                        {
                            ht.Add("result", "fail");
                            ht.Add("reason", "ID not exist");
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

        //管理员修改用户学院信息
        public Hashtable modifyByAdmin(string user_id, string school)
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
                            "update user_1 set school = :school where user_id = :user_id";

                        cmd.Parameters.Add(new OracleParameter("user_id", user_id));
                        cmd.Parameters.Add(new OracleParameter("school", school));

                        cmd.ExecuteNonQuery();
                        ht.Add("asd", "dfdf");
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }

                }
            }
            return ht;

        }

        //查看特定用户
        public Hashtable showOneUserProfileForSuperAdmin(string user_id)
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
                        //user_id, name, credit, password, portrait, school, identity
                        cmd.CommandText =
                            "select * from user_1 where user_id = :user_id";
                        cmd.Parameters.Add(new OracleParameter("user_id", user_id));
                        OracleDataReader rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            ht.Add("user_id", rd.GetString(0));
                            ht.Add("name", rd.GetString(1));
                            ht.Add("credit", rd.GetInt32(2));
                            ht.Add("password", rd.GetString(3));
                            ht.Add("portrait", rd.GetString(4));
                            ht.Add("school", rd.GetString(5));
                            ht.Add("identity", rd.GetString(6));
                        }
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }

                }
            }
            return ht;
        }
    

        //查看所有用户
        public Hashtable showAllUserProfileForSuperAdmin()
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
                        //user_id, name, credit, password, portrait, school, identity
                        cmd.CommandText =
                            "select * from user_1";
                        OracleDataReader rd = cmd.ExecuteReader();
                        List<Hashtable> ls = new List<Hashtable>();
                        while(rd.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("user_id", rd.GetString(0));
                            temp.Add("name", rd.GetString(1));
                            temp.Add("credit", rd.GetInt32(2));
                            temp.Add("password", rd.GetString(3));
                            temp.Add("portrait", rd.GetString(4));
                            temp.Add("school", rd.GetString(5));
                            temp.Add("identity", rd.GetString(6));
                            ls.Add(temp);
                        }
                        ht.Add("users", ls);
                    }
                    catch (Exception ex)
                    {
                        ht.Add("error", ex.Message);
                    }
                }
            }
            return ht;
        }

        public Hashtable get_myinfo(string user_id)
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

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select USER_NAME,CREDIT,portrait,SCHOOL,IDENTITY_1 from USER_1 where USER_ID = :id";

                        // Assign id to the department number 50 
                        cmd.Parameters.Add(new OracleParameter("id", user_id));

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("name", reader.GetString(0));
                            if(reader.GetInt16(1)==' ')
                            {
                                ht.Add("credit", 0);
                            }
                            else
                            {
                                ht.Add("credit", reader.GetInt16(1));
                            }
                            
                            ht.Add("portrait", common.url_portrait(reader.GetString(2)));
                            //ht.Add("portrait", reader.GetString(2));
                            ht.Add("school", reader.GetString(3));
                            ht.Add("identity", reader.GetString(4));
                        }
                        //reader.Dispose();

                        cmd.CommandText = "select count(*) from USER_RELATION where ACTOR_ID =:id";
                        OracleDataReader reader2 = cmd.ExecuteReader();
                        cmd.Parameters.Add(new OracleParameter("id", user_id));
                        while (reader2.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("following_number", reader2.GetInt16(0));
                        }
                        //reader2.Dispose();

                        cmd.CommandText = "select count(*) from USER_RELATION where ACTOR_ID =:id";
                        OracleDataReader reader3 = cmd.ExecuteReader();
                        while (reader3.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("followed_number", reader3.GetInt16(0));
                        }
                        //reader3.Dispose();

                        cmd.CommandText = "select count(*) from POST where USER_ID =:id";
                        OracleDataReader reader4 = cmd.ExecuteReader();
                        while (reader4.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("post_number", reader4.GetInt16(0));
                        }
                        //reader4.Dispose();

                        cmd.CommandText = "select POST.POST_ID,SECTION_ID,TIME_1,TITLE,DELETE_FLAG,CONTENT_1,picture_id from POST join picture on post.post_id=picture.post_id where post.USER_ID=:id";
                        cmd.Parameters.Add(new OracleParameter("id", user_id));
                        OracleDataReader reader5 = cmd.ExecuteReader();

                        List<Hashtable> posts = new List<Hashtable>();
                        while (reader5.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("post_id", reader5.GetString(0));
                            temp.Add("section_id", reader5.GetString(1));
                            temp.Add("time_1", reader5.GetDateTime(2).ToString());
                            temp.Add("title", reader5.GetString(3));
                            temp.Add("delete_flag", reader5.GetInt16(4));
                            temp.Add("content_1", reader5.GetString(5));
                            temp.Add("picture", common.url_post_pic(reader5.GetString(6)));
                            //temp.Add("FORWORD_FROM_ID", reader.GetString(5));
                            posts.Add(temp);
                        }
                        ht.Add("posts", posts);

                        reader.Dispose();
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
        public Hashtable modify_info(string user_id, string user_name, string school, string portrait)
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

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "UPDATE USER_1 SET USER_NAME = :u_name, PORTRAIT= :u_portrait,SCHOOL=:u_school where USER_ID = :id";

                        // Assign id to the department number 50 
                        cmd.Parameters.Add(new OracleParameter("id", user_id));
                        cmd.Parameters.Add(new OracleParameter("u_school", school));
                        cmd.Parameters.Add(new OracleParameter("u_portrait", portrait));
                        cmd.Parameters.Add(new OracleParameter("u_name", user_name));
                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("SUCCESS", reader.GetString(0));
                        }

                        reader.Dispose();
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
        public Hashtable get_objectorinfo(string user_id, string target_id)
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

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select USER_NAME,CREDIT,portrait,SCHOOL,IDENTITY_1 from USER_1 where USER_ID = :id";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", target_id);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("name", reader.GetString(0));
                            ht.Add("credit", reader.GetInt16(1));
                            ht.Add("portrait", common.url_portrait(reader.GetString(2)));
                            ht.Add("school", reader.GetString(3));
                            ht.Add("identity", reader.GetString(4));
                        }
                        cmd.CommandText = "select count(*) from USER_RELATION where ACTOR_ID =:id";
                        OracleDataReader reader2 = cmd.ExecuteReader();
                        while (reader2.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("following_number", reader2.GetInt16(0));
                        }
                        cmd.CommandText = "select count(*) from USER_RELATION where ACTOR_ID =:id";

                        OracleDataReader reader3 = cmd.ExecuteReader();
                        while (reader3.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("followed_number", reader3.GetInt16(0));
                        }
                        cmd.CommandText = "select count(*) from POST where USER_ID =:id";

                        OracleDataReader reader4 = cmd.ExecuteReader();
                        while (reader4.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            ht.Add("post_number", reader4.GetInt16(0));
                        }

                        cmd.CommandText = "select count(*) from USER_RELATION where OBJECT_ID =:id and ACTOR_ID=:id_user and RELATION_TYPE= 1";
                        cmd.Parameters.Add(new OracleParameter("id_user", user_id));
                        OracleDataReader reader5 = cmd.ExecuteReader();
                        while (reader5.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            if (reader5.GetInt16(0) == 1)
                            {
                                ht.Add("is_following", 1);
                            }
                            else
                            {
                                ht.Add("is_following", 0);
                            }

                        }
                        cmd.CommandText = "select count(*) from USER_RELATION where OBJECT_ID =:id and ACTOR_ID=:id_user and RELATION_TYPE= 0";
                        cmd.Parameters.Add(new OracleParameter("id_user", user_id));
                        OracleDataReader reader6 = cmd.ExecuteReader();
                        while (reader6.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            if (reader6.GetInt16(0) == 1)
                            {
                                ht.Add("is_blocked", 1);
                            }
                            else
                            {
                                ht.Add("is_blocked", 0);
                            }

                        }
                        cmd.CommandText = "select count(*) from USER_RELATION where ACTOR_ID =:id and OBJECT_ID=:id_user and RELATION_TYPE= 1";
                        cmd.Parameters.Add(new OracleParameter("id_user", user_id));
                        OracleDataReader reader7 = cmd.ExecuteReader();
                        while (reader7.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                            if (reader7.GetInt16(0) == 1)
                            {
                                ht.Add("is_my_fan", "myfans");
                            }
                            else
                            {
                                ht.Add("is_my_fans", "notmyfans");
                            }

                        }
                        cmd.CommandText = "select POST.POST_ID,SECTION_ID,TIME_1,TITLE,DELETE_FLAG,CONTENT_1,picture_id from POST join picture on post.post_id=picture.post_id where USER_ID=:id";
                        cmd.Parameters.Add(new OracleParameter("id", user_id));
                        OracleDataReader reader8 = cmd.ExecuteReader();
                        List<Hashtable> posts = new List<Hashtable>();
                        while (reader8.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("post_id", reader8.GetString(0));
                            temp.Add("section_id", reader8.GetString(1));
                            temp.Add("time_1", reader8.GetDateTime(2).ToString());
                            temp.Add("title", reader5.GetString(3));
                            temp.Add("delete_flag", reader8.GetInt16(4));
                            temp.Add("content_1", reader8.GetString(5));
                            temp.Add("picture", common.url_post_pic(reader8.GetString(6)));
                            //temp.Add("FORWORD_FROM_ID", reader8.GetString(5));
                            posts.Add(temp);
                        }
                        ht.Add("posts", posts);
                        reader.Dispose();

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
        public List<Hashtable> show_collection(String user_id)
        {
            Hashtable ht = new Hashtable();
            List<Hashtable> posts = new List<Hashtable>();
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
                        //cmd.CommandText = "select POST_ID,SECTION_ID,TIME_1,TITLE,DELETE_FLAG,CONTENT_1 from POST where USER_ID=:id";
                        cmd.CommandText = "select post_id from post_attitude where actor_id=:id and attitude_type=3";
                        // Assign id to the department number 50 
                        cmd.Parameters.Add(new OracleParameter("id", user_id));

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        //List<Hashtable> posts = new List<Hashtable>();
                        while (reader.Read())
                        {
                            Hashtable temp = new Hashtable();
                            temp.Add("post_id", reader.GetString(0));
                            posts.Add(temp);
                        }
                        //ht.Add("posts", posts);
                        //ht.Add("111", "222");
                        //ht.Add("youinfo", user_id);
                        //while (reader.Read())
                        //{
                        //    //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                        //    ht.Add("post_id", reader.GetString(0));
                        //    ht.Add("section_id", reader.GetString(1));
                        //    ht.Add("time", reader.GetString(2));
                        //    ht.Add("title", reader.GetString(3));
                        //    ht.Add("flag", reader.GetInt16(4));
                        //    ht.Add("content", reader.GetString(5));
                        //    ht.Add("forword", reader.GetString(6));
                        //}
                        foreach (Hashtable i in posts)
                        {
                            //i["post_id"]
                            //Hashtable temp = new Hashtable();
                            cmd.CommandText = "select SECTION_ID,TIME_1,TITLE,DELETE_FLAG,CONTENT_1 from post where post_id=:pos";
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new OracleParameter("pos", i["post_id"]));
                            OracleDataReader reader2 = cmd.ExecuteReader();
                            //i.Add("sss", i["post_ids"]);
                            while (reader2.Read())
                            {

                                i.Add("section_id", reader2.GetString(0));
                                i.Add("time_1", reader2.GetDateTime(1).ToString());
                                i.Add("title", reader2.GetString(2));
                                i.Add("delete_flag", reader2.GetInt16(3));
                                i.Add("content_1", reader2.GetString(4));
                                //i.Add("pop", reader2.GetString(5));
                               // i.Add("picture", common.url_post_pic(reader.GetString(5)));
                            }
                            cmd.CommandText = "select picture_id from picture where post_id=:pos";
                            //cmd.Parameters.Add(new OracleParameter("postt", i["post_ids"]));
                            OracleDataReader reader3 = cmd.ExecuteReader();
                            if (reader3.Read())
                            {
                                i.Add("pictures", common.url_post_pic(reader3.GetString(0)));
                            }
                         }
                            //reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Hashtable info = new Hashtable();
                        //wait context.Response.WriteAsync(ex.Message);

                        info.Add("error", ex.Message);
                        posts.Add(info);
                    }
                }

            }
            //ht.Add("111", "111");
            return posts;
        }
    }
}
