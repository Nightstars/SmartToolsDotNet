using App.Common.Cache;
using App.Common.Enums;
using App.Common.Events;
using App.Common.Factory;
using App.Common.Models.Cache;
using App.Common.Models.Common;
using App.Common.Models.events;
using App.Common.Status;
using App.Common.Utils;
using CodeleSmartSoft.SmartUI.WPF.Controls;
using HandyControl.Controls;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using User.Core.Factory;
using User.Core.Models;

namespace UserUI.ViewModels
{
    public class ProfileViewModel : BindableBase, INavigationAware,IRegionMemberLifetime
    {
        #region 初始化
        private IRegionNavigationJournal _navigationJournal;
        private IEventAggregator _ea;
        public ProfileViewModel(IEventAggregator ea)
        {
            _ea = ea;
            UserCorps = new NotifyTaskCompletion<List<ComboboxItem>>(LoadData());
        }
        #endregion

        #region 导航
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationJournal = navigationContext.NavigationService.Journal;
        }
        #endregion

        #region 返回
        public ICommand GoBackCommand => new DelegateCommand<object>(OnGoBack);

        private void OnGoBack(Object obj)
        {
            //_regionManager.RequestNavigate("contentRegion", "AdvanceCodeless");
            _navigationJournal.GoBack();
        }
        #endregion

        #region 注销
        public ICommand LogoutCommand => new DelegateCommand<object>(OnLogout);

        private async void OnLogout(object obj)
        {
            _navigationJournal.GoBack();
            var userinfo =await new LoginUtilFactory().GetInstance().Login(Cache.appCache.Get<string>(CacheKeys.Authority), true);

            _ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object> { msgtype = MsgType.logout, data = userinfo });
        }
        #endregion

        #region 是否保留页面
        public bool KeepAlive => false;
        #endregion

        #region 用户工厂信息
        private string _userCorp;
        public string UserCorp
        {
            get { return _userCorp; }
            set { SetProperty(ref _userCorp, value); }
        }

        private NotifyTaskCompletion<List<ComboboxItem>> _UserCorp;

        public NotifyTaskCompletion<List<ComboboxItem>> UserCorps
        {
            get { return _UserCorp; }
            set { SetProperty(ref _UserCorp, value); }
        }
        #endregion

        #region 切换工厂
        public ICommand CorpChangeCommand => new DelegateCommand<object>(OnChangSite);

        private async void OnChangSite(object obj)
        {
            try
            {
                var changers = await new HttpUtil<CommonResult<AppUser>>(_ea).PostAsync("PlatformCenter/Corp/ChangeSite", new { UserId  = Cache.appCache.Get<string>(CacheKeys.UserId) as string, SccCode = _userCorp });

                ////更新应用缓存
                Cache.appCache.Set(CacheKeys.UserName, changers.Data?.UserName);
                Cache.appCache.Set(CacheKeys.Site, changers?.Data?.CurrentCorpInfo?.Site);
                Cache.appCache.Set(CacheKeys.CorpName, changers?.Data?.CurrentCorpInfo?.CorpName);
                Cache.appCache.Set(CacheKeys.UserId, changers?.Data?.UserId);

                ////刷新持久化缓存
                var instance = new CacheFactory().GetInstance();
                var rs = instance.SetAppuser(new Appuser
                {
                    SeqNo = Guid.NewGuid().ToString("N"),
                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    UserId = changers?.Data?.UserId,
                    UserName = changers?.Data?.UserName,
                    UserEmail = changers?.Data?.EmailAddress,
                    CropId = changers?.Data?.CurrentCorpInfo?.CorpId,
                    CropName = changers?.Data?.CurrentCorpInfo?.CorpName,
                    CropCode = changers?.Data?.CurrentCorpInfo?.CorpSccCode,
                    Site = changers?.Data?.CurrentCorpInfo?.Site,
                    Version = Cache.appCache.Get(CacheKeys.CurrentCmsVersion) as string,
                });

                var rftk = Cache.appCache.Get<string>(CacheKeys.RefreshToken);

                using HttpClient _client = new HttpClient(); ;
                var newToken = await _client.RequestRefreshTokenAsync(new RefreshTokenRequest
                {
                    Address = $"{Cache.appCache.Get<string>(CacheKeys.Authority)}/connect/token",

                    ClientId = "SmartTools",
                    ClientSecret = "secret",
                    RefreshToken = Cache.appCache.Get<string>(CacheKeys.RefreshToken)
                });

                if (newToken.HttpResponse.IsSuccessStatusCode)
                {
                    ////更新应用缓存
                    Cache.appCache.Set(CacheKeys.Token, newToken?.AccessToken);
                    Cache.appCache.Set(CacheKeys.RefreshToken, newToken?.RefreshToken);
                    Cache.appCache.Set(CacheKeys.ExpireAt, DateTime.Now.AddSeconds((double)newToken?.ExpiresIn));

                    ////刷新持久化Token
                    var rs1 = instance.SetToken(new AppToken
                    {
                        SeqNo = Guid.NewGuid().ToString("N"),
                        CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Token = newToken?.AccessToken,
                        RefreshToken = newToken?.RefreshToken,
                        Expire = DateTime.Now.AddSeconds((double)newToken?.ExpiresIn).ToString("yyyy-MM-dd HH:mm:ss"),
                        Version = Cache.appCache.Get(CacheKeys.CurrentCmsVersion) as string,
                    });

                    _ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object>
                    {
                        data = Cache.appCache.Get<string>(CacheKeys.CorpName),
                        msgtype = MsgType.changeSite
                    });
                }

                _navigationJournal.GoBack();
                //_userCorp = Cache.appCache.Get<string>(CacheKeys.Site);
            }
            catch
            {

            }
        }
        #endregion

        #region 初始化加载
        public ICommand LoadedCommand => new DelegateCommand<object>(OnLoadedAsync);

        private void OnLoadedAsync(object obj)
        {

        }
        #endregion

        #region 获取用户绑定的工厂信息
        private async Task<List<ComboboxItem>> LoadData()
        {
            var rs = await new HttpUtil<CommonResult<List<ComboboxItem>>>(_ea).PostAsync("PlatformCenter/User/GetCurrUserCorp", new { Key = Cache.appCache.Get<string>(CacheKeys.UserId) as String });

            return rs?.Data;
        }
        #endregion
    }
}
