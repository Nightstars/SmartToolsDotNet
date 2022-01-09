using HandyControl.Controls;
using HandyControl.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SmartSoft.SmartUI.WPF.Common.Message;
using SmartSoft.SmartUI.WPF.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommonModule.ViewModels
{
    public class ContentViewModel : BindableBase
    {

        #region initialize
        public ContentViewModel(IEventAggregator ea)
        {
            //ea.GetEvent<ShowMessageEvent>().Subscribe(OnReceive);
        }
        #endregion


        #region OnReceive
        public void OnReceive(Message msg)
        {
            Growl.Success("Success Message");
        }
        #endregion
        public ICommand SwitchCommand  => new DelegateCommand<object>(OnSwitch);

        private void OnSwitch(object obj)
        {
            System.Windows.MessageBox.Show(obj.ToString());
        }
    }
}
