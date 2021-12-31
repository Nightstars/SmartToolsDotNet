using SmartSoft.common.Utils.solution;
using SmartToolsDotNet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartToolsDotNet.ViewModel
{
    public class CodeLessVM : INotifyPropertyChanged
    {
        #region initiazlize
        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string obj = "")
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(obj));
            }
        }
        #endregion

        #region 数据库列表
        /// <summary>
        /// 数据库列表
        /// </summary>
        public List<DataBaseInfo> databaselist { get; set; }
        #endregion

        #region 数据库连接字符串
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        //public String connectString= "Database=CMS;Server=127.0.0.1,6666;User ID = sa; Password = Ihavenoidea@0;";
        public String connectString= "Database=CMS;Server=192.168.2.52,63765;User ID = sa; Password = Longnows2021;";

        public string ConnectionString
        {
            get{ return connectString; }
            set{ connectString = value; Notify(); }
        }
        #endregion

        #region 数据库
        /// <summary>
        /// 数据库
        /// </summary>
        public String database = "";

        public string Database
        {
            get { return database; }
            set { database = value; Notify(); }
        }
        #endregion

        #region 命名空间
        /// <summary>
        /// 命名空间
        /// </summary>
        public String _rootnamespace = "";

        public string Rootnamespace
        {
            get { return _rootnamespace; }
            set { _rootnamespace = value; Notify(); }
        }
        #endregion

        #region 数据库表
        /// <summary>
        /// 数据库表
        /// </summary>
        public String dbTable = "";

        public string DbTable
        {
            get { return dbTable; }
            set { dbTable = value; Notify(); }
        }
        #endregion

        #region 解决方案地址
        /// <summary>
        /// 解决方案地址
        /// </summary>
        public String slnfileaddr = "";

        public string Slnfileaddr
        {
            get { return slnfileaddr; }
            set { slnfileaddr = value; Notify(); }
        }
        #endregion

        #region 项目列表
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<SolutionInfo> projectlist { get; set; }

        public List<SolutionInfo> Projectlist
        {
            get { return projectlist; }
            set { projectlist = value; Notify(); }
        }
        #endregion

        #region 项目
        /// <summary>
        /// 数据库
        /// </summary>
        public String project = "";

        public string Project
        {
            get { return project; }
            set { project = value; Notify(); }
        }
        #endregion

        #region 项目区域
        /// <summary>
        /// 项目区域
        /// </summary>
        public String projectArea = "";

        public string ProjectArea
        {
            get { return projectArea; }
            set { projectArea = value; Notify(); }
        }
        #endregion

        #region 项目地址
        /// <summary>
        /// 项目地址
        /// </summary>
        public String projectPath  { get;set; }
        #endregion

        #region XML配置地址
        /// <summary>
        /// 项目地址
        /// </summary>
        public String xmlpath { get; set; }
        #endregion

        #region 区域列表
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<string> arealist { get; set; }

        public List<string> Arealist
        {
            get { return arealist; }
            set { arealist = value; Notify(); }
        }
        #endregion

        #region 生成目录
        /// <summary>
        /// 生成目录
        /// </summary>
        public String buildpath { get; set; }
        #endregion

        #region 业务主键
        /// <summary>
        /// 业务主键
        /// </summary>
        public String primarykey = "";

        public string Primarykey
        {
            get { return primarykey; }
            set { primarykey = value; Notify(); }
        }
        #endregion

        #region 数据库字段列表
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<DbTableInfo> dbTableInfos { get; set; }

        public List<DbTableInfo> DbTableInfos
        {
            get { return dbTableInfos; }
            set { dbTableInfos = value; Notify(); }
        }
        #endregion

        #region 查询字段
        /// <summary>
        /// 查询字段
        /// </summary>
        public List<DbTableInfo> searchParams { get; set; }
        #endregion

        #region 项目类型列表
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<string> projecttypelist { get; set; }

        public List<string> Projecttypelist
        {
            get { return projecttypelist; }
            set { projecttypelist = value; Notify(); }
        }
        #endregion

        #region 项目类型
        /// <summary>
        /// 数据库
        /// </summary>
        public String projecttype = "";

        public string Projecttype
        {
            get { return projecttype; }
            set { projecttype = value; Notify(); }
        }
        #endregion

        #region 输出类型列表
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<string> outputtypelist { get; set; }

        public List<string> Outputtypelist
        {
            get { return outputtypelist; }
            set { outputtypelist = value; Notify(); }
        }
        #endregion

        #region 输出类型
        /// <summary>
        /// 数据库
        /// </summary>
        public String outputtype = "";

        public string Outputtype
        {
            get { return outputtype; }
            set { outputtype = value; Notify(); }
        }
        #endregion

        #region 基类类型列表
        /// <summary>
        /// 基类类型列表
        /// </summary>
        public List<string> basetypelist { get; set; }

        public List<string> Basetypelist
        {
            get { return basetypelist; }
            set { basetypelist = value; Notify(); }
        }
        #endregion

        #region 基类类型
        /// <summary>
        /// 基类类型
        /// </summary>
        public String basetype = "";

        public string Basetype
        {
            get { return basetype; }
            set { basetype = value; Notify(); }
        }
        #endregion

        #region 视图标题
        /// <summary>
        /// 视图标题
        /// </summary>
        public String viewtitle = "";

        public string Viewtitle
        {
            get { return viewtitle; }
            set { viewtitle = value; Notify(); }
        }
        #endregion
    }
}
