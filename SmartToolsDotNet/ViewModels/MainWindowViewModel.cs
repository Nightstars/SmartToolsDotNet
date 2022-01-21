using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace SmartToolsDotNet.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region initialize
        public MainWindowViewModel(IRegionManager regionManager)
        {

        }
        #endregion

        #region 菜单切换
        public ICommand SwitchMenuCommand => new DelegateCommand<object>(SwitchMenu);

        private void SwitchMenu(object obj)
        {
            var temp = obj;
        }
        #endregion
    }
}
