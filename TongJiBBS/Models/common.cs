using NETCore.Encrypt;
using System.Collections.Generic;



namespace TongJiBBS.Models
{
    public class common
    {
        public static string conString = "User Id=system;Password=Aa123...;Data Source=localhost:1521/orcl;";

        public static List<string> pic = new List<string>();

        //MD-5加密生成密码
        public static string md5(string password, string ID)
        {
            if (!string.IsNullOrEmpty(password))
            {
                return EncryptProvider.Md5(password + ID);
            }
            return string.Empty;
        }
        public static string src_portrait = "uploads/images/portrait/";

        public static string src_posts = "uploads/images/posts/";

        private static string url_head = "http://192.168.1.200:52674/";

        public static string url_post_pic(string filename)
        {
            return url_head + src_posts + filename;
        }

        public static string url_portrait(string filename)
        {
            return url_head + src_portrait + filename;
        }

    }
}
