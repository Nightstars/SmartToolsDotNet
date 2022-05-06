using AxMSTSCLib;
using MSTSCLib;
using System.ComponentModel;

namespace RDP
{
    partial class RdpControl
    {
        #region 清理资源
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            _rdpClient = new MyRDP();

            ((ISupportInitialize)(this._rdpClient)).BeginInit();
            this.SuspendLayout();
            // 
            // UserControl1
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = SystemColors.Desktop;
            this.Controls.Add(this._rdpClient);
            this.Name = "RemoteDesktopControl";
            this.Size = new System.Drawing.Size(665, 422);
            ((ISupportInitialize)(this._rdpClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyRDP _rdpClient;
    }
}
