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
    public class send_letterController : Controller
    {
        [HttpPost]
        public string Send(string sender_id, string receiver_id, string content)
        {
            private_letter pl= new private_letter();
          Hashtable ht = pl.insert_letter(sender_id, receiver_id, content);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }

        [HttpGet]
        public string get()
        {

            Hashtable ht = new Hashtable();
            ht.Add("dsofodsfnoisnfoie", "3fdsvsdfdf");
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }

    
   
}
