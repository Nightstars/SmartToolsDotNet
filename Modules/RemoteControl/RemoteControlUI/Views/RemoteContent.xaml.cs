using App.Common.Enums;
using App.Common.Events;
using Prism.Events;
using RDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RemoteControlUI
{
    /// <summary>
    /// RemoteContent.xaml 的交互逻辑
    /// </summary>
    public partial class RemoteContent : Window
    {
        #region 初始化
        private readonly IEventAggregator _ea;
        private RdpControl _rdp;
        public RemoteContent(IEventAggregator ea)
        {
            InitializeComponent();
            _ea = ea;
            _ea.GetEvent<CommonEvent>().Subscribe(OnRdpOperation, ThreadOption.PublisherThread, false, filter => filter.msgtype == MsgType.rdp);
            _rdp = new RdpControl();
            host.Child = _rdp;
            _rdp.Connect(Convert.ToInt32(Math.Floor((Screen.PrimaryScreen.Bounds.Width -20)* 0.8)), Convert.ToInt32(Math.Floor((Screen.PrimaryScreen.Bounds.Height - 88+40) * 0.8)));
        }
        #endregion

        #region OnRdpOperation
        public void OnRdpOperation(object obj)
        {
            switch (Convert.ToInt32(obj))
            {
                case 0:
                    host.Child = null;
                    _rdp.Disconnect();
                    _rdp = new RdpControl();
                    host.Child = _rdp;
                    _rdp.Connect(Convert.ToInt32(Math.Floor((Screen.PrimaryScreen.Bounds.Width - 0) * 0.8)), Convert.ToInt32(Math.Floor((Screen.PrimaryScreen.Bounds.Height - 90) * 0.8)));
                    break;
                case 1:
                    _rdp.Disconnect();
                    host.Child = null;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
