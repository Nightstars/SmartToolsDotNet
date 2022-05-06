using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using SettingsUI.Views;
using SmartSoft.SmartUI.WPF.Common.Menu;
using System;

namespace SettingsUI
{
    public class SettingsUI : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //◊¢≤·≤Àµ•
            MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "Settings","…Ë÷√", "CogBox", -1);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Settings>();
        }
    }
}
