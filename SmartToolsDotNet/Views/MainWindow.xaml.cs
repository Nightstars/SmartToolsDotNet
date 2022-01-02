using Prism.Regions;
using SmartToolsDotNet.Utils;
using System.Windows;
using System.Windows.Input;

namespace SmartToolsDotNet.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region initialize
        private readonly IRegionManager _regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            _regionManager = regionManager;
            new ControlUtil().HideBoundingBox(this);

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
