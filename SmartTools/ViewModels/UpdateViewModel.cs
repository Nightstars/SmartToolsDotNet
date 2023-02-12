using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.ViewModels
{
    internal class UpdateViewModel : BindableBase
    {
        #region 初始化
        private readonly IEventAggregator _ea;
        #endregion
        public UpdateViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }
    }
}
