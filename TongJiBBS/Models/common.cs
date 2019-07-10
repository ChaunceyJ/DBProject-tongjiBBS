using NETCore.Encrypt;

namespace TongJiBBS.Models
{
    public class common
    {
        public static string conString = "User Id=system;Password=1234;Data Source=localhost:1521/orcl;";

        //MD-5加密生成密码
        public static string md5(string password, string ID)
        {
            if (!string.IsNullOrEmpty(password))
            {
                return EncryptProvider.Md5(password+ID);           
            }
            return string.Empty;
        }
    }

   

}
