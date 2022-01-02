using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CodelessModule.ViewModels
{
    public class CodelessHeadViewModel : BindableBase
    {
        #region initialize
        private decimal _simpleBtnOpacity;
        public decimal SimpleBtnOpacity
        {
            get { return _simpleBtnOpacity; }
            set { SetProperty(ref _simpleBtnOpacity, value); }
        }

        private decimal _advanceBtnOpacity;
        public decimal AdvanceBtnOpacity
        {
            get { return _advanceBtnOpacity; }
            set { SetProperty(ref _advanceBtnOpacity, value); }
        }

        public ICommand SimplepageCommand { get => new DelegateCommand<Object>(OnSimpleBtnClicked); }
        public ICommand AdvancepageCommand { get => new DelegateCommand<Object>(OnAdvanceBtnClicked); }

        private readonly IRegionManager _regionManager;

        public CodelessHeadViewModel(IRegionManager regionManager)
        {
            _simpleBtnOpacity = 1;
            _advanceBtnOpacity = 0.7M;
            _regionManager = regionManager;
        }
        #endregion

        #region OnSimpleBtnClicked
        public void OnSimpleBtnClicked(Object obj)
        {
            SimpleBtnOpacity = 1;
            AdvanceBtnOpacity = 0.7M;
            _regionManager.RequestNavigate("codelessRegion", "CodeLessControl");
        }
        #endregion

        #region OnSimpleBtnClicked
        public void OnAdvanceBtnClicked(Object obj)
        {
            SimpleBtnOpacity = 0.7M;
            AdvanceBtnOpacity = 1;
            _regionManager.RequestNavigate("codelessRegion", "AdvanceCodeless");
        }
        #endregion
    }
}
