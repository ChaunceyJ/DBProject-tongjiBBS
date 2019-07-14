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
    public class View_profileController : Controller
    {
        // GET: api/view_profile
        [HttpPost]
        public string Get(string user_id, string target_id)
        {
            User me = new User();
            //me.user_id = id;
            Hashtable ht = me.get_objectorinfo(user_id,target_id);
            //Hashtable ht2 = me.get_fans(user_id);
            //Hashtable ht2 = me.get_fans(user_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
}
