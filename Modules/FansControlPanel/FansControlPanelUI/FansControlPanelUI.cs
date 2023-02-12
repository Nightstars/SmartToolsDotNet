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
            //ע��˵�
            MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "FansControlPanel", title: "���ȿ������", icon: "CarCruiseControl", weight: 995);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FansControlPanel>();
        }
    }
}
