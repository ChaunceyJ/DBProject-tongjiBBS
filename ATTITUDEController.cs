using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TongJiBBS.Models;
using System.Collections;
using System.Web.Script.Serialization;
namespace TongJiBBS.Controllers
{
    
        [Route("api/[controller]")]
        public class attitudeController : Controller
        {
            // GET api/attitude
            [HttpGet]
            public string attitude(string post_id, string actor_id, int attitude_type)
            {
                attitude attitude1 = new attitude();
                Hashtable ht = attitude1.Attitude(post_id, actor_id, attitude_type);
                JavaScriptSerializer js = new JavaScriptSerializer();
                string strJson = js.Serialize(ht);
                return strJson;
            }
        }
    
}
