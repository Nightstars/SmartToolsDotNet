using CommonModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace CommonModule
{
    public class CommonModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("headerRegion", typeof(Header));
            regionManager.RegisterViewWithRegion("contentRegion", typeof(Content));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
