using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartToolsDotNet.Utils;
using SmartToolsDotNet.Views;
using SmartToolsDotNet.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SmartToolsDotNet.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region initialize
        private string _title = "SmartToolsDotnet-Alpha 1.0";
        private readonly IRegionManager _regionManager;

        private BitmapSource _appicon = FileUtil.GetImage("icon.icon.png");

        //关闭按钮
        public DelegateCommand CloseCommand { get; set; }

        //最小化按钮
        public DelegateCommand MinCommand { get; set; }

        //最大化按钮
        public DelegateCommand MaxCommand { get; set; }

        public string Title {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public BitmapSource AppIcon {
            get { return _appicon; }
            set { SetProperty(ref _appicon, value); }
        }

        //最大化图标设置
        private string _maxIcon = "WindowMaximize";
        public string MaxIcon {
            get { return _maxIcon; }
            set { SetProperty(ref _maxIcon, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("contentRegion", typeof(UserControl1));
            _regionManager.RegisterViewWithRegion("codelessRegion", typeof(CodeLessControl));
            _regionManager.RegisterViewWithRegion("headerRegion", typeof(Header));

            //关闭按钮逻辑
            CloseCommand = new DelegateCommand(() =>
            {
                Application.Current.Shutdown();
            });

            //最小化按钮逻辑
            MinCommand = new DelegateCommand(() =>
            {
                SystemCommands.MinimizeWindow(Application.Current.MainWindow);
            });

            //最大化按钮逻辑
            MaxCommand = new DelegateCommand(() =>
            {
                Window win = Application.Current.MainWindow;
                WindowState state = win.WindowState;

                switch (state)
                {
                    case WindowState.Normal:
                        SystemCommands.MaximizeWindow(win);
                        MaxIcon = "WindowRestore";
                        break;
                    case WindowState.Maximized:
                        SystemCommands.RestoreWindow(win);
                        MaxIcon = "WindowMaximize";
                        break;
                }
            });
        }
        #endregion
    }
}
