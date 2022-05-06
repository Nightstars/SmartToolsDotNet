using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public NetAnywhereViewModel()
        {
            Init();
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

        #region 控制台内容
        private string _logs;
        public string Logs
        {
            get { return _logs; }
            set { SetProperty(ref _logs, value); }
        }
        #endregion

        #region 连接文本
        private string _connectText = "连接";
        public string ConnectText
        {
            get { return _connectText; }
            set { SetProperty(ref _connectText, value); }
        }
        #endregion

        #region
        //连接状态
        private bool IsConnected;
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
                //this.Invoke(ReadStdOutput, new object[] { e.Data });
                ReadStdOutput(e.Data);
            }
        }

        #endregion

        #region p_ErrorDataReceived
        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                //this.Invoke(ReadErrOutput, new object[] { e.Data });
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
                ConnectText = "断开";
            }
            else if(result.Contains("login to server failed"))
            {
                IsConnected = false;
                ConnectText = "连接中...";
            }
            else if (result.Contains("try to reconnect to server..."))
            {
                IsConnected= false;
                ConnectText = "连接中...";
            }
            Logs += $"{result}\r\n";
        }

        #endregion

        #region ReadErrOutputAction
        private void ReadErrOutputAction(string result)
        {
            //this.textBoxShowErrRet.AppendText(result + "\r\n");
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
            // 启动进程执行相应命令,此例中以执行ping.exe为例
            if(!IsConnected)
            {
                Process.GetProcessesByName("frpc").ToList().ForEach(x => x.Kill());
                RealAction("./apps/e6ceff885fd78a86e9ddd8a39a2310f1/frpc.exe", "-c ./apps/e6ceff885fd78a86e9ddd8a39a2310f1/frpc.ini");
            }
            else
            {
                Process.GetProcessesByName("frpc").ToList().ForEach(x => x.Kill());
                IsConnected = false;
                ConnectText = "连接";
            }

        }
        #endregion
    }
}
