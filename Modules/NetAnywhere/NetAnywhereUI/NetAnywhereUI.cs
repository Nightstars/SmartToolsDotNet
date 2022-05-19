using NetAnywhereUI.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using SmartSoft.SmartUI.WPF.Common.Menu;
using System;

namespace NetAnywhereUI
{
    public class NetAnywhereUI : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //ע��˵�
            MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "NetAnywhere", title: "������͸", icon: "Earth", weight: 996);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NetAnywhere>();
            containerRegistry.RegisterForNavigation<ConfigFrp>();
        }
    }
}
