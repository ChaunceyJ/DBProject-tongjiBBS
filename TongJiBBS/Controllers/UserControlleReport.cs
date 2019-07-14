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
    public class reportController : Controller
    {
        [HttpPost]
        public string SendReport(string post_id, string actor_id, string reason)
        {
            Report rp = new Report();
            Hashtable ht = rp.sendReport(post_id, actor_id, reason);
            rp.AddToNotification(post_id, reason);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }

}
