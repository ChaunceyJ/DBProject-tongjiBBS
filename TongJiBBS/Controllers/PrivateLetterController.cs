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
    //[Route("api/[controller]")]
    //public class send_letterController : Controller
    //{
    //    [HttpPost]
    //    public string Send(string sender_id, string receiver_id, string content)
    //    {
    //        private_letter pl= new private_letter();
    //        Hashtable ht = pl.insert_letter(sender_id, receiver_id, content);
    //        JavaScriptSerializer js = new JavaScriptSerializer();
    //        string strJson = js.Serialize(ht);
    //        return strJson;
    //    }
    //}

    //[Route("api/[controller]")]
    //public class get_letterController : Controller
    //{
    //    [HttpPost]
    //    public string Send(string user_id, string target_id)
    //    {
    //        private_letter pl = new private_letter();
    //        Hashtable ht = pl.get_letter(user_id, target_id);
    //        JavaScriptSerializer js = new JavaScriptSerializer();
    //        string strJson = js.Serialize(ht);
    //        return strJson;
    //    }
    //}
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class contactsController : Controller
    {
        [HttpGet]
        public string Contacts(string id)
        {
            private_letter pl = new private_letter();
            List<Hashtable> ht = pl.getContacts(id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class newChatController : Controller
    {
        [HttpGet]
        public string newChat(string mainId, string subId, string time)
        {
            private_letter pl = new private_letter();
            List<Hashtable> ht = pl.getNewChat(mainId, subId,time);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class chatInfoController : Controller
    {
        [HttpGet]
        public string ChatInfo(string mainId, string subId)
        {
            private_letter pl = new private_letter();
            List<Hashtable> ht = pl.getChatInfo(mainId, subId);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
    [EnableCors("Domain")]
    [Route("api/[controller]")]
    public class sendChatController : Controller
    {
        [HttpPost]
        public string sendChat(string send_id, string receiver_id, string content)
        {
            private_letter pl = new private_letter();
            Hashtable ht = pl.send(send_id, receiver_id, content);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
}
