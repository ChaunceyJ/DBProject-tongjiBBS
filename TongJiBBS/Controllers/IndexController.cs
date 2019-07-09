using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TongJiBBS.Models;
using System.Collections;
using System.Web.Script.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TongJiBBS.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        
        // POST api/<controller>
        [HttpPost]
        public string Post(string id, string name, string password, string verification_code)
        {
            //return id+name+password+verification_code;
            User Newuser = new User(id, name, password);
            Hashtable ht = Newuser.signUp(verification_code);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }

    }

    [Route("api/[controller]")]
    public class Send_verificationController : Controller
    {
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            sendEmail sE = new sendEmail();
            Hashtable ht = sE.send(id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }

        

    }
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        // POST api/<controller>
        [HttpPost]
        public string Post(string id, string password)
        {
            //return id+name+password+verification_code;
            User Olduser = new User(id, password);
            Hashtable ht = Olduser.login();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }

    }

    [Route("api/[controller]")]
    public class Show_postController : Controller
    {

        // POST api/<controller>
        [HttpPost]
        public string Post(string id, string section_id)
        {
            Post Posts = new Post(section_id, id);
            List<Hashtable> reslut = Posts.getAllPost_30();
           // Hashtable ht = new Hashtable();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(reslut);
            return strJson;
        }

    }

    [Route("api/[controller]")]
    public class Search_postController : Controller
    {

        // POST api/<controller>
        //id content 必须； section_id默认为00；
        [HttpPost]
        public string Post(string id, string content, string section_id = "00")
        {
            Post Posts = new Post(section_id, id);
            List<Hashtable> reslut = Posts.searchPost_30(content);
            // Hashtable ht = new Hashtable();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(reslut);
            return strJson;
        }

    }

    [Route("api/[controller]")]
    public class Get_user_notificationController : Controller
    {

        // POST api/<controller>
        //type == 1 -> attitude; type == 2 -> comment
        //read = 0<>
        [HttpPost]
        public string Post(string id, int type)
        {
            Notification notices = new Notification(id);
            List<Hashtable> reslut = notices.getUserNotice(type);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(reslut);
            return strJson;
        }

    }

    [Route("api/[controller]")]
    public class Get_admin_notificationController : Controller
    {

        // POST api/<controller>
        //type == 1 -> attitude; type == 2 -> comment
        //read = 0<>
        [HttpPost]
        public string Post(string id)
        {
            Notification notices = new Notification(id);
            List<Hashtable> reslut = notices.getAdminNotice();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(reslut);
            return strJson;
        }

    }
}
