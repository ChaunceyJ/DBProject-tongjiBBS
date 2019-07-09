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
    public class appointController : Controller
    {
        [HttpPost]
        public string appoint(string admin_id, string selected_id)
        {
            Admin ad = new Admin();
            Hashtable ht = ad.appointNewAdmin(admin_id, selected_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }

    [Route("api/[controller]")]
    public class modify_profile_by_superController : Controller
    {
        [HttpPost]
        public string modify_profile_by_super(string user_id, string school)
        {
            User u = new User();
            Hashtable ht = u.modifyByAdmin(user_id, school);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }

    [Route("api/[controller]")]
    public class get_report_listController : Controller
    {
        [HttpPost]
        public string get_report_list(string admin_id)
        {
            Report rp = new Report();
            Hashtable ht = rp.getReportList(admin_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }

    [Route("api/[controller]")]
    public class show_contentAdmin_listController : Controller
    {
        [HttpPost]
        public string show_contentAdmin_list()
        {
            Admin ad = new Admin();
            Hashtable ht = ad.show_contentAdmin_list();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }

    //[Route("api/[controller]")]
    //public class admin_delController : Controller
    //{
    //    [HttpPost]
    //    public string admin_del(string admin_id, string selected_id)
    //    {
    //        //Admin ad = new Admin();
    //        //Hashtable ht = ad.appointNewAdmin(admin_id, selected_id);
    //        //JavaScriptSerializer js = new JavaScriptSerializer();
    //        //string strJson = js.Serialize(ht);
    //        //return strJson;
    //    }
    //}

    [Route("api/[controller]")]
    public class show_user_profile_for_superAdminController : Controller
    {
        [HttpGet]
        //查看全部用户
        public string show_user_profile_for_superAdmin()
        {
            User u = new User();
            Hashtable ht = u.showAllUserProfileForSuperAdmin();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }

        [HttpGet("{user_id}")]
        //查看特定用户
        public string show_user_profile_for_superAdmin(string user_id)
        {
            User u = new User();
            Hashtable ht = u.showOneUserProfileForSuperAdmin(user_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
}


