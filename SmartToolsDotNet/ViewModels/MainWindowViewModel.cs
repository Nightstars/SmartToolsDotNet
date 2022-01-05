using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartToolsDotNet.Utils;
using SmartToolsDotNet.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SmartToolsDotNet.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region initialize
        public MainWindowViewModel(IRegionManager regionManager)
        {
            //regionManager.RegisterViewWithRegion("codelessRegion", typeof(CodeLessControl));
        }
        #endregion
    }
}
