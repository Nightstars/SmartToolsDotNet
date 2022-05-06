using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using UserUI.Views;

namespace UserUI
{
    public class UserUI : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("userRegion", typeof(UserView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Login>();
            containerRegistry.RegisterForNavigation<Profile>();
        }
    }
}
