using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeUI.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        #region 初始化
        public HomeViewModel()
        {
            _mages = new List<string>
            {
                "https://www.yulumi.cn/gl/uploads/allimg/201128/1622554105-6.jpg",
                "https://scpic.chinaz.net/files/pic/pic9/201709/bpic3269.jpg",
                "https://tse2-mm.cn.bing.net/th/id/OIP-C.4hVvk_1NpgqefdiADFF44AHaE7?pid=ImgDet&rs=1"
            };
        }
        #endregion

        #region 轮播图
        private List<string> _mages;
        public List<string> Images
        {
            get { return _mages; }
            set { SetProperty(ref _mages, value); }
        }
        #endregion
    }
}
