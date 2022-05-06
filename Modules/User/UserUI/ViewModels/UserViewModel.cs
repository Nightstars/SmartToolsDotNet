using App.Common.Cache;
using App.Common.Db;
using App.Common.Enums;
using App.Common.Events;
using App.Common.Factory;
using App.Common.Interface;
using App.Common.Models.events;
using App.Common.Status;
using CodeleSmartSoft.SmartUI.WPF.Controls;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.Caching.Memory;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using User.Core.Factory;
using User.Core.Models;
using UserUI.Utils;
using ResourceUtil = UserUI.Utils.ResourceUtil;

namespace UserUI.ViewModels
{
    public class UserViewModel : BindableBase
    {
        #region 初始化
        private readonly ICacheUtil _cache;
        private IRegionManager _regionManager;
        private IEventAggregator _ea;
        public UserViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _cache = new CacheFactory().GetInstance();
            var appuser = _cache.GetAppuser();
            if (appuser != null)
            {
                _title = $"{appuser.UserName} 【{appuser.Version}】";
                _site = appuser.CropName;
                IsLogin = true;
                _showUserName = "Visible";

                //初始化状态
                Cache.appCache.Set(CacheKeys.CurrentCmsVersion, appuser.Version);
                //State.CurrentCmsVersion = appuser.Version;
                //State.UserName = appuser.UserName;
                //State.Site = appuser.Site;
                //State.CorpName = appuser.CropName;

                var token = new CacheFactory().GetInstance().GetToken();
                //State.Token = token?.Token;
                //State.RefreshToken = token?.RefreshToken;
                //State.ExpireAt = DateTime.Parse(token?.Expire);

                var server = new CacheFactory().GetInstance().GetAppServer()?.Find(x => x.Version == appuser.Version);

                //State.Authority = server?.Host;

                Cache.appCache.Set(CacheKeys.UserId, appuser.UserId);
                Cache.appCache.Set(CacheKeys.UserName, appuser.UserName);
                Cache.appCache.Set(CacheKeys.Site, appuser.Site);
                Cache.appCache.Set(CacheKeys.CorpName, appuser.CropName);
                Cache.appCache.Set(CacheKeys.Token, token?.Token);
                Cache.appCache.Set(CacheKeys.RefreshToken, token?.RefreshToken);
                Cache.appCache.Set(CacheKeys.ExpireAt, DateTime.Parse(token?.Expire));
                Cache.appCache.Set(CacheKeys.Authority, server?.Host);

                //var testversion = Cache.appCache.Get("CurrentCmsVersion");
            }
            _regionManager = regionManager;

            _ea = ea;

            //订阅登录事件
            _ea.GetEvent<CommonEvent>().Subscribe(OnLoginAsync, ThreadOption.PublisherThread,false, (filter) => filter.msgtype == MsgType.login);
            //订阅注销事件
            _ea.GetEvent<CommonEvent>().Subscribe(OnLogoutAsync, ThreadOption.PublisherThread, false, (filter) => filter.msgtype == MsgType.logout);
            //订阅切换工厂事件
            _ea.GetEvent<CommonEvent>().Subscribe(x => Site = x.data as string, ThreadOption.PublisherThread, false, (filter) => filter.msgtype == MsgType.changeSite);

            //订阅清除登录信息事件
            _ea.GetEvent<CommonEvent>().Subscribe(OnClearLoginCache, ThreadOption.PublisherThread, false, (filter) => filter.msgtype == MsgType.clearLoginCache);

            //订阅清除登录失效事件
            _ea.GetEvent<CommonEvent>().Subscribe(OnLoginExpired, ThreadOption.PublisherThread, false, (filter) => filter.msgtype == MsgType.loginExpired);
        }
        #endregion

        #region 用户头像
        private BitmapSource _appicon = ResourceUtil.GetImage("icon.user.png");

