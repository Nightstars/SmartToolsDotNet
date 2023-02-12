using HandyControl.Controls;
using HandyControl.Data;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FansControlPanelUI.ViewModels
{
    internal class FansControlPanelViewModel : BindableBase
    {
        #region init
        private static string current = AppDomain.CurrentDomain.BaseDirectory;
        private string ipmitoolPath = $"{current}apps\\25dd149cfd8c7533b86c958b16b7025d\\ipmitool.exe";
        // 1.定义委托
        public delegate void DelReadStdOutput(string result);
        public delegate void DelReadErrOutput(string result);

        // 2.定义委托事件
        public event DelReadStdOutput ReadStdOutput;
        public event DelReadErrOutput ReadErrOutput;

        public FansControlPanelViewModel()
        {
            Init();
        }

        private void Init()
        {
            //3.将相应函数注册到委托事件中
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            ReadErrOutput += new DelReadErrOutput(ReadErrOutputAction);
        }
        #endregion

        #region ip
        private String _ip = "192.168.1.150";
        public String Ip
        {
            get { return _ip; }
            set { SetProperty(ref _ip, value); }
        }
        #endregion

        #region user
        private String _user = "root";
        public String User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
        #endregion

        #region password
        private String _pwd = "Sj.shangjian1998";
        public String Pwd
        {
            get { return _pwd; }
            set { SetProperty(ref _pwd, value); }
        }
        #endregion

        #region speed
        private int _speed = 12;
        public int Speed
        {
            get { return _speed; }
            set { SetProperty(ref _speed, value); }
        }
        #endregion

        #region 设置
        public ICommand SetCommand => new DelegateCommand<object>(SetFanSpeed);

        private void SetFanSpeed(object obj)
        {
            string fullExecuteDisableAutoMode = $"-I lanplus -H {_ip} -U {_user} -P {_pwd} raw 0x30 0x30 0x01 0x00";

            RealAction(ipmitoolPath, fullExecuteDisableAutoMode);

            string formatSetSpeed = "-I lanplus -H {0} -U {1} -P {2} raw 0x30 0x30 0x02 0xff 0x{3:x2}";

            string fullExecuteSetSpeed = string.Format(formatSetSpeed, _ip, _user, _pwd, _speed);

            RealAction(ipmitoolPath, fullExecuteSetSpeed);

            Growl.Success(new GrowlInfo { Message = "操作成功", ShowDateTime = false, WaitTime = 1 });
        }
        #endregion

        #region 重置
        public ICommand ResetCommand => new DelegateCommand<object>(ResetFanSpeed);

        private void ResetFanSpeed(object obj)
        {
            Speed = 10;

            string parametersReset = $"-I lanplus -H {_ip} -U {_user} -P {_pwd} raw 0x30 0x30 0x01 0x01";

            RealAction(ipmitoolPath, parametersReset);

            Growl.Success(new GrowlInfo { Message = "重置完成", ShowDateTime = false, WaitTime = 1 });
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
    }
}
