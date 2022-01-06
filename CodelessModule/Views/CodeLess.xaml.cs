using CodelessModule.CustomControls;
using CodelessModule.Models;
using CodelessModule.Services;
using CodelessModule.Utils;
using CodelessModule.ViewModels;
using HandyControl.Controls;
using MaterialDesignThemes.Wpf;
using SmartSoft.common.Utils.solution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodelessModule.Views
{
    /// <summary>
    /// CodeLess.xaml 的交互逻辑
    /// </summary>
    [SupportedOSPlatform("windows7.0")]
    public partial class CodeLess : UserControl
    {
        #region initialize
        private string _currentLan = string.Empty;
        public CodeLess()
        {
            InitializeComponent();
            _currentLan = "zh-cn";
        }
        #endregion

        #region GenCode_Click
        private void GenCode_Click(object sender, RoutedEventArgs e)
        {
            //var logading = Dialog.Show(new Loading());

            //if (string.IsNullOrWhiteSpace(codeLessVM._connectString))
            //{
            //    HandyControl.Controls.MessageBox.Error("连接字符串不能为空!");
            //    logading.Close();
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(codeLessVM._database))
            //{
            //    HandyControl.Controls.MessageBox.Error("请选择数据库!");
            //    logading.Close();
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(codeLessVM._dbTable))
            //{
            //    HandyControl.Controls.MessageBox.Error("请选择数据库表!");
            //    logading.Close();
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(codeLessVM._primarykey) && codeLessVM._projecttype == "wechat api")
            //{
            //    HandyControl.Controls.MessageBox.Error("请选择业务主键!");
            //    logading.Close();
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(codeLessVM._rootnamespace))
            //{
            //    HandyControl.Controls.MessageBox.Error("请填写命名空间!");
            //    logading.Close();
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(codeLessVM._projecttype))
            //{
            //    HandyControl.Controls.MessageBox.Error("请选择项目类型!");
            //    logading.Close();
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(codeLessVM._outputtype))
            //{
            //    HandyControl.Controls.MessageBox.Error("请选择生成类型!");
            //    logading.Close();
            //    return;
            //}


            //try
            //{
            //    var projname = $"{codeLessVM.project.Split(".").Reverse().Skip(1).Take(1).FirstOrDefault()}";
            //    var tbname = $"{ FiledUtil.GetModelName(codeLessVM._dbTable) }";

            //    var temp = codeLessVM._buildpath;

            //    //生成代码
            //    new CodeBuilder
            //    (
            //        codeLessVM.DbTableInfos,
            //        codeLessVM._rootnamespace,
            //        tbname.Contains(projname) ? $"{tbname}" : $"{projname}{tbname}",
            //        codeLessVM._database,
            //        codeLessVM._dbTable,
            //        codeLessVM._buildpath,
            //        codeLessVM._projecttype,
            //        codeLessVM._outputtype,
            //        codeLessVM._primarykey,
            //        codeLessVM._searchParams,
            //        codeLessVM._projectArea,
            //        $"{codeLessVM.buildpath.Split("Areas").FirstOrDefault()}{codeLessVM._xmlpath}",
            //        codeLessVM._viewtitle
            //    )
            //    .BuildModel()
            //    .BuildSearchModel()
            //    .BuildService()
            //    .BuildController()
            //    .BuildViews()
            //    .BuildDbinfo();

            //    //show result
            //    if (codeLessVM._outputtype == "项目")
            //        HandyControl.Controls.MessageBox.Success("生成成功");
            //    else
            //    {
            //        var result = HandyControl.Controls.MessageBox.Show("生成成功,是否打开输出文件夹？", "温馨提示", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
            //        if (result == MessageBoxResult.OK)
            //        {
            //            logading.Close();
            //            Process.Start("explorer.exe", $@"{AppDomain.CurrentDomain.BaseDirectory}Oupput\");
            //        }
            //        else
            //            logading.Close();
            //    }
            //    logading.Close();
            //}
            //catch (Exception ex)
            //{
            //    HandyControl.Controls.MessageBox.Error(ex.Message);
            //    logading.Close();
            //}
            //finally
            //{
            //    logading.Close();
            //}
        }
        #endregion

    }
}
