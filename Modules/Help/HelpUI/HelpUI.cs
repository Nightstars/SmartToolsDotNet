using HelpUI.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using SmartSoft.SmartUI.WPF.Common.Menu;
using System;

namespace HelpUI
{
    public class HelpUI : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //ע��˵�
            MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "Help", title:"ʹ�ð���",weight: 0, icon: "HelpRhombus");
            //MenuRegistry.RegisterForMenu(containerProvider.Resolve<IEventAggregator>(), "https://blog.smartcloud.fun:3/doc-help-SmartToolsDotnet", title:"ʹ�ð���",weight: 0, icon: "HelpRhombus",type:MenuType.Url);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Help>();
        }
    }
}
