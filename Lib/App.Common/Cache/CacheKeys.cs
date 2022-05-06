using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Cache
{
    public class CacheKeys
    {
        //当前登录的CMS版本
        public static string CurrentCmsVersion = "CurrentCmsVersion";

        //当前登录的用户名
        public static string UserName = "UserName";
        
        //当前登录的用ID
        public static string UserId = "UserId";

        //当前Site
        public static string Site = "Site";

        //Token
        public static string Token = "Token";

        //Authority
        public static string Authority = "Authority";

        //RefreshToken
        public static string RefreshToken = "RefreshToken";

        //工厂
        public static string CorpName = "CorpName";

        //过期时间
        public static string ExpireAt = "ExpireAt";
    }
}