        public BitmapSource AppIcon
        {
            get { return _appicon; }
            set { SetProperty(ref _appicon, value); }
        }
        #endregion

        #region 基本操作
        public ICommand LoginCommand => new DelegateCommand<object>(OnLogin);

        private async void OnLogin(object obj)
        {
            if (!IsLogin)
                _regionManager.RequestNavigate("contentRegion", "Login");
            else
                _regionManager.RequestNavigate("contentRegion", "Profile");
        }
        #endregion

        #region 是否显示用户名
        private string _showUserName = "Collapsed";

        public string ShowUserName
        {
            get { return _showUserName; }
            set { SetProperty(ref _showUserName, value); }
        }
        #endregion

        #region 是否显示登陆按钮
        private string _showLogin = "Visible";

        public string ShowLogin
        {
            get { return _showLogin; }
            set { SetProperty(ref _showLogin, value); }
        }
        #endregion

        #region 用户名
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        #endregion

        #region 工厂
        private string _site;

        public string Site
        {
            get { return _site; }
            set { SetProperty(ref _site, value); }
        }
        #endregion

        #region 按钮文本
        private string _title = "立即登录";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion

        #region 是否已登录
        private bool IsLogin = false;
        #endregion

        #region 注销登录
        public void OnLogoutAsync(CommoneventInfo<object> info)
        {
            Dialog? logading = Dialog.Show(new Loading());

            var userinfo = info.data as AppUser;

            if (userinfo != null && !userinfo.IsError)
            {
                if (!IsLogin)
                {
                    ShowUserName = "Visible";
                    ShowLogin = "Collapsed";
                    UserName = userinfo.UserName;
                    Title = userinfo.UserName;
                    Site = userinfo.CurrentCorpInfo?.CorpName;
                }
                else
                {
                    ShowUserName = "Collapsed";
                    Title = "立即登录";
                }

                IsLogin = !IsLogin;
                logading.Close();
                Growl.Success(new GrowlInfo { Message = IsLogin ? "登录成功" : "已注销", WaitTime = 1, ShowDateTime = false });
            }
            else
            {
                Growl.Error(new GrowlInfo { Message = userinfo?.ErrorMessage, ShowDateTime = false });
                logading.Close();
            }
        }
        #endregion

        #region 用户登录
        public void OnLoginAsync(CommoneventInfo<object> info)
        {
            Dialog? logading = Dialog.Show(new Loading());

            var userinfo = info.data as AppUser;

            if (userinfo != null && !userinfo.IsError)
            {
                if (!IsLogin)
                {
                    ShowUserName = "Visible";
                    ShowLogin = "Collapsed";
                    UserName = userinfo.UserName;
                    Title = $"{userinfo.UserName} 【{Cache.appCache.Get<string>(CacheKeys.CurrentCmsVersion)}】";
                    Site = userinfo.CurrentCorpInfo?.CorpName;
                }
                else
                {
                    ShowUserName = "Collapsed";
                    Title = "立即登录";
                }

                IsLogin = !IsLogin;
                logading.Close();
                Growl.Success(new GrowlInfo { Message = IsLogin ? "登录成功" : "已注销", WaitTime = 1, ShowDateTime = false });
            }
            else
            {
                Growl.Error(new GrowlInfo { Message = userinfo?.ErrorMessage, ShowDateTime = false });
                logading.Close();
            }
        }
        #endregion

        #region 清除登录信息
        private void OnClearLoginCache(object obj)
        {
            ShowUserName = "Collapsed";
            Title = "立即登录";
            IsLogin = false;
        }
        #endregion

        #region 登录失效

        private void OnLoginExpired(object obj)
        {
            Growl.Warning(new GrowlInfo { Message = IsLogin ? "登录已失效，请重新登录" : "请先登录", WaitTime = 5, ShowDateTime = false });
            IsLogin = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                _regionManager.RequestNavigate("contentRegion", "Login");
            });
        }
        #endregion
    }
}
