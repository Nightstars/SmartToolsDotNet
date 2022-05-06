using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Wpf;
using User.Core.Interface;
using User.Core.Models;
using Newtonsoft.Json;
using App.Common.Factory;
using App.Common.Models.Cache;
using App.Common.Status;
using App.Common.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace User.Core.Utils
{
    public class LoginUtil
    {

        public async Task<AppUser> Login(string authority, bool IsLogin)
        {
            var options = new OidcClientOptions()
            {
                Authority = authority,
                ClientId = "SmartTools",
                ClientSecret = "secret",
                Scope = "openid offline_access api_sysfile api_sysuni",
                RedirectUri = "http://127.0.0.1/smarttoolsdotnet-app",
                Browser = new WpfBrowser(),
                Policy = new Policy
                {
                    RequireIdentityTokenSignature = false,
                    Discovery = new IdentityModel.Client.DiscoveryPolicy
                    {
                        RequireHttps = false
                    }
                },
            };

            var _oidcClient = new OidcClient(options);

            LoginResult loginResult = null;
            LogoutResult logoutResult = null;
            try
            {
                if(!IsLogin)
                    loginResult =await _oidcClient.LoginAsync();
                else
                    logoutResult = await _oidcClient.LogoutAsync();
            }
            catch (Exception exception)
            {
                return new AppUser
                {
                    ErrorMessage = exception.Message
                };
            }

            if (!IsLogin)
            {
                if (loginResult.IsError)
                {
                    return new AppUser
                    {
                        ErrorMessage = loginResult.Error == "UserCancel" ? "登录窗口已关闭,授权未完成" : loginResult.Error
                    };
                }
                else
                {
                    string userinfo = loginResult.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata")?.Value;
                    var user = JsonConvert.DeserializeObject<AppUser>(userinfo);

                    var instance = new CacheFactory().GetInstance();
                    var rs = instance.SetAppuser(new Appuser
                    {
                        SeqNo = Guid.NewGuid().ToString("N"),
                        CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        UserId = user?.UserId,
                        UserName = user?.UserName,
                        UserEmail = user?.EmailAddress,
                        CropId = user?.CurrentCorpInfo?.CorpId,
                        CropName = user?.CurrentCorpInfo?.CorpName,
                        CropCode = user?.CurrentCorpInfo?.CorpSccCode,
                        Site = user?.CurrentCorpInfo?.Site,
                        Version = Cache.appCache.Get(CacheKeys.CurrentCmsVersion) as string,
                    });

                    var rs1 = instance.SetToken(new AppToken
                    {
                        SeqNo = Guid.NewGuid().ToString("N"),
                        CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Token = loginResult?.AccessToken,
                        RefreshToken = loginResult?.RefreshToken,
                        Expire = loginResult?.AccessTokenExpiration.DateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        //Version = State.CurrentCmsVersion,
                        Version = Cache.appCache.Get(CacheKeys.CurrentCmsVersion) as string,
                    });

                    //登录完成缓存状态
                    //State.UserName = user?.UserName;
                    //State.Site = user?.CurrentCorpInfo?.Site;
                    //State.CorpName = user?.CurrentCorpInfo?.CorpName;
                    //State.Token = loginResult?.AccessToken;
                    //State.RefreshToken = loginResult?.RefreshToken;
                    //State.ExpireAt = (DateTime)loginResult?.AccessTokenExpiration.DateTime;

                    Cache.appCache.Set(CacheKeys.UserId, user?.UserId);
                    Cache.appCache.Set(CacheKeys.UserName, user?.UserName);
                    Cache.appCache.Set(CacheKeys.Site, user?.CurrentCorpInfo?.Site);
                    Cache.appCache.Set(CacheKeys.CorpName, user?.CurrentCorpInfo?.CorpName);
                    Cache.appCache.Set(CacheKeys.Token, loginResult?.AccessToken);
                    Cache.appCache.Set(CacheKeys.RefreshToken, loginResult?.RefreshToken);
                    Cache.appCache.Set(CacheKeys.ExpireAt, (DateTime)loginResult?.AccessTokenExpiration.DateTime);

                    user.IsError = false;
                    return user;
                }
            }
            else
            {
                if (logoutResult.IsError)
                {
                    return new AppUser
                    {
                        ErrorMessage = logoutResult.Error
                    };
                }
                else
                {
                    return new AppUser { IsError = false };
                }
            }

            return new AppUser { ErrorMessage = "登陆失败" };
        }
    }
}
