using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace RemoteControlUI.ViewModels
{
    internal class RemoteViewModel : BindableBase
    {

        #region 初始化
        private IEventAggregator _ea;
        public RemoteViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }
        #endregion

        #region 连接远程
        public ICommand ConnectCommand { get => new DelegateCommand<object>(OnConnect); }

        private void OnConnect(object obj)
        {
            RemoteContent remote = new RemoteContent(_ea);
            remote.Title = "远程连接";
            remote.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            remote.Width = Convert.ToInt32(Math.Ceiling(Screen.PrimaryScreen.Bounds.Width * 0.8));
            remote.Height = Convert.ToInt32(Math.Ceiling(Screen.PrimaryScreen.Bounds.Height * 0.8));

            remote.Show();
        }
        #endregion
    }
}
