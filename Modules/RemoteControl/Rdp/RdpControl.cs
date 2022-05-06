using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDP
{
    public partial class RdpControl : UserControl
    {
        public RdpControl()
        {
            InitializeComponent();
        }

        public void Connect(int rdpwidth,int rdpheight)
        {
            //int rdpwidth = 1920, rdpheight = 1080;

            _rdpClient.AdvancedSettings9.AuthenticationLevel = 2;
            _rdpClient.AdvancedSettings9.EnableCredSspSupport = true;
            _rdpClient.AdvancedSettings9.RedirectDrives = false;
            _rdpClient.AdvancedSettings9.RedirectPrinters = false;
            _rdpClient.AdvancedSettings9.RedirectPrinters = false;
            _rdpClient.AdvancedSettings9.RedirectSmartCards = false;

            _rdpClient.ColorDepth = 24; // int value can be 8, 15, 16, or 24
            _rdpClient.DesktopWidth = rdpwidth; // int value
            _rdpClient.DesktopHeight = rdpheight; // int value
            _rdpClient.Width = rdpwidth;
            _rdpClient.Height = rdpheight;

            _rdpClient.Server = "127.0.0.1";
            _rdpClient.AdvancedSettings9.RDPPort = 11002;

            _rdpClient.UserName = "Administrator";
            _rdpClient.AdvancedSettings9.ClearTextPassword = "Ihavenoidea#@!0";

            _rdpClient.Connect();
        }

        public void FullScreen()
        {

        }

        public void Disconnect()
        {
            _rdpClient.Disconnect();
        }
    }
}
