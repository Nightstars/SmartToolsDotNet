using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;
using SmartToolsDotNet.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SmartToolsDotNet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        #region CreateShell
        /// <summary>
        /// CreateShell
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        #endregion

        #region RegisterTypes
        /// <summary>
        /// RegisterTypes
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //ViewModelLocationProvider.Register<Header, HeaderViewModel>();
        }
        #endregion

    }
}
