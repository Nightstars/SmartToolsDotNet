using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommonModule.ViewModels
{
    public class HeaderViewModel : BindableBase
    {
        #region initialize
        private string _title = "SmartToolsDotnet-Alpha 1.0";

        //private BitmapSource _appicon = FileUtil.GetImage("icon.icon.png");

        //关闭按钮
        public ICommand CloseCommand { get => new DelegateCommand<Object>(OnClose); }

        //最小化按钮
        public ICommand MinCommand { get => new DelegateCommand<Object>(OnMin); }

        //最大化按钮
        public ICommand MaxCommand { get => new DelegateCommand<Object>(OnMax); }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        //public BitmapSource AppIcon
        //{
        //    get { return _appicon; }
        //    set { SetProperty(ref _appicon, value); }
        //}

        //最大化图标设置
        private string _maxIcon = "WindowMaximize";
        public string MaxIcon
        {
            get { return _maxIcon; }
            set { SetProperty(ref _maxIcon, value); }
        }

        public HeaderViewModel()
        {

        }
        #endregion

        #region 关闭按钮逻辑
        /// <summary>
        /// OnClose
        /// </summary>
        /// <param name="obj"></param>
        public void OnClose(Object obj)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region 最小化按钮逻辑
        /// <summary>
        /// OnMin
        /// </summary>
        /// <param name="obj"></param>
        public void OnMin(Object obj)
        {
            SystemCommands.MinimizeWindow(Application.Current.MainWindow);
        }
        #endregion

        #region 最大化按钮逻辑
        public void OnMax(Object obj)
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
        }
        #endregion
    }
}
