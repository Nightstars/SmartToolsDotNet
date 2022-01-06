using HandyControl.Controls;
using Prism.Commands;
using Prism.Mvvm;
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
        //public RelayCommand<FunctionEventArgs<object>> SwitchItemCmd => new Lazy<RelayCommand<FunctionEventArgs<object>>>(() =>
        //    new RelayCommand<FunctionEventArgs<object>>(SwitchItem)).Value;

        //private void SwitchItem(FunctionEventArgs<object> info) => Growl.Info((info.Info as SideMenuItem)?.Header.ToString(), "InfoMessage");

        //public RelayCommand<string> SelectCmd => new Lazy<RelayCommand<string>>(() =>
        //    new RelayCommand<string>(Select)).Value;

        //private void Select(string header) => Growl.Success(header, "InfoMessage");

        public ICommand SwitchCommand  => new DelegateCommand<object>(OnSwitch);

        private void OnSwitch(object obj)
        {
            System.Windows.MessageBox.Show(obj.ToString());
        }
    }
}
