using App.Common.Cache;
using App.Common.Enums;
using App.Common.Events;
using App.Common.Factory;
using App.Common.Models.Cache;
using App.Common.Models.events;
using App.Common.Status;
using Microsoft.Extensions.Caching.Memory;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using User.Core.Factory;

namespace UserUI.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        #region 初始化
        private IRegionNavigationJournal _navigationJournal;
        private IEventAggregator _ea;
        public LoginViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _appServers = new CacheFactory().GetInstance().GetAppServer();

            //首次登录不存在已登录信息
            if (string.IsNullOrWhiteSpace(Cache.appCache.Get<string>(CacheKeys.CurrentCmsVersion)))
            {
                var tempServerInfo = _appServers?.FirstOrDefault();
                _appServer = tempServerInfo?.Host;

                Cache.appCache.Set(CacheKeys.CurrentCmsVersion, tempServerInfo?.Version);
                Cache.appCache.Set(CacheKeys.Authority, tempServerInfo?.Host);
                //State.CurrentCmsVersion = tempServerInfo?.Version;
                //State.Authority = tempServerInfo.Host;
            }
            else
                _appServer = _appServers?.Find(x => x.Version == Cache.appCache.Get<string>(CacheKeys.CurrentCmsVersion))?.Host;
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

        #region 登录
        public ICommand LoginCommand => new DelegateCommand<object>(OnLogout);
        private async void OnLogout(object obj)
        {
            _navigationJournal.GoBack();

            var userinfo = await new LoginUtilFactory().GetInstance().Login(_appServer, false);            

            _ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object> { msgtype = MsgType.login, data = userinfo });
        }
        #endregion

        #region 服务器信息
        private string _appServer;
        public string AppServer
        {
            get { return _appServer; }
            set { SetProperty(ref _appServer, value); }
        }

        private List<AppServer> _appServers;
        public List<AppServer> AppServers
        {
            get { return _appServers; }
            set { SetProperty(ref _appServers, value); }
        }
        #endregion

        #region 切换CMS版本
        public ICommand ChangeServerCommand => new DelegateCommand<object>(OnChangCmsVersion);

        private void OnChangCmsVersion(object obj)
        {
            Cache.appCache.Set(CacheKeys.CurrentCmsVersion, (obj as AppServer)?.Version);
            Cache.appCache.Set(CacheKeys.Authority,(obj as AppServer)?.Host);
        }
        #endregion
    }
}
