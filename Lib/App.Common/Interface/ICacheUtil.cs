using App.Common.Models.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Interface
{
    public interface ICacheUtil
    {
        public AppToken GetToken();

        public bool SetToken(AppToken token);

        public Appuser GetAppuser();

        public bool SetAppuser(Appuser user);

        public List<AppServer> GetAppServer();

        //删除登录缓存
        public bool ClearLoginCache();

        //获取下拉数据源
        public List<AppComboDatasource> GetComboDatasources();
    }
}
