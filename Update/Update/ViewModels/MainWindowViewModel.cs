using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        #region 初始化
        private readonly IEventAggregator _ea;
        public MainWindowViewModel(IEventAggregator ea)
        {
            _ea = ea;
            string[] args = Environment.GetCommandLineArgs();
            _arg = args.Length >= 2 ? args[1] : "";
            Title = _arg;

        }
        #endregion

        #region 命令行参数
        private string _arg;
        #endregion

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
