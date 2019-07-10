using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TongJiBBS.Models;
using System.Collections;
using System.Web.Script.Serialization;
using Microsoft.AspNetCore.Cors;
namespace TongJiBBS.Controllers
{  
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class postController : Controller
    {
        // GET api/post
        [HttpGet]
        public string post(string user_id, string section_id, string title, string content_1)
        {
            post post1 = new post();
            Hashtable ht = post1.Post(user_id, section_id, title, content_1);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
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
        [HttpGet]
        public string all(string post_id)
        {
            post all1 = new post();
            Hashtable ht = all1.All( post_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }

}
