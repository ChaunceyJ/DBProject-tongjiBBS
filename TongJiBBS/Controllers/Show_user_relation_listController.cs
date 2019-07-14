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
    public class Show_user_relation_listController : Controller
    {
        // GET: api/show_user_relation_list
        [HttpPost]
        public string Get(string relation_type, string user_id,string target_id)
        {
            user_realation me = new user_realation();
            //me.user_id = id;
            List<Hashtable> ht = me.show_user_relation_list(relation_type, user_id,target_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
}
