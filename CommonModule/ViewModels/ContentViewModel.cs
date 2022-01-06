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

        public ICommand SwitchCommand  => new DelegateCommand<object>(OnSwitch);

        private void OnSwitch(object obj)
        {
            System.Windows.MessageBox.Show(obj.ToString());
        }
    }
}
