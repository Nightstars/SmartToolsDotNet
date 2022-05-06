using CommonModule.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SmartSoft.SmartUI.WPF.Common.Menu;
using System;

namespace CommonModule
{
    public class AppCommonModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("menuRegion", typeof(Menu));
            regionManager.RegisterViewWithRegion("headerRegion", typeof(Header));
            //regionManager.RegisterViewWithRegion("contentRegion", typeof(Content));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Content>();
        }
    }
}
