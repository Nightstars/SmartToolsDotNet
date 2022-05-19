using App.Common.Enums;
using App.Common.Events;
using App.Common.Models.events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetAnywhereUI.ViewModels
{
    internal class ConfigFrpViewModel : BindableBase, INavigationAware
    {
        #region 初始化
        private readonly IEventAggregator _ea;
        private IRegionNavigationJournal _navigationJournal;
        public ConfigFrpViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<CommonEvent>().Subscribe(OnReceiveLog, ThreadOption.PublisherThread, false, filter => filter.msgtype == MsgType.logs);
        }
        #endregion

        #region 控制台内容
        private string _logs;
        public string Logs
        {
            get { return _logs; }
            set { SetProperty(ref _logs, value); }
        }
        #endregion

        #region OnReceiveLog
        public void OnReceiveLog(object obj)
        {
            var rs = obj as CommoneventInfo<object>;

            if (Logs == null)
                Logs += $"{rs?.data}\r\n";
            else
                Logs = Logs.Insert(0, $"{rs?.data}\r\n");
        }
        #endregion

        #region 导航
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationJournal = navigationContext.NavigationService.Journal;
        }
        #endregion

        #region 返回
        public ICommand GoBackCommand => new DelegateCommand<object>(OnGoBack);

        private void OnGoBack(Object obj)
        {
            _navigationJournal.GoBack();
        }
        #endregion
    }
}
