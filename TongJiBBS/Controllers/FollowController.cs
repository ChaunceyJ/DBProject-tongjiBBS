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
    [Route("api/[Controller]")]
    public class FollowController : Controller
    {
        // GET: api/follow
        [HttpPost]
        public string Get(string actor_id, string object_id)
        {
            user_realation me = new user_realation();
            //me.user_id = id;
            Hashtable ht = me.follow(actor_id, object_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
}
