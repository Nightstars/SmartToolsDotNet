using CodelessModule.Events;
using CodelessModule.Models;
using Prism.Events;
using SmartSoft.SmartUI.WPF.Common;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Controls;

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
            ControlUtil.HideBoundingBox(this);
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
