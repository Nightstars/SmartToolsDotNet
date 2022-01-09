using HandyControl.Controls;
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
    /// Content.xaml 的交互逻辑
    /// </summary>
    public partial class Content : UserControl
    {
        public Content()
        {
            InitializeComponent();
            ControlUtil.HideBoundingBox(this);
            RegionManager.SetRegionName(codelessHead, "codelessHeadRegion");
            RegionManager.SetRegionName(codeless, "codelessRegion");
        }

        private void cmb_SelectionChanged(object sender, HandyControl.Data.FunctionEventArgs<object> e)
        {
            var item = e?.Info as SideMenuItem;

            //item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2786E4"));

            //System.Windows.MessageBox.Show((e?.Info as SideMenuItem)?.Header?.ToString());
        }
    }
}
