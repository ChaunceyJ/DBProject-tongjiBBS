using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TongJiBBS.Models;
using Oracle.ManagedDataAccess.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TongJiBBS.Controllers
{
    [Route("api/[controller]")]
    public class PictureTController : Controller
    {
        //type == 1 -> post; type == 2 -> portrait
        [HttpPost]
        public ActionResult Upload(IFormCollection Files)
        {
            Hashtable hash = new Hashtable();
            hash.Add("name", null);
            hash.Add("status", null);
            hash.Add("url", null);
            hash.Add("thumbUrl", null);

            try
            {
                //var form = Request.Form;//直接从表单里面获取文件名不需要参数
                string dd = Files["File"];
                var form = Files;//定义接收类型的参数

                IFormFileCollection cols = Request.Form.Files;
                if (cols == null || cols.Count == 0)
                {
                    hash["status"] = "-1";
                    return Json(hash);
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
                        var new_name = DateTime.Now.ToString("yyyyMMddHHmmssfff") + currentPictureExtension;
                        string new_path = Path.Combine(common.src_portrait, new_name);



                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", new_path);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {


                            hash["name"] = new_name;
                            hash["thumbUrl"] = hash["url"] = common.url_portrait(new_name);


                            //把文件保存的文件夹中
                            file.CopyTo(stream);
                            hash.Add("file", "/" + new_path);

                        }
                    }
                    else
                    {
                        hash["status"] = "-2";
                        return Json(hash);
                    }
                }

                hash["status"] = "done";

                return Json(hash);
            }
            catch (Exception ex)
            {
                hash["status"] = "-3";

                return Json(hash);
            }

        }

    }


    [Route("api/[controller]")]
    public class Check_confirmController : Controller
    {

        [HttpPost]
        public ActionResult Post(string id, string portrait)
        {
            Hashtable hash = new Hashtable();
            hash.Add("id", id);
            hash.Add("portrait", portrait);

            using (OracleConnection con = new OracleConnection(common.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();

                        cmd.CommandText = "update user_1 set portrait = '" + portrait + "' where user_id = '" + id + "'";

                        cmd.ExecuteNonQuery();

                        hash.Add("result", "success");
                    }
                    catch (Exception ex)
                    {
                        hash.Add("result", "fail");
                        hash.Add("error", ex.Message);
                    }

                }
            }
            return Json(hash);
        }
    }

}
