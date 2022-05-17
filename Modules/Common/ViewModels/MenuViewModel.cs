using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SmartSoft.SmartUI.WPF.Common.Menu;
using SmartSoft.SmartUI.WPF.Events;
using SmartToolsDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CommonModule.ViewModels
{
    internal class MenuViewModel : BindableBase
    {
        #region initialize
        public BitmapSource AppIcon
        {
            get { return _appicon; }
            set { SetProperty(ref _appicon, value); }
        }
        private BitmapSource _appicon = FileUtil.GetImage("icon.icon.png");

        private IRegionManager _regionManager;

        private IEventAggregator _ea;

        public MenuViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;

            _ea = ea;

            _ea.GetEvent<MenuEvent>().Subscribe(LoadMenu);

            //_menuItems = new List<MenuItem>
            //{
            //    new MenuItem{ icon = "TextBoxMultiple", title = "CodeLess", ViewName = "Content"},
            //    new MenuItem{ icon = "TextBoxMultiple", title = "demo", ViewName = "DemoPage"},
            //    new MenuItem{ icon = "CogBox", title = "设置", ViewName = "Settings"},
            //};
        }
        #endregion

        #region 版本号
        private string _appVersion = $"Beta {FileVersionInfo.GetVersionInfo(@"./SmartTools.exe")?.FileVersion ?? "1.0.0.0"}";
        public string AppVersion {
            get { return _appVersion; }
            set { SetProperty(ref _appVersion, value); }
        }
        #endregion

        #region 菜单
        private List<MenuItem> _menuItems;
        public List<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }
        #endregion

        #region 菜单切换
        public ICommand SwitchMenuCommand => new DelegateCommand<MenuItem>(SwitchMenu);

        private void SwitchMenu(MenuItem item)
        {
            switch (item.type)
            {
                case MenuType.Menu:
                    _regionManager.RequestNavigate("contentRegion", item.ViewName);
                    break;
                case MenuType.Url:
                    System.Diagnostics.Process.Start("explorer.exe", item.ViewName);
                    break;
                default:
                    break;
            }
            _regionManager.RequestNavigate("contentRegion", item.ViewName);
        }
        #endregion

        #region 加载菜单
        private void LoadMenu(MenuItem menuItem)
        {
            var menus = new List<MenuItem>();
            if(_menuItems?.Count > 0)
                menus.AddRange(_menuItems);
            menus.Add(menuItem);
            MenuItems = menus.OrderByDescending(x => x.Weight).ToList();

        }
        #endregion
    }
}
