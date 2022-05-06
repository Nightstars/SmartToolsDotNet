using App.Common.Cache;
using App.Common.Enums;
using App.Common.Events;
using App.Common.Factory;
using App.Common.Interface;
using App.Common.Models.Cache;
using App.Common.Models.events;
using App.Common.Status;
using Flurl;
using Flurl.Http;
using HandyControl.Controls;
using HandyControl.Data;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Utils
{
    public class HttpUtil<Result> : IHttpUtil<Result> where Result : class
    {
        #region 初始化
        private readonly AppToken _appToken;
        private readonly Appuser _appuser;
        private readonly List<AppServer> _appserver;
        private readonly ICacheUtil _cacheUtil;
        private IEventAggregator _ea;
        public HttpUtil(IEventAggregator ea)
        {
            _cacheUtil = new CacheFactory().GetInstance();

            _appToken = _cacheUtil.GetToken();
            _appuser = _cacheUtil.GetAppuser();
            _appserver = _cacheUtil.GetAppServer();
            _ea = ea;

        }
        #endregion

        #region 验证Token
        public async Task<bool> ValidateTokenAsync()
        {
            if (DateTime.Now > Cache.Cache.appCache.Get<DateTime>(CacheKeys.ExpireAt))
            {
                try
                {
                    using HttpClient _client = new HttpClient(); ;
                    var newToken = await _client.RequestRefreshTokenAsync(new RefreshTokenRequest
                    {
                        Address = $"{Cache.Cache.appCache.Get<string>(CacheKeys.Authority)}/connect/token",

                        ClientId = "SmartTools",
                        ClientSecret = "secret",
                        RefreshToken = Cache.Cache.appCache.Get<string>(CacheKeys.RefreshToken)
                    });

                    if (newToken.HttpResponse.IsSuccessStatusCode)
                    {
                        ////更新应用缓存
                        Cache.Cache.appCache.Set(CacheKeys.Token, newToken?.AccessToken);
                        Cache.Cache.appCache.Set(CacheKeys.RefreshToken, newToken?.RefreshToken);
                        Cache.Cache.appCache.Set(CacheKeys.ExpireAt, DateTime.Now.AddSeconds((double)newToken?.ExpiresIn));

                        ////刷新持久化Token
                        var rs1 = _cacheUtil.SetToken(new AppToken
                        {
                            SeqNo = Guid.NewGuid().ToString("N"),
                            CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            Token = newToken?.AccessToken,
                            RefreshToken = newToken?.RefreshToken,
                            Expire = DateTime.Now.AddSeconds((double)newToken?.ExpiresIn).ToString("yyyy-MM-dd HH:mm:ss"),
                            Version = Cache.Cache.appCache.Get(CacheKeys.CurrentCmsVersion) as string,
                        });

                    }

                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
                
            }

            return true;
        }
        #endregion

        #region Post请求返回实体
        public async Task<Result> PostAsync(string url, object param)
        {
            var validatetokenresult = await ValidateTokenAsync();
            if (!validatetokenresult)
            {
                _ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object> { msgtype = MsgType.loginExpired });
                return null;
            }

            return await _appserver?.Find(x => x.Version == Cache.Cache.appCache.Get<string>(CacheKeys.CurrentCmsVersion))?.Host?.AppendPathSegment(url)
                            .WithHeader("Content-type","application/json")
                            .WithHeader("charset","UTF-8")
                            .WithHeader("Authorization",$"Bearer {Cache.Cache.appCache.Get<string>(CacheKeys.Token)}")
                            .PostJsonAsync(param)
                            .ReceiveJson<Result>();
        }
        #endregion

        #region Post请求返回流
        public async Task<Stream> PostStreamAsync(string url, object param)
        {
            var validatetokenresult = await ValidateTokenAsync();
            if (!validatetokenresult)
            {
                _ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object> { msgtype = MsgType.loginExpired });
                return null;
            }

            return await _appserver?.Find(x => x.Version == Cache.Cache.appCache.Get<string>(CacheKeys.CurrentCmsVersion))?.Host?.AppendPathSegment(url)
                            .WithHeader("Content-type", "application/json")
                            .WithHeader("charset", "UTF-8")
                            .WithHeader("Authorization", $"Bearer {Cache.Cache.appCache.Get<string>(CacheKeys.Token)}")
                            .PostJsonAsync(param)
                            .ReceiveStream();
        }
        #endregion
    }
}
