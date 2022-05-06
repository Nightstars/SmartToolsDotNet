using App.Common.Enums;
using App.Common.Events;
using App.Common.Factory;
using App.Common.Models.events;
using App.Common.Utils;
using HandyControl.Controls;
using HandyControl.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SmartSoft.SmartUI.WPF.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SettingsUI.ViewModels
{
    public class SettingsViewModel : BindableBase, IRegionMemberLifetime
    {
        #region 初始化
        private readonly IEventAggregator _ea;
        public SettingsViewModel(IEventAggregator ea)
        {
            _ea = ea;
            Init();
        }
        #endregion

        #region cachesize
        private string _cachesize;
        public string Cachesize
        {
            get { return _cachesize; }
            set { SetProperty(ref _cachesize, value); }
        }

        public bool KeepAlive => false;

        #endregion

        #region buildPath
        private string _buildPath;
        public string BuildPath
        {
            get { return _buildPath; }
            set { SetProperty(ref _buildPath, value); }
        }
        #endregion

        #region 初始化数据
        public void Init()
        {
            if (Directory.Exists(@"./Oupput"))
            {
                long cachebyte = 0;
                FileUtil.GetDirSizeByPath("./Oupput", ref cachebyte);
                //FileUtil.GetDirSizeByPath("./SmartToolsDotNet.exe.WebView2", ref cachebyte);
                long cacheKb = cachebyte / 1024;
                long cacheMb = cacheKb / 1024;
                Cachesize = cacheMb == 0 ? $"{cacheKb} KB" : $"{cacheMb} MB";

                _buildPath = new DirectoryInfo(@"./Oupput")?.FullName;
            }
        }
        #endregion

        #region 清除缓存
        public ICommand ClearCacheCommand => new DelegateCommand<object>(OnClearCache);

        private void OnClearCache(object obj)
        {
            try
            {
                if (Directory.Exists("./Oupput"))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo("./Oupput");

                    DirectoryInfo[] dirs = dirInfo.GetDirectories();
                    FileInfo[] files = dirInfo.GetFiles();

                    foreach (var item in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(item.FullName);
                        di.Delete(true);
                    }

                    foreach (var item in files)
                    {
                        File.Delete(item.FullName);
                    }
                }
                Init();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 打开文件夹
        public ICommand OpenFileCommand => new DelegateCommand<object>(OnOpenFile);

        private void OnOpenFile(object obj)
        {
            if (Directory.Exists("./Oupput/"))
                FileUtil.OpenFileByDefaultApp($@"{AppDomain.CurrentDomain.BaseDirectory}Oupput\");
        }
        #endregion

        #region 清除登录缓存
        public ICommand ClearLoginCacheCommand => new DelegateCommand<object>(OnClearLoginCache);

        private void OnClearLoginCache(object obj)
        {
            var rs = new CacheFactory().GetInstance().ClearLoginCache();
            if(rs)
            {
                _ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object> { msgtype = MsgType.clearLoginCache, data = "logout" });
                Growl.Success(new GrowlInfo { Message = "登录信息已清除", WaitTime = 1, ShowDateTime = false });
            }
        }
        #endregion
    }
}
