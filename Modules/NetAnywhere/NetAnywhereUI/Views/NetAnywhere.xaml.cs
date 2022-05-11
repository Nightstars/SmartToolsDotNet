using App.Common.Enums;
using App.Common.Events;
using Prism.Events;
using SmartSoft.SmartUI.WPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetAnywhereUI.Views
{
    /// <summary>
    /// NetAnywhere.xaml 的交互逻辑
    /// </summary>
    public partial class NetAnywhere : UserControl
    {
        #region 初始化
        private IEventAggregator _ea;
        public NetAnywhere(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<CommonEvent>().Subscribe(OnLoadLog, ThreadOption.PublisherThread, false, (filter) => filter.msgtype == MsgType.loadlog);
            InitializeComponent();
            ControlUtil.HideBoundingBox(this);
        }
        #endregion

        #region OnLoadLog
        private void OnLoadLog(object obj)
        {
            this.logs.ScrollToEnd();
        }
        #endregion
    }
}
