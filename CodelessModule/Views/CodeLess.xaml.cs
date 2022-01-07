using CodelessModule.CustomControls;
using CodelessModule.Events;
using CodelessModule.Models;
using CodelessModule.Services;
using CodelessModule.Utils;
using CodelessModule.ViewModels;
using HandyControl.Controls;
using MaterialDesignThemes.Wpf;
using Prism.Events;
using SmartSoft.common.Utils.solution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodelessModule.Views
{
    /// <summary>
    /// CodeLess.xaml 的交互逻辑
    /// </summary>
    [SupportedOSPlatform("windows7.0")]
    public partial class CodeLess : UserControl
    {
        #region initialize
        private string _currentLan = string.Empty;

        private IEventAggregator _ea;
        public CodeLess(IEventAggregator ea)
        {
            InitializeComponent();
            _currentLan = "zh-cn";
            _ea = ea;
        }
        #endregion

        #region CheckComboBox_SelectionChanged
        /// <summary>
        /// CheckComboBox_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = searchparam.SelectedItems.Cast<DbTableInfo>().ToList();
            _ea.GetEvent<SearchParamChangedEvent>().Publish(list);
        }
        #endregion

    }
}
