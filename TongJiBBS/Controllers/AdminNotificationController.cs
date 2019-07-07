﻿using System;
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
    public class admin_del_notifyController : Controller
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