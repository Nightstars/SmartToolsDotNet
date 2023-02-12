using FansControlPanelUI.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using SmartSoft.SmartUI.WPF.Common.Menu;
using System;

namespace FansControlPanelUI
{
    public class FansControlPanelUI : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //注册菜单
            MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "FansControlPanel", title: "风扇控制面板", icon: "CarCruiseControl", weight: 995);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FansControlPanel>();
        }
    }
}
