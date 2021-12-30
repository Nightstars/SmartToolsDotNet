using Prism.Mvvm;
using Prism.Regions;
using SmartToolsDotNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartToolsDotNet.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region initialize
        private string _title = "SmartToolsDotnet";
        private readonly IRegionManager _regionManager;

        public string Title {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("contentRegion", typeof(UserControl1));
            _regionManager.RegisterViewWithRegion("headerRegion", typeof(Header));
        }
        #endregion
    }
}
