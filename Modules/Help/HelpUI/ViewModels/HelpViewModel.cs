using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpUI.ViewModels
{
    public class HelpViewModel : BindableBase, IRegionMemberLifetime
    {
        #region Url地址
        private string _url = "https://blog.smartcloud.fun:3/doc-help-SmartToolsDotnet";
        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }

        public bool KeepAlive => false;
        #endregion
    }
}
