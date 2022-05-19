using App.Common.Enums;
using App.Common.Events;
using App.Common.Models.events;
using HandyControl.Controls;
using HandyControl.Data;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace NetAnywhereUI.ViewModels
{
    internal class NetAnywhereViewModel : BindableBase
    {
        #region 初始化
        // 1.定义委托
        public delegate void DelReadStdOutput(string result);
        public delegate void DelReadErrOutput(string result);

        // 2.定义委托事件
        public event DelReadStdOutput ReadStdOutput;
        public event DelReadErrOutput ReadErrOutput;

        private IEventAggregator _ea;
        private readonly IRegionManager _regionManager;

        public NetAnywhereViewModel(IEventAggregator ea, IRegionManager regionManager)
        {
            Init();
            _ea = ea;
            _regionManager = regionManager;
            _showState = "Visibility";
            _showLogs = "Collapsed";
        }
        #endregion

        #region init
        private void Init()
        {
            //3.将相应函数注册到委托事件中
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            ReadErrOutput += new DelReadErrOutput(ReadErrOutputAction);
        }

        #endregion

        #region 日志内容
        private string _logs;
        public string Logs
        {
            get { return _logs; }
            set { SetProperty(ref _logs, value); }
        }
        #endregion

        #region 连接文本
        private string _connectText = "未连接";
        public string ConnectText
        {
            get { return _connectText; }
            set { SetProperty(ref _connectText, value); }
        }
        #endregion

        #region 连接文本
        private string _connectTime = "00:00:00";
        public string ConnectTime
        {
            get { return _connectTime; }
            set { SetProperty(ref _connectTime, value); }
        }
        #endregion

        #region 按钮颜色
        private string _btnColor = "#db3340";
        public string BtnColor
        {
            get { return _btnColor; }
            set { SetProperty(ref _btnColor, value); }
        }
        #endregion

        #region 初始时间
        private DateTime startime = DateTime.MinValue;
        #endregion

        #region 连接状态
        //连接状态
        private bool IsConnected;
        #endregion

        #region 展示连接信息
        private string _showState;
        public string ShowState
        {
            get { return _showState; }
            set { SetProperty(ref _showState, value); }
        }
        #endregion

        #region 显示日志
        private string _showLogs;
        public string ShowLogs
        {
            get { return _showLogs; }
            set { SetProperty(ref _showLogs, value); }
        }
        #endregion

        #region RealAction
        private void RealAction(string StartFileName, string StartFileArg)
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = StartFileName;      // 命令
            CmdProcess.StartInfo.Arguments = StartFileArg;      // 参数

            CmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入
            CmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出
            CmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出
                                                                //CmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            CmdProcess.EnableRaisingEvents = true;                      // 启用Exited事件
            CmdProcess.Exited += new EventHandler(CmdProcess_Exited);   // 注册进程结束事件

            CmdProcess.Start();
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            // 如果打开注释，则以同步方式执行命令，此例子中用Exited事件异步执行。
            // CmdProcess.WaitForExit();
        }

        #endregion

        #region p_OutputDataReceived
        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                // 4. 异步调用，需要invoke
                ReadStdOutput(e.Data);
            }
        }

        #endregion

        #region p_ErrorDataReceived
        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Growl.Error(new GrowlInfo { Message = e.Data, ShowDateTime = false });
            }
        }

        #endregion

        #region ReadStdOutputAction
        private void ReadStdOutputAction(string result)
        {
            //this.textBoxShowStdRet.AppendText(result + "\r\n");
            if(result.Contains("login to server success"))
            {
                IsConnected = true;
                ConnectText = "已连接";
                Growl.Success(new GrowlInfo { Message = "连接成功", ShowDateTime = false, WaitTime = 1 });
            }
            else if(result.Contains("login to server failed"))
            {
                IsConnected = false;
                ConnectText = "连接中...";
                Growl.Error(new GrowlInfo { Message = "连接失败", ShowDateTime = false });
            }
            else if (result.Contains("try to reconnect to server..."))
            {
                IsConnected= false;
                ConnectText = "连接中...";
            }
            if (Logs == null)
                Logs += $"{result}\r\n";
            else
                Logs = Logs.Insert(0, $"{result}\r\n");

            //_ea.GetEvent<CommonEvent>().Publish(new CommoneventInfo<object> { msgtype = MsgType.logs, data = result });
        }

        #endregion

        #region ReadErrOutputAction
        private void ReadErrOutputAction(string result)
        {
            Growl.Error(new GrowlInfo { Message = result, ShowDateTime = false });
        }

        #endregion

        #region CmdProcess_Exited
        private void CmdProcess_Exited(object sender, EventArgs e)
        {
            // 执行结束后触发
        }
        #endregion

        #region 连接
        public ICommand ConnectCommand { get => new DelegateCommand<object>(OnConnect); }

        private void OnConnect(object obj)
        {
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += timer_Tick;
            // 启动进程执行相应命令,此例中以执行ping.exe为例
            if (!IsConnected)
            {
                Process.GetProcessesByName("frpc").ToList().ForEach(x => x.Kill());
                BtnColor = "#008000";
                RealAction("./apps/e6ceff885fd78a86e9ddd8a39a2310f1/frpc.exe", "-c ./apps/e6ceff885fd78a86e9ddd8a39a2310f1/frpc.ini");
                //timer.Start();
            }
            else
            {
                Process.GetProcessesByName("frpc").ToList().ForEach(x => x.Kill());
                IsConnected = false;
                ConnectText = "未连接";
                Growl.Warning(new GrowlInfo { Message = "断开连接", ShowDateTime = false, WaitTime = 1 });
                BtnColor = "#db3340";
                //timer.Stop();
                startime = DateTime.MinValue;
                ConnectTime = DateTime.MinValue.ToString("HH:mm:ss");
            }

        }
        #endregion

        #region 显示日志
        public ICommand ShowLogsCommand { get => new DelegateCommand<object>(OnShowLogs); }

        private void OnShowLogs(object obj)
        {
            //_regionManager.RequestNavigate("contentRegion", "Logs");
            ShowState = "Collapsed";
            ShowLogs = "Visibility";
        }
        #endregion

        #region 隐藏日志
        public ICommand HideLogsCommand => new DelegateCommand<object>(OnHideLogs);

        private void OnHideLogs(Object obj)
        {
            ShowState = "Visibility";
            ShowLogs = "Collapsed";
        }
        #endregion

        #region 配置
        public ICommand ConfigCommand => new DelegateCommand<object>(OnConfig);

        private void OnConfig(Object obj)
        {
            //_regionManager.RequestNavigate("contentRegion", "ConfigFrp");
            Process.Start("explorer.exe", @".\apps\e6ceff885fd78a86e9ddd8a39a2310f1\frpc.ini");
        }
        #endregion

        #region 计时器
        void timer_Tick(object sender, EventArgs e)
        {
            startime = startime.AddSeconds(1);
           ConnectTime = startime.ToString("HH:mm:ss");
        }
        #endregion
    }
}
