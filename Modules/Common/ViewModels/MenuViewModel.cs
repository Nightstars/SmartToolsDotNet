using CommonModule.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartToolsDotNet.Utils;
using System;
using System.Collections.Generic;
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

        public MenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _menuItems = new List<MenuItem>
            {
                new MenuItem{ icon = "TextBoxMultiple", title = "CodeLess", ViewName = "Content"},
                new MenuItem{ icon = "CogBox", title = "设置", ViewName = "Settings"},
            };
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
            _regionManager.RequestNavigate("contentRegion", item.ViewName);
        }
        #endregion
    }
}
