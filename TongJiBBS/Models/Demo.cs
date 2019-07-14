using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections;


namespace TongJiBBS.Models
{
    public class Demo
    {
        private string user_id = "1752058";
        private string name;
        
        public Hashtable get_name()
        {
            Hashtable ht = new Hashtable();
            ht.Add("1", common.md5("1234", "1"));
            ht.Add("2", common.md5("1234", "2"));
            ht.Add("3", common.md5("1234", "3"));
            ht.Add("4", common.md5("1234", "4"));
            ht.Add("5", common.md5("1234", "5"));
            ht.Add("6", common.md5("1234", "6"));
            ht.Add("7", common.md5("1234", "7"));
            return ht;
        }

    }
}
