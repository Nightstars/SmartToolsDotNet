using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Status
{
    public class State
    {
        //当前登录的CMS版本
        public static string CurrentCmsVersion = string.Empty;

        //当前登录的用户名
        public static string  UserName = string.Empty;

        //当前Site
        public static string Site = string.Empty;

        //Token
        public static string Token = string.Empty;

        //Authority
        public static string Authority = string.Empty;

        //RefreshToken
        public static string RefreshToken = string.Empty;

        //工厂
        public static string CorpName = string.Empty;

        //过期时间
        public static DateTime ExpireAt;

    }
}
