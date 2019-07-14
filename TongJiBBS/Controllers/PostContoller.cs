using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TongJiBBS.Models;
using System.Collections;
using System.Web.Script.Serialization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Oracle.ManagedDataAccess.Client;
using System.IO;


namespace TongJiBBS.Controllers
{  
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class postController : Controller
    {
        // GET api/post
        [HttpPost]
        public string post(string user_id, string select, string title, string content, ArrayList picture_name)
        {

            post post1 = new post();
            Hashtable ht = post1.Post(user_id, select, title, content);
            
            ht.Add("picture", insert_post_picture(picture_name, post1.post_id));
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }


        private bool insert_post_picture(ArrayList name, string _id)
        {
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        foreach(string i in name)
                        {
                            cmd.CommandText = "insert into picture(post_id, picture_id) values ('" + _id + "', '" + i + "')";

                            cmd.ExecuteNonQuery();
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


    }
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class del_postController : Controller
    {
        // GET api/del_post
        [HttpGet]
        public string del_post(string user_id, string post_id)
        {
            post post2 = new post();
            Hashtable ht = post2.Del_post(user_id, post_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class forwardController : Controller
    {
        // GET api/forward
        [HttpGet]
        public string forward(string user_id, string post_id)
        {
            post forward = new post();
            Hashtable ht = forward.Forward(user_id, post_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class allController : Controller
    {
        // GET api/all
        [HttpPost]
        public string all(string id)
        {
            post all1 = new post();
            Hashtable ht = all1.All(id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }


}



namespace TongJiBBS.Controllers
{
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class post2Controller : Controller
    {
        //POST api/post
        [HttpPost]
        public string post(string user_id, string section_id, string title, string content_1, IFormCollection Upload)
        {

            post post1 = new post();
            Hashtable ht = post1.Post(user_id, section_id, title, content_1);

            ht.Add("picture", upload(Upload, (string)ht["post_id"]));
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }


        private bool insert_post_picture(string name, string _id, Hashtable hs)
        {
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = "insert into picture(post_id, picture_id) values ('" + _id + "', '" + name + "')";

                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        hs.Add("error", ex.Message);
                        return false;
                    }

                }
            }
            return true;
        }

        private bool insert_portrait(string name, string _id, Hashtable hs)
        {
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = "update user_1 set potrait = '" + name + "' where user_id = '" + _id + "'";

                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        hs.Add("error", ex.Message);
                        return false;
                    }

                }
            }
            return true;
        }

        public string upload(IFormCollection Files, string id)
        {

            try
            {
                //var form = Request.Form;//直接从表单里面获取文件名不需要参数
                string dd = Files["File"];
                var form = Files;//定义接收类型的参数
                Hashtable hash = new Hashtable();
                IFormFileCollection cols = Request.Form.Files;
                if (cols == null || cols.Count == 0)
                {
                    return new string("status : -1 ,message : 没有上传文件, data :");
                }
                foreach (IFormFile file in cols)
                {
                    //定义图片数组后缀格式
                    string[] LimitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
                    //获取图片后缀是否存在数组中
                    string currentPictureExtension = Path.GetExtension(file.FileName).ToUpper();
                    if (LimitPictureType.Contains(currentPictureExtension))
                    {

                        //重新生成文件名称
                        //post -> post_id + timestamp    portrait -> user_id + timestamp
                        var new_name = id + DateTime.Now.ToString("yyyyMMddHHmmssfff") + currentPictureExtension;
                        string new_path = Path.Combine(common.src_posts, new_name);



                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", new_path);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {

                            //图片路径保存到数据库里面去
                            bool flage
                                 = insert_post_picture(new_name, id, hash);


                            if (flage == true)
                            {
                                //再把文件保存的文件夹中
                                file.CopyTo(stream);
                                hash.Add("file", "/" + new_path);
                            }
                        }
                    }
                    else
                    {
                        return new string(" status = -2, message = 请上传指定格式的图片, data = hash ");
                    }
                }

                return new string(" status = 0, message = 上传成功, data = hash ");
            }
            catch (Exception ex)
            {

                return new string("status = -3, message = 上传失败, data = ex.Message ");
            }

        }

    }

    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class post3Controller : Controller
    {
        // GET api/post
        [HttpPost]
        public string post(string user_id, string select, string title, string content)
        {

            post post1 = new post();
            Hashtable ht = post1.Post(user_id, select, title, content);
            foreach (string i in common.pic)
            {
                insert_post_picture(i, post1.post_id);
            }
            ht.Add("picture", common.pic);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }


        private bool insert_post_picture(string name, string _id)
        {
            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = "insert into picture(post_id, picture_id) values ('" + _id + "', '" + name + "')";

                        cmd.ExecuteNonQuery();



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
    }
}