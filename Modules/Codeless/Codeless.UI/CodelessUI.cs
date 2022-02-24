using CodelessUI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace CodelessUI
{
    public class CodelessUI : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("codelessHeadRegion", typeof(CodelessHead));
            regionManager.RegisterViewWithRegion("codelessRegion", typeof(CodeLess));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AdvanceCodeless>();
            containerRegistry.RegisterForNavigation<CodeLess>();
        }
    }
}
