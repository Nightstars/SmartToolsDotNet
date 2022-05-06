using Demo.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using SmartSoft.SmartUI.WPF.Common.Menu;
using System;

namespace Demo
{
    public class Demo : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //×¢²á²Ëµ¥
            //MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "DemoPage");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DemoPage>();


        }
    }
}
