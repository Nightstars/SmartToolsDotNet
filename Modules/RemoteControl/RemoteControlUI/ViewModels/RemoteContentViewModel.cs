using App.Common.Enums;
using App.Common.Events;
using App.Common.Models.events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace RemoteControlUI.ViewModels
{
    public class RemoteContentViewModel : BindableBase
    {
        #region 初始化
        private readonly IEventAggregator _ea;
        public RemoteContentViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }
        #endregion

        #region 全屏
        public ICommand FullScreenCommand { get => new DelegateCommand<object>(OnFullScreen); }

        private void OnFullScreen(object obj)
        {
            _ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object> { msgtype = MsgType.rdp, data = 0 });
        }
        #endregion

        #region 断开连接
        public ICommand DistinctCommand { get => new DelegateCommand<object>(OnDistinct); }

        private void OnDistinct(object obj)
        {
            _ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object> { msgtype = MsgType.rdp, data = 1 });
        }
        #endregion


    }
}
