using Prism.Regions;
using SmartSoft.SmartUI.WPF.Common;
using System.Windows.Input;

namespace SmartToolsDotNet.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        #region initialize
        private readonly IRegionManager _regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            _regionManager = regionManager;
            ControlUtil.HideBoundingBox(this);

            RegionManager.SetRegionName(menu, "menuRegion");
            RegionManager.SetRegionName(header, "headerRegion");
            RegionManager.SetRegionName(content, "contentRegion");

        }
        #endregion

        #region NavBar_MouseLeftButtonDown
        private void NavBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

    }
}
