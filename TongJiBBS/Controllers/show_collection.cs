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
    public class  Show_collectionController : Controller
        {

            // GET: api/show_collection
            [HttpPost]
            public String Get(String user_id)
            {
                User me = new User();
                //me.user_id = id;
                List<Hashtable> ht = me.show_collection(user_id);
                JavaScriptSerializer js = new JavaScriptSerializer();
                string strJson = js.Serialize(ht);
                return strJson;
            }
        }
}
