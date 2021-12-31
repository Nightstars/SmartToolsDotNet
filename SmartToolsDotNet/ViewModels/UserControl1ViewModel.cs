using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartToolsDotNet.ViewModels
{
    internal class UserControl1ViewModel : BindableBase
    {
        #region initialzie
        public DelegateCommand TestCommond { get; set; }

        public UserControl1ViewModel()
        {
            TestCommond = new DelegateCommand(() =>
            {
                MessageBox.Show("test");
            });
        }
        #endregion
    }
}
