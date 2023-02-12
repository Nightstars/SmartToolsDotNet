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
using System.Windows.Input;

namespace FansControlPanelUI.ViewModels
{
    internal class FansControlPanelViewModel : BindableBase
    {
        #region init
        private static string ipmitoolPath = "./apps/25dd149cfd8c7533b86c958b16b7025d/ipmitool.exe";
        #endregion

        #region ip
        private String _ip = "111";
        public String Ip {
            get { return _ip; }
            set { SetProperty(ref _ip, value); }
        }
        #endregion

        #region user
        private String _user = "222";
        public String User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
        #endregion

        #region password
        private String _pwd = "333";
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
            string fullExecuteDisableAutoMode = $"{ipmitoolPath} -I lanplus -H {_ip} -U {_user} -P {_pwd} raw 0x30 0x30 0x01 0x00";

            string resultDisableAutoMode = execute(fullExecuteDisableAutoMode);

            string formatSetSpeed = $"{ipmitoolPath} -I lanplus -H {_ip} -U {_user} -P {_pwd} raw 0x30 0x30 0x02 0xff 0x{3:x2}";

            string fullExecuteSetSpeed = string.Format(formatSetSpeed, _speed);

            string resultSetSpeed = execute(fullExecuteSetSpeed);

            Growl.Success(new GrowlInfo { Message = "操作成功", ShowDateTime = false, WaitTime = 1 });
        }
        #endregion

        #region 重置
        public ICommand ResetCommand => new DelegateCommand<object>(ResetFanSpeed);

        private void ResetFanSpeed(object obj)
        {
            Speed = 10;

            string parametersReset = $"-I lanplus -H {_ip} -U {_user} -P {_pwd} raw 0x30 0x30 0x01 0x01";

            string fullExecuteReset = $"{ipmitoolPath} {parametersReset}";

            execute(fullExecuteReset);

            Growl.Success(new GrowlInfo { Message = "重置完成", ShowDateTime = false, WaitTime = 1 });
        }
        #endregion

        #region 执行
        private string execute(string parameter)
        {
            Process process = null;
            string result = string.Empty;
            try
            {
                process = new Process();

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.Start();

                process.StandardInput.WriteLine(parameter + "& exit");
                process.StandardInput.AutoFlush = true;
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ExceptionOccurred:{ 0},{ 1}", ex.Message, ex.StackTrace.ToString());
                return null;
            }
        }
        #endregion
    }
}
