﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartToolsDotNet.Views;
using SmartToolsDotNet.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartToolsDotNet.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region initialize
        private string _title = "SmartToolsDotnet";
        private readonly IRegionManager _regionManager;

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

        //最大化图标设置
        private string _icon = "WindowMaximize";
        public string Icon {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("contentRegion", typeof(UserControl1));
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
                        Icon = "WindowRestore";
                        break;
                    case WindowState.Maximized:
                        SystemCommands.RestoreWindow(win);
                        Icon = "WindowMaximize";
                        break;
                }
            });
        }
        #endregion
    }
}
