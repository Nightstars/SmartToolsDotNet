using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using SmartToolsDotNet.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
            //clearCache();
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

        #region CreateContainerExtension
        protected override IContainerExtension CreateContainerExtension()
        {
            IServiceCollection servicecollection = new ServiceCollection();

            return new DryIocContainerExtension(new Container(CreateContainerRules()).WithDependencyInjectionAdapter(servicecollection));
        }
        #endregion

        #region CreateModuleCatalog
        protected override IModuleCatalog CreateModuleCatalog()
        {
            IModuleCatalog moduleCatalog = new DirectoryModuleCatalog { ModulePath = @".\Paks"};
            return moduleCatalog;
        }
        #endregion

        #region clearCache
        private void clearCache()
        {
            try
            {
                if (Directory.Exists("./SmartToolsDotNet.exe.WebView2"))
                {

                    DirectoryInfo di = new DirectoryInfo("./SmartToolsDotNet.exe.WebView2");
                    di.Delete(true);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region OnExit
        protected override void OnExit(ExitEventArgs e)
        {
            //clearCache();
            base.OnExit(e);
        }
        #endregion
    }
}
