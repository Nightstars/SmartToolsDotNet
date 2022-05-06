using HomeUI.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SmartSoft.SmartUI.WPF.Common.Menu;
using System;

namespace HomeUI
{
    public class HomeUI : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("contentRegion", typeof(Home));

            //注册菜单
            MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "Home", title: "首页", icon: "Home", weight: 999);

            //跳转首页
            regionManager.RequestNavigate("contentRegion", "Home");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Home>();
        }
    }
}
