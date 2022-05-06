using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using RemoteControlUI.Views;
using SmartSoft.SmartUI.WPF.Common.Menu;
using System;

namespace RemoteControlUI
{
    public class RemoteControlUI : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //ע��˵�
            MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "Remote", title: "Զ�̿���", icon: "RemoteDesktop", weight: 997);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Remote>();
        }
    }
}
