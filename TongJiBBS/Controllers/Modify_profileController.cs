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
    public class Modify_profileController : Controller
    {
        // GET: api/modify_profile
        [HttpGet]
        public string Get(string user_id, string user_name, string school, string portrait)
        {
            User me = new User();
            //me.user_id = id;
            Hashtable ht = me.modify_info(user_id, user_name, school, portrait);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
}
