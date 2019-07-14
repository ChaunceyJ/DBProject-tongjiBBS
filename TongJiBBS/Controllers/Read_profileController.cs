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
    public class Read_profileController : Controller
    {
        // GET: api/read_profile
        [HttpPost]
        public String Get(string user_id)
        {
            User me = new User();
            //me.user_id = id;
            Hashtable ht = me.get_myinfo(user_id);
            //Hashtable ht2 = me.get_fans(user_id);
            //Hashtable ht2 = me.get_fans(user_id);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJson = js.Serialize(ht);
            return strJson;
        }
    }
   
    //[Route("api/[controller]")]
    //public class user_infoController : Controller
    //{
    //    //GET: api/

    //}
   
    
    //[Route("api/[controller]")]
    //public class DemosController : Controller
    //{
    //    // GET: api/Demos
    //    [HttpGet]
    //    public IEnumerable<string> Get()
    //    {
    //        return new string[] { "value1", "value2" };
    //    }

    //    // GET api/<controller>/5
    //    [HttpGet("{id}")]
    //    public string Get(int id)
    //    {
    //        return "value";
    //    }

    //    // POST api/<controller>
    //    [HttpPost]
    //    public void Post([FromBody]string value)
    //    {
    //    }

    //    // PUT api/<controller>/5
    //    [HttpPut("{id}")]
    //    public void Put(int id, [FromBody]string value)
    //    {
    //    }

    //    // DELETE api/<controller>/5
    //    [HttpDelete("{id}")]
    //    public void Delete(int id)
    //    {
    //    }
    //}
}
