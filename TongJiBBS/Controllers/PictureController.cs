using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TongJiBBS.Controllers
{
    [Route("api/[controller]")]
    public class PictureController : Controller
    {
        [HttpPost]
        public ActionResult Upload(IFormCollection Files, int EntId, int CrtUser)
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
                    return Json(new { status = -1, message = "没有上传文件", data = hash });
                }
                foreach (IFormFile file in cols)
                {
                    //定义图片数组后缀格式
                    string[] LimitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
                    //获取图片后缀是否存在数组中
                    string currentPictureExtension = Path.GetExtension(file.FileName).ToUpper();
                    if (LimitPictureType.Contains(currentPictureExtension))
                    {

                        //为了查看图片就不在重新生成文件名称了
                        // var new_path = DateTime.Now.ToString("yyyyMMdd")+ file.FileName;
                        var new_path = Path.Combine("uploads/images/", file.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", new_path);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {

                            //图片路径保存到数据库里面去
                            bool flage = true;
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
                        return Json(new { status = -2, message = "请上传指定格式的图片", data = hash });
                    }
                }

                return Json(new { status = 0, message = "上传成功", data = hash });
            }
            catch (Exception ex)
            {

                return Json(new { status = -3, message = "上传失败", data = ex.Message });
            }

        }


       
    }

   

}
