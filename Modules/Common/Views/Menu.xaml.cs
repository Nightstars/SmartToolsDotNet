using Prism.Regions;
using SmartSoft.SmartUI.WPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CommonModule.Views
{
    /// <summary>
    /// Menu.xaml 的交互逻辑
    /// </summary>
    public partial class Menu : UserControl
    {
        private readonly IRegionManager _regionManager;
        public Menu(IRegionManager regionManager)
        {
            InitializeComponent();
            ControlUtil.HideBoundingBox(this);
            _regionManager = regionManager;

            RegionManager.SetRegionName(user, "userRegion");
        }
    }
}
