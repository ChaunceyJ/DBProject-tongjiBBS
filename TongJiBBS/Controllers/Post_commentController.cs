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
    public class commentController : Controller
    {
        // GET api/comment
        [HttpGet]
        public string comment(string content_1, string original_id, string actor_id,string at_id)
        {
            post_comment comment1 = new post_comment();
            Hashtable ht = comment1.Comment(content_1, original_id, actor_id,at_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class del_commentController : Controller
    {
        // GET api/del_comment
        [HttpGet]
        public string del_comment(string user_id, string comment_id)
        {
            post_comment comment2 = new post_comment();
            Hashtable ht = comment2.del_comment(user_id, comment_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
    
}
