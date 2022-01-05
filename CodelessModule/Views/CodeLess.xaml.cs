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

        #region connect database
        private void connect_Click(object sender, RoutedEventArgs e)
        {
            //var logading = Dialog.Show(new Loading());
            //if (string.IsNullOrWhiteSpace(codeLessVM._connectString))
            //{
            //    HandyControl.Controls.MessageBox.Error("连接字符串不能为空!");
            //    return;
            //}

            //try
            //{
            //    codeLessVM._databaselist = new CodeLessService(codeLessVM._connectString).GetDatabase();

            //    databases.ItemsSource = codeLessVM._databaselist;
            //    databases.SelectedValuePath = "name";
            //    databases.DisplayMemberPath = "name";
            //    //HandyControl.Controls.MessageBox.Success("数据库连接成功");

            //}
            //catch (Exception ex)
            //{
            //    HandyControl.Controls.MessageBox.Error(ex.Message);
            //}
            //logading.Close();
        }
        #endregion

        #region databases_Selected
        private void databases_Selected(object sender, RoutedEventArgs e)
        {
            //var logading = Dialog.Show(new Loading());
            //if (string.IsNullOrWhiteSpace(codeLessVM._connectString))
            //{
            //    HandyControl.Controls.MessageBox.Error("连接字符串不能为空!");
            //    return;
            //}

            //dbtables.ItemsSource = new List<DbTable>();

            //try
            //{
            //    if (!string.IsNullOrWhiteSpace(codeLessVM._database))
            //    {
            //        dbtables.ItemsSource = new CodeLessService(codeLessVM._connectString).GetDbTable(codeLessVM._database);
            //        dbtables.SelectedValuePath = "name";
            //        dbtables.DisplayMemberPath = "name";
            //    }
            //    else
            //    {
            //        dbtables.SelectedIndex = -1;
            //        dbtables.ItemsSource = null;
            //        //primarykey.SelectedIndex = -1;
            //        //primarykey.ItemsSource = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    HandyControl.Controls.MessageBox.Error(ex.Message);
            //}
            //logading.Close();
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

        #region seelect sln file
        /// <summary>
        /// selectfile_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectfile_Click(object sender, RoutedEventArgs e)
        {
            //var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            //{
            //    Filter = "Solution Files (*.sln)|*.sln",
            //    Title = "选择解决方案"

            //};
            //var result = openFileDialog.ShowDialog();
            //if (result == true)
            //{
            //    codeLessVM.Arealist = new List<string>();
            //    codeLessVM.Rootnamespace = "";
            //    codeLessVM.Slnfileaddr = openFileDialog.FileName;
            //    codeLessVM.Projectlist = new SolutionUtil(codeLessVM._slnfileaddr).SlnParse();
            //    //codeLessVM.Project = codeLessVM.Projectlist.First().projName;

            //}
            //else
            //{
            //    HandyControl.Controls.MessageBox.Error("未选择任何解决方案!");
            //}
        }
        #endregion

        #region project_SelectionChanged
        /// <summary>
        /// project_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void project_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (codeLessVM._project != null)
            //{
            //    var path = codeLessVM?._projectlist?.Where(x => x.projName == codeLessVM._project)?.FirstOrDefault()?.projFullName;
            //    codeLessVM._projectPath = $"{path?.Substring(0, path.LastIndexOf("/"))}/Areas";
            //    codeLessVM._xmlpath = "App_Data";
            //    codeLessVM.Arealist = new List<string>();
            //    if (Directory.Exists(@$"{codeLessVM.slnfileaddr.Substring(0, codeLessVM.slnfileaddr.LastIndexOf("\\"))}\{codeLessVM._projectPath}"))
            //    {
            //        DirectoryInfo directoryInfo = new DirectoryInfo(@$"{codeLessVM.slnfileaddr.Substring(0, codeLessVM.slnfileaddr.LastIndexOf("\\"))}\{codeLessVM._projectPath}");
            //        var files = directoryInfo.GetDirectories();
            //        List<string> areas = new List<string>();
            //        foreach (var file in files)
            //        {
            //            areas.Add(file.Name);
            //        }
            //        codeLessVM.Arealist = areas;

            //    }
            //}
        }
        #endregion

        #region projectArea_SelectionChanged
        /// <summary>
        /// projectArea_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void projectArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (Directory.Exists(@$"{codeLessVM.slnfileaddr.Substring(0, codeLessVM.slnfileaddr.LastIndexOf("\\"))}\{codeLessVM._projectPath}"))
            //{
            //    codeLessVM._buildpath = @$"{codeLessVM.slnfileaddr.Substring(0, codeLessVM.slnfileaddr.LastIndexOf("\\"))}\{codeLessVM._projectPath}\{codeLessVM._projectArea}".Replace("\\", "/");
            //    var path = codeLessVM?._projectlist?.Where(x => x.projName == codeLessVM._project).FirstOrDefault()?.projName;
            //    codeLessVM.Rootnamespace = $"{path?.Substring(0, path.LastIndexOf("."))}.Areas.{codeLessVM?._projectArea}";
            //}
        }
        #endregion

        #region dbtables_SelectionChanged
        private void dbtables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(codeLessVM._dbTable))
            //    codeLessVM.DbTableInfos = new CodeLessService(codeLessVM._connectString).GetDbTableInfo(codeLessVM._database, codeLessVM._dbTable);
            //else
            //    codeLessVM.DbTableInfos = new List<DbTableInfo>();
        }
        #endregion

        #region CheckComboBox_SelectionChanged
        /// <summary>
        /// CheckComboBox_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //codeLessVM._searchParams = searchparam.SelectedItems.Cast<DbTableInfo>().ToList();
        }
        #endregion

        private void testConnect_Click(object sender, RoutedEventArgs e)
        {
            //use the message queue to send a message.
            //the message queue can be called from any thread
            //SnackbarThree.MessageQueue?.Enqueue(
            //$"连接成功",
            //null,
            //null,
            //null,
            //false,
            //true,
            //TimeSpan.FromSeconds(1));

            DialogHost.Show("haha");
        }
    }
}
