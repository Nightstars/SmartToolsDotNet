using CodelessModule.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CodelessModule.Utils
{
    public class CodeBuilder
    {
		#region initialize
		/// <summary>
		/// 字段信息
		/// </summary>
		private List<DbTableInfo> _list;

		/// <summary>
		/// 命名空间
		/// </summary>
		private string _rootnamespace;

		/// <summary>
		/// 模块名称
		/// </summary>
		private string _modelName;

		/// <summary>
		/// 选中的数据库
		/// </summary>
		private string _database;

		/// <summary>
		/// 选中的表
		/// </summary>
		private string _tbname;

		/// <summary>
		/// 生成路径
		/// </summary>
		private string _buildpath;

		/// <summary>
		/// 项目类型
		/// </summary>
		private string _projtype;
		/// <summary>
		/// 输出类型
		/// </summary>
		private string _outputtype;

		/// <summary>
		/// 业务主键
		/// </summary>
		private string _primarykey;

		/// <summary>
		/// 查询字段
		/// </summary>
		private List<DbTableInfo> _searchlist;

		/// <summary>
		/// 数据库表
		/// </summary>
		private string _dbtable;

		/// <summary>
		/// 区域
		/// </summary>
		private string _area;

		/// <summary>
		/// XML路径
		/// </summary>
		private string _xmlpath;

		/// <summary>
		/// 视图标题
		/// </summary>
		private string _viewtitle;

		public CodeBuilder(List<DbTableInfo> ls,
			string rootnamespace,
			string modelName,
			string database,
			string tbname,
			string buildpath,
			string projtype,
			string outputtype,
			string primarykey,
			List<DbTableInfo> searchls,
			string area,
			string xmlpath,
			string viewtitle)
        {
			_list = ls;
			_rootnamespace = rootnamespace;
			_modelName = modelName;
			_database = database;
            if (tbname.Contains("_"))
            {
				_tbname = tbname.Replace("_","");
            }
            else
            {
				_tbname = tbname;
            }
			_dbtable = tbname;
			_buildpath = buildpath;
			_projtype = projtype;
			_outputtype = outputtype;
			_primarykey = primarykey;
			_searchlist = searchls;
			_area = area;
			_xmlpath = xmlpath;
			_viewtitle= viewtitle;

		}
        #endregion

        #region build model
        /// <summary>
        /// BuildModel
        /// </summary>
        public CodeBuilder BuildModel()
		{
			if(_projtype == "wechat api")
            {
				BuildModelForWechatApi();
            } else
            {
				BuildModelForCms();
            }

			return this;
		}
		#endregion

		#region build Searchmodel
		/// <summary>
		/// BuildSearchModel
		/// </summary>
		public CodeBuilder BuildSearchModel()
		{
			if (_projtype == "wechat api")
			{
				BuildSearchModelForWechatApi();
			}
			else
			{
				BuildImXmlForCms();
				BuildEpXmlForCms();
				BuildSearchModelForCms();
			}

			return this;
		}
		#endregion

		#region build service
		/// <summary>
		/// BuildService
		/// </summary>
		/// <param name="ls"></param>
		/// <param name="rootnamespace"></param>
		/// <param name="modelName"></param>
		public CodeBuilder BuildService()
		{
			if (_projtype == "wechat api")
			{
				BuildServiceForWechatApi();
			}
			else
			{
				BuildServiceForCms();
			}

			return this;
		}
		#endregion

		#region build controller
		/// <summary>
		/// BuildController
		/// </summary>
		public CodeBuilder BuildController()
		{
			if (_projtype == "wechat api")
			{
				BuildControllerForWechatApi();
			}
			else
			{
				BuildControllerForCms();
			}

			return this;
		}
		#endregion

		#region build BuildViews
		/// <summary>
		/// BuildController
		/// </summary>
		public CodeBuilder BuildViews()
		{
			if (_projtype == "cms")
			{
				BuildIndexForCms();
				BuildCreateForCms();
				BuildEditForCms();
				BuildDetailsForCms();
			}

			return this;
		}
		#endregion

		#region build Db info for api_wechat
		/// <summary>
		/// BuildDbinfo
		/// </summary>
		/// <returns></returns>
		public CodeBuilder BuildDbinfo()
		{
			if (_projtype == "wechat api")
			{
				var arr = _buildpath.Split('/');
				string path = $"{ string.Join("/", arr.Take(arr.Length - 2)) }/Models/CoreDbTables.cs";
				StringBuilder stringBuilder = new StringBuilder()
				.AppendLine("#region Tables")
				.AppendLine("		/// <summary>")
				.AppendLine($"		/// {_tbname}")
				.AppendLine("		/// </summary>")
				.AppendLine($"		public const string {_tbname} = \"{_dbtable}\";")
				.AppendLine("");
				string text = null;
				using FileStream filestream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
				using StreamReader streamReader = new StreamReader(filestream);
				text = streamReader.ReadToEnd();
				text = text.Replace("#region Tables", stringBuilder.ToString());
				var data = Encoding.UTF8.GetBytes(text);
				filestream.Seek(0, SeekOrigin.Begin);
				filestream.Write(data, 0, data.Length);
			}
			return this;
		}
		#endregion

		#region build model for api_wechat
		/// <summary>
		/// BuildModelForWechatApi
		/// </summary>
		public void BuildModelForWechatApi()
        {
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DbTableInfo item in _list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					if (stringBuilder.Length == 0)
					{
						stringBuilder.AppendLine("/// <summary>");
					}
					else
					{
						stringBuilder.AppendLine("        /// <summary>");
					}
					stringBuilder.AppendLine($"        /// {item.columnDescription}")
					.AppendLine("        /// </summary>")
					.AppendLine($"        [Display(Name = \"{item.columnDescription}\")]")
					//stringBuilder.AppendLine("        [TableColumn(\"" + item.columnName + "\")]");
					.AppendLine(string.Concat(new string[]
					{
						"        public ",
						FiledUtil.GetFiledType(item.type),
						" ",
						item.columnName,
						" { get; set; }"
					}))
					.AppendLine("");
				}
			}
			string text = FileUtil.GetTplContent("Tpl.wechatapi.SmartTools.Net.model.tpl");
			text = text.Replace("$Rootnamespace$", $"{_rootnamespace}.Models");
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			text = text.Replace("$EntityField$", stringBuilder.ToString());
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Models"))
					Directory.CreateDirectory($"{_buildpath}/Models");
				File.WriteAllText($"{_buildpath}/Models/{_modelName}.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}.cs", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build SearchModel for api_wechat
		/// <summary>
		/// BuildSearchModelForWechatApi
		/// </summary>
		public void BuildSearchModelForWechatApi()
		{
			StringBuilder stringBuilder = new StringBuilder();
			var list = new List<DbTableInfo>();
			if (_searchlist.Any())
			{
				list = _list.Intersect(_searchlist).ToList();
			}
			else
			{
				list = _list;
			}
			if (!list.Exists(x => x.columnName == _primarykey))
			{
				list.Add(_list.Find(x => x.columnName == _primarykey));
			}
			foreach (DbTableInfo item in list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					if (stringBuilder.Length == 0)
					{
						stringBuilder.AppendLine("/// <summary>");
					}
					else
					{
						stringBuilder.AppendLine("        /// <summary>");
					}
					stringBuilder.AppendLine($"        /// {item.columnDescription}")
					.AppendLine("        /// </summary>")
					.AppendLine($"        [Display(Name = \"{item.columnDescription}\")]")
					//stringBuilder.AppendLine("        [TableColumn(\"" + item.columnName + "\")]");
					.AppendLine(string.Concat(new string[]
					{
						"        public ",
						FiledUtil.GetFiledType(item.type),
						" ",
						item.columnName,
						" { get; set; }"
					}))
					.AppendLine("");
				}
			}
			string text = FileUtil.GetTplContent("Tpl.wechatapi.SmartTools.Net.searchmodel.tpl");
			text = text.Replace("$Rootnamespace$", $"{_rootnamespace}.Data");
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			text = text.Replace("$EntityField$", stringBuilder.ToString());
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Data"))
					Directory.CreateDirectory($"{_buildpath}/Data");
				File.WriteAllText($"{_buildpath}/Data/{_modelName}SearchDto.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}SearchDto.cs", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build service for api_wechat
		/// <summary>
		/// BuildServiceForWechatApi
		/// </summary>
		public void BuildServiceForWechatApi()
		{
			StringBuilder stringBuilder = new StringBuilder();
			var list = new List<DbTableInfo>();
			if (_searchlist.Any())
			{
				list = _list.Intersect(_searchlist).ToList();
			}
			else
			{
				list = _list;
			}
			foreach (DbTableInfo item in list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					if (stringBuilder.Length == 0)
					{
						stringBuilder.AppendLine($".WhereIF(!string.IsNullOrWhiteSpace(searchDto.{item.columnName}),x=>x.{item.columnName}.Contains(searchDto.{item.columnName}))");
					}
					else
					{
						stringBuilder.AppendLine($"				.WhereIF(!string.IsNullOrWhiteSpace(searchDto.{item.columnName}),x=>x.{item.columnName}.Contains(searchDto.{item.columnName}))");
					}
				}
			}
			string text = FileUtil.GetTplContent("Tpl.wechatapi.SmartTools.Net.service.tpl");
			text = text.Replace("$Rootnamespace$", _rootnamespace);
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$Dadabase$", _database);
			text = text.Replace("$Dbtable$", _tbname);
			text = text.Replace("$PK$", _primarykey);
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			text = text.Replace("$SearchFiled$", stringBuilder.ToString());
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Services"))
					Directory.CreateDirectory($"{_buildpath}/Services");
				File.WriteAllText($"{_buildpath}/Services/{_modelName}Service.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}Service.cs", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build controller for api_wechat
		/// <summary>
		/// BuildControllerForWechatApi
		/// </summary>
		public void BuildControllerForWechatApi()
		{
			string text = FileUtil.GetTplContent("Tpl.wechatapi.SmartTools.Net.controller.tpl");
			var pktype = _list.Find(x => x.columnName == _primarykey).type;
			text = text.Replace("$Rootnamespace$", _rootnamespace);
			text = text.Replace("$ModelName$", _modelName);
			text = text.Replace("$PK$", _primarykey);
			text = text.Replace("$PKType$", FiledUtil.GetFiledType(pktype));
			text = text.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Controllers"))
					Directory.CreateDirectory($"{_buildpath}/Controllers");
				File.WriteAllText($"{_buildpath}/Controllers/{_modelName}Controller.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}Controller.cs", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build Model for cms
		/// <summary>
		/// BuildModelForCms
		/// </summary>
		public void BuildModelForCms()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DbTableInfo item in _list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					if (stringBuilder.Length == 0)
					{
						stringBuilder.AppendLine("/// <summary>");
					}
					else
					{
						stringBuilder.AppendLine("        /// <summary>");
					}
					stringBuilder.AppendLine($"        /// {item.columnDescription}")
					.AppendLine("        /// </summary>")
					.AppendLine($"        [Display(Name = \"{item.columnDescription}\")]")
					.AppendLine("        [TableColumn(\"" + item.columnName + "\")]")
					.AppendLine(string.Concat(new string[]
					{
						"        public ",
						FiledUtil.GetFiledType(item.type),
						" ",
						FiledUtil.GetField(item.columnName),
						" { get; set; }"
					}))
					.AppendLine("");
				}
			}

			string text = FileUtil.GetTplContent("Tpl.cms.SmartTools.Net.model.tpl")
			.Replace("$Rootnamespace$", $"{_rootnamespace}.Models")
			.Replace("$ModelName$", _modelName)
			.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
			.Replace("$EntityField$", stringBuilder.ToString());

			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Models"))
					Directory.CreateDirectory($"{_buildpath}/Models");
				File.WriteAllText($"{_buildpath}/Models/{_modelName}.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}.cs", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build SearchModel for cms
		/// <summary>
		/// BuildSearchModelForCms
		/// </summary>
		public void BuildSearchModelForCms()
		{
			StringBuilder stringBuilder = new StringBuilder()
				.AppendLine($"public override string ImportXmlName => \"GenImport.xml\";")
				.AppendLine()
				.AppendLine($"		public override string ImportXmlNode => \"{_modelName.ToUpper()}_IMPORT\";")
				.AppendLine()
				.AppendLine($"		public override string ExportFileName => \"{_viewtitle}\";")
				.AppendLine()
				.AppendLine($"		public override string ExportXmlName => \"GenExport.xml\";")
				.AppendLine()
				.AppendLine($"		public override string ExportXmlNode => \"{_modelName.ToUpper()}_EXPORT\";")
				.AppendLine();

			var list = new List<DbTableInfo>();
			if (_searchlist.Any())
			{
				list = _list.Intersect(_searchlist).ToList();
			}
			else
			{
				list = _list;
			}
			if (!list.Exists(x => x.columnName == _primarykey) && !string.IsNullOrWhiteSpace(_primarykey))
			{
				list.Add(_list.Find(x => x.columnName == _primarykey));
			}

			foreach (DbTableInfo item in list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					if (stringBuilder.Length == 0)
					{
						stringBuilder.AppendLine("/// <summary>");
					}
					else
					{
						stringBuilder.AppendLine("        /// <summary>");
					}
					stringBuilder.AppendLine($"        /// {item.columnDescription}")
					.AppendLine("        /// </summary>")
					.AppendLine($"        [Display(Name = \"{item.columnDescription}\")]")
					//stringBuilder.AppendLine("        [TableColumn(\"" + item.columnName + "\")]");
					.AppendLine(string.Concat(new string[]
					{
						"        public ",
						FiledUtil.GetFiledType(item.type),
						" ",
						FiledUtil.GetField(item.columnName),
						" { get; set; }"
					}))
					.AppendLine("");
				}
			}

			string text = FileUtil.GetTplContent("Tpl.cms.SmartTools.Net.searchmodel.tpl")
			.Replace("$Rootnamespace$", $"{_rootnamespace}.Data")
			.Replace("$ModelName$", _modelName)
			.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
			.Replace("$EntityField$", stringBuilder.ToString());

			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Data"))
					Directory.CreateDirectory($"{_buildpath}/Data");
				File.WriteAllText($"{_buildpath}/Data/{_modelName}SearchDto.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}SearchDto.cs", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build service for cms
		/// <summary>
		/// BuildServiceForCms
		/// </summary>
		public void BuildServiceForCms()
		{
			StringBuilder stringBuilder = new StringBuilder()
			.AppendLine($"protected override string DeleteSql => @\"DELETE FROM {_database}.dbo.{_dbtable} WHERE SEQ_NO = @SeqNo\";")
			.AppendLine()
			.AppendLine($"		protected override string GetSql => @\"SELECT * FROM {_database}.dbo.{_dbtable} WHERE SEQ_NO = @SeqNo\";")
			.AppendLine()
			.AppendLine($"		protected override string GetExecutePageSql => @\"SELECT * FROM {_database}.dbo.{_dbtable} ")
			.AppendLine("                                                          WHERE SITE = @Site\";")
			.AppendLine()
			.AppendLine($"		protected override string InsertSql => @\"INSERT INTO {_database}.dbo.{_dbtable}(");
			var insertlist = _list.Where(x => !FiledUtil.insertSkipFileds.Contains(x.columnName)).ToList();
			for (int i = 0; i < insertlist.Count; i++)
			{
				insertlist[i].columnDescription = insertlist[i].columnDescription ?? insertlist[i].columnName;
				if (i == insertlist.Count - 1)
					stringBuilder.AppendLine($"													{insertlist[i].columnName}	--{insertlist[i].columnDescription}");
				else
					stringBuilder.AppendLine($"													{insertlist[i].columnName},	--{insertlist[i].columnDescription}");
			}

			stringBuilder.AppendLine("												)");
			stringBuilder.AppendLine("												VALUES(");
			for (int i = 0; i < insertlist.Count; i++)
			{
				insertlist[i].columnDescription = insertlist[i].columnDescription ?? insertlist[i].columnName;
				if(i== insertlist.Count-1)
					stringBuilder.AppendLine($"													@{FiledUtil.GetField(insertlist[i].columnName)}	--{insertlist[i].columnDescription}");
				else
					stringBuilder.AppendLine($"													@{FiledUtil.GetField(insertlist[i].columnName)},	--{insertlist[i].columnDescription}");
			}
			stringBuilder.AppendLine("												)\";")
			.AppendLine()
			.AppendLine($"        protected override string UpdateSql => @\"UPDATE {_database}.dbo.{_dbtable} SET");
			var updatelist = _list.Where(x => !FiledUtil.updateSkipFileds.Contains(x.columnName)).ToList();
			for (int i = 0; i < updatelist.Count; i++)
			{
				updatelist[i].columnDescription = updatelist[i].columnDescription ?? updatelist[i].columnName;
				if (i == updatelist.Count - 1)
					stringBuilder.AppendLine($"													{updatelist[i].columnName} = @{FiledUtil.GetField(updatelist[i].columnName)}	--{updatelist[i].columnDescription}");
				else
					stringBuilder.AppendLine($"													{updatelist[i].columnName} = @{FiledUtil.GetField(updatelist[i].columnName)},	--{updatelist[i].columnDescription}");
			}
			stringBuilder.AppendLine("												WHERE SEQ_NO = @SeqNo\";");


			string text = FileUtil.GetTplContent("Tpl.cms.SmartTools.Net.service.tpl")
			.Replace("$Rootnamespace$", _rootnamespace)
			.Replace("$DBTable$", $"{_database}.dbo.{_dbtable}")
			.Replace("$ModelName$", _modelName)
			.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
			.Replace("$sqls$", stringBuilder.ToString());

			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Services"))
					Directory.CreateDirectory($"{_buildpath}/Services");
				File.WriteAllText($"{_buildpath}/Services/{_modelName}Service.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}Service.cs", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build controller for cms
		/// <summary>
		/// BuildControllerForCms
		/// </summary>
		public void BuildControllerForCms()
		{
			string text = FileUtil.GetTplContent("Tpl.cms.SmartTools.Net.controller.tpl")
			//var pktype = _list.Find(x => x.columnName == _primarykey).type;
			.Replace("$Rootnamespace$", _rootnamespace)
			.Replace("$ModelName$", _modelName)
			.Replace("$area$", _area)
			//text = text.Replace("$PK$", _primarykey);
			//text = text.Replace("$PKType$", FiledUtil.GetFiledType(pktype));
			.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Controllers"))
					Directory.CreateDirectory($"{_buildpath}/Controllers");
				File.WriteAllText($"{_buildpath}/Controllers/{_modelName}Controller.cs", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists("./Oupput"))
					Directory.CreateDirectory("./Oupput");
				File.WriteAllText($"./Oupput/{_modelName}Controller.cs", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build Views.Index for cms
		/// <summary>
		/// BuildIndexForCms
		/// </summary>
		public void BuildIndexForCms()
		{
			string text = FileUtil.GetTplContent("Tpl.cms.SmartTools.Net.Views.Index.tpl");

			//表格列字段
			StringBuilder fields= new StringBuilder();
			foreach (DbTableInfo item in _list)
			{
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					if (fields.Length == 0)
					{
						fields.AppendLine(string.Concat(new string[]
						{
							"<th lay-data = \"{field: '",
							FiledUtil.GetField(item.columnName),
							"', width: 180, align: 'center'}\">",
							item.columnDescription,
							"</th>"
						}));
					}
					else
					{
						fields.AppendLine(string.Concat(new string[]
						{
							"				<th lay-data = \"{field: '",
							FiledUtil.GetField(item.columnName),
							"', width: 180, align: 'center'}\">",
							item.columnDescription,
							"</th>"
						}));
					}
				}
			}

			//查询字段
			StringBuilder queryArea = new StringBuilder();
			if(_searchlist.Count > 0)
            {
				FiledUtil.GetRows(_searchlist, out int totoalrows, out int residu);

				//完整行
                for (int i = 0; i < totoalrows-1; i++)
                {
					var list = _searchlist.Skip(4 * i).Take(4);
					if(queryArea.Length == 0)
						queryArea.AppendLine("<div class=\"layui-row\">");
					else
						queryArea.AppendLine("				<div class=\"layui-row\">");
                    foreach (var item in list)
                    {
						item.columnDescription = item.columnDescription ?? item.columnName;
						queryArea.AppendLine("					<div class=\"layui-col-md1 layui-col-sm1 layui-lb\">")
						.AppendLine($"						<label>{item.columnDescription}</label>")
						.AppendLine($"					</div>")
						.AppendLine("					<div class=\"layui-col-md2 layui-col-sm2 \">")
						.AppendLine($"						<input id='srh{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\">")
						.AppendLine($"					</div>");

					}
					queryArea.AppendLine("				</div>").AppendLine();
				}

				//不足一行需占位
				var residulist = _searchlist.Skip(totoalrows - residu).Take(residu);
				queryArea.AppendLine("				<div class=\"layui-row\">");
				foreach (var item in residulist)
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					queryArea.AppendLine("					<div class=\"layui-col-md1 layui-col-sm1 layui-lb\">")
					.AppendLine($"						<label>{item.columnDescription}</label>")
					.AppendLine($"					</div>")
					.AppendLine("					<div class=\"layui-col-md2 layui-col-sm2 \">")
					.AppendLine($"						<input id='srh{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\">")
					.AppendLine($"					</div>");

				}
				if (residu != 0)
					queryArea.AppendLine($"					<div class=\"layui-col-md{12-residu*3} layui-col-sm{12 - residu * 3} \" ></div>");
				queryArea.AppendLine("				</div>");

			}

			//查询字段
			StringBuilder searchParam = new StringBuilder();
            foreach (var item in _searchlist)
            {
				item.columnDescription = item.columnDescription ?? item.columnName;
				if (searchParam.Length == 0)
					searchParam.AppendLine($"{FiledUtil.GetField(item.columnName)}: $('#srh{FiledUtil.GetField(item.columnName)}').val(), //{item.columnDescription}");
                else
					searchParam.AppendLine($"					{FiledUtil.GetField(item.columnName)}: $('#srh{FiledUtil.GetField(item.columnName)}').val(), //{item.columnDescription}");
			}

			//重置查询条件
			StringBuilder resetParam = new StringBuilder();
			foreach (var item in _searchlist)
			{
				item.columnDescription = item.columnDescription ?? item.columnName;
				if (resetParam.Length == 0)
					resetParam.AppendLine($"$('#srh{FiledUtil.GetField(item.columnName)}').val(''); //{item.columnDescription}");
				else
					resetParam.AppendLine($"				$('#srh{FiledUtil.GetField(item.columnName)}').val(''); //{item.columnDescription}");
			}

			//替换模板文本
			text = text.Replace("$title$", _viewtitle)
				.Replace("$tableFields$", fields.ToString())
				.Replace("$searchArea$", queryArea.ToString())
				.Replace("$queryParam$", searchParam.ToString())
				.Replace("$resetParam$", resetParam.ToString())
				.Replace("$privilege$", $"/{_area}/{_modelName}")
				.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			//生成输出文件
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Views/{_modelName}"))
					Directory.CreateDirectory($"{_buildpath}/Views/{_modelName}");
				File.WriteAllText($"{_buildpath}/Views/{_modelName}/Index.cshtml", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists($"./Oupput/Views/{_modelName}"))
					Directory.CreateDirectory($"./Oupput/Views/{_modelName}");
				File.WriteAllText($"./Oupput/Views/{_modelName}/Index.cshtml", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build Views.Create for cms
		/// <summary>
		/// BuildCreateForCms
		/// </summary>
		public void BuildCreateForCms()
		{
			string text = FileUtil.GetTplContent("Tpl.cms.SmartTools.Net.Views.create.tpl");

			//查询字段
			StringBuilder formArea = new StringBuilder();
			if (_list.Count > 0)
			{
				var createlist = new List<DbTableInfo>();
				_list.ForEach(x => { if (!FiledUtil.modelSkipFileds.Contains(x.columnName)) createlist.Add(x); });

				FiledUtil.GetRows(createlist, out int totoalrows, out int residu);

				//完整行
				for (int i = 0; i < totoalrows - 1; i++)
				{
					var list = createlist.Skip(3 * i).Take(3);
					if (formArea.Length == 0)
						formArea.AppendLine("<div class=\"layui-row\">");
					else
						formArea.AppendLine("			<div class=\"layui-row\">");
					foreach (var item in list)
					{
						item.columnDescription = item.columnDescription ?? item.columnName;
						formArea.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2 layui-lb\">")
						.AppendLine($"					<label>{item.columnDescription}</label>")
						.AppendLine($"				</div>")
						.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2\">");

						if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("int"))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onkeypress=\"return (/^[\\d]*$/.test(String.fromCharCode(event.keyCode)))\" onchange=\"NumFormat(this, 0)\" oninput=\"NumFormat(this, 0)\" >");
						else if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("date"))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onchange=\"this.value = this.value.replace(/\\s/g, '')\" oninput=\"this.value = this.value.replace(/\\s/g, '')\" >");
						else if (!string.IsNullOrWhiteSpace(item.type) && (item.type.Contains("float") || item.type.Contains("numeric") || item.type.Contains("decimal")))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" Onkeyup=\"this.value = this.value.toString().match(/^\\d{{0,14}}(?:\\.\\d{{0,5}})?/)\" onchange=\"NumFormat(this, 5)\" oninput=\"NumFormat(this, 5)\" >");
						else
							formArea.AppendLine($"					<input maxlength='{item.length}' name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" >");

						formArea.AppendLine($"				</div>");

					}
					formArea.AppendLine("			</div>").AppendLine();
				}

				//不足一行需占位
				var residulist = createlist.Skip(totoalrows - residu).Take(residu);
				formArea.AppendLine("			<div class=\"layui-row\">");
				foreach (var item in residulist)
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					formArea.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2 layui-lb\">")
					.AppendLine($"					<label>{item.columnDescription}</label>")
					.AppendLine($"				</div>")
					.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2\">");

					if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("int"))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onkeypress=\"return (/^[\\d]*$/.test(String.fromCharCode(event.keyCode)))\" onchange=\"NumFormat(this, 0)\" oninput=\"NumFormat(this, 0)\" >");
					else if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("date"))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onchange=\"this.value = this.value.replace(/\\s/g, '')\" oninput=\"this.value = this.value.replace(/\\s/g, '')\" >");
					else if (!string.IsNullOrWhiteSpace(item.type) && (item.type.Contains("float") || item.type.Contains("numeric") || item.type.Contains("decimal")))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" Onkeyup=\"this.value = this.value.toString().match(/ ^\\d{{ 0,14}} (?:\\.\\d{{ 0,5}})?/)\" onchange=\"NumFormat(this, 5)\" oninput=\"NumFormat(this, 5)\" >");
					else
						formArea.AppendLine($"					<input maxlength='{item.length}' name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" >");

					formArea.AppendLine($"				</div>").AppendLine();


				}
				if (residu != 0 && residu != 3)
					formArea.AppendLine($"				<div class=\"layui-col-md{12 - residu * 4} layui-col-sm{12 - residu * 4} \"></div>");
				formArea.AppendLine($"			</div>");

			}

			//替换模板文本
			text = text.Replace("$title$", _viewtitle)
				.Replace("$formArea$", formArea.ToString())
				.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			//生成输出文件
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Views/{_modelName}"))
					Directory.CreateDirectory($"{_buildpath}/Views/{_modelName}");
				File.WriteAllText($"{_buildpath}/Views/{_modelName}/Create.cshtml", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists($"./Oupput/Views/{_modelName}"))
					Directory.CreateDirectory($"./Oupput/Views/{_modelName}");
				File.WriteAllText($"./Oupput/Views/{_modelName}/Create.cshtml", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build Views.Edit for cms
		/// <summary>
		/// BuildEditForCms
		/// </summary>
		public void BuildEditForCms()
		{
			string text = FileUtil.GetTplContent("Tpl.cms.SmartTools.Net.Views.edit.tpl");

			//查询字段
			StringBuilder formArea = new StringBuilder();
			if (_list.Count > 0)
			{
				var createlist = new List<DbTableInfo>();
				_list.ForEach(x => { if (!FiledUtil.modelSkipFileds.Contains(x.columnName)) createlist.Add(x); });

				FiledUtil.GetRows(createlist, out int totoalrows, out int residu);

				//完整行
				for (int i = 0; i < totoalrows - 1; i++)
				{
					var list = createlist.Skip(3 * i).Take(3);
					if (formArea.Length == 0)
						formArea.AppendLine("<div class=\"layui-row\">");
					else
						formArea.AppendLine("			<div class=\"layui-row\">");
					foreach (var item in list)
					{
						item.columnDescription = item.columnDescription ?? item.columnName;
						formArea.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2 layui-lb\">")
						.AppendLine($"					<label>{item.columnDescription}</label>")
						.AppendLine($"				</div>")
						.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2\">");

						if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("int"))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onkeypress=\"return (/^[\\d]*$/.test(String.fromCharCode(event.keyCode)))\" onchange=\"NumFormat(this, 0)\" oninput=\"NumFormat(this, 0)\" >");
						else if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("date"))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onchange=\"this.value = this.value.replace(/\\s/g, '')\" oninput=\"this.value = this.value.replace(/\\s/g, '')\" >");
						else if (!string.IsNullOrWhiteSpace(item.type) && (item.type.Contains("float") || item.type.Contains("numeric") || item.type.Contains("decimal")))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" Onkeyup=\"this.value = this.value.toString().match(/^\\d{{0,14}}(?:\\.\\d{{0,5}})?/)\" onchange=\"NumFormat(this, 5)\" oninput=\"NumFormat(this, 5)\" >");
						else
							formArea.AppendLine($"					<input maxlength='{item.length}' name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" >");

						formArea.AppendLine($"				</div>");

					}
					formArea.AppendLine("			</div>").AppendLine();
				}

				//不足一行需占位
				var residulist = createlist.Skip(totoalrows - residu).Take(residu);
				formArea.AppendLine("			<div class=\"layui-row\">");
				foreach (var item in residulist)
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					formArea.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2 layui-lb\">")
					.AppendLine($"					<label>{item.columnDescription}</label>")
					.AppendLine($"				</div>")
					.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2\">");

					if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("int"))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onkeypress=\"return (/^[\\d]*$/.test(String.fromCharCode(event.keyCode)))\" onchange=\"NumFormat(this, 0)\" oninput=\"NumFormat(this, 0)\" >");
					else if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("date"))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onchange=\"this.value = this.value.replace(/\\s/g, '')\" oninput=\"this.value = this.value.replace(/\\s/g, '')\" >");
					else if (!string.IsNullOrWhiteSpace(item.type) && (item.type.Contains("float") || item.type.Contains("numeric") || item.type.Contains("decimal")))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" Onkeyup=\"this.value = this.value.toString().match(/ ^\\d{{ 0,14}} (?:\\.\\d{{ 0,5}})?/)\" onchange=\"NumFormat(this, 5)\" oninput=\"NumFormat(this, 5)\" >");
					else
						formArea.AppendLine($"					<input maxlength='{item.length}' name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" >");

					formArea.AppendLine($"				</div>");

				}
				if (residu != 0 && residu != 3)
					formArea.AppendLine($"				<div class=\"layui-col-md{12 - residu * 4} layui-col-sm{12 - residu * 4} \"></div>");
				formArea.AppendLine($"			</div>");

			}

			//替换模板文本
			text = text.Replace("$title$", _viewtitle)
				.Replace("$formArea$", formArea.ToString())
				.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			//生成输出文件
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Views/{_modelName}"))
					Directory.CreateDirectory($"{_buildpath}/Views/{_modelName}");
				File.WriteAllText($"{_buildpath}/Views/{_modelName}/Edit.cshtml", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists($"./Oupput/Views/{_modelName}"))
					Directory.CreateDirectory($"./Oupput/Views/{_modelName}");
				File.WriteAllText($"./Oupput/Views/{_modelName}/Edit.cshtml", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build Views.Details for cms
		/// <summary>
		/// BuildDetailsForCms
		/// </summary>
		public void BuildDetailsForCms()
		{
			string text = FileUtil.GetTplContent("Tpl.cms.SmartTools.Net.Views.details.tpl");

			//查询字段
			StringBuilder formArea = new StringBuilder();
			if (_list.Count > 0)
			{
				var createlist = new List<DbTableInfo>();
				_list.ForEach(x => { if (!FiledUtil.modelSkipFileds.Contains(x.columnName)) createlist.Add(x); });

				FiledUtil.GetRows(createlist, out int totoalrows, out int residu);

				//完整行
				for (int i = 0; i < totoalrows - 1; i++)
				{
					var list = createlist.Skip(3 * i).Take(3);
					if (formArea.Length == 0)
						formArea.AppendLine("<div class=\"layui-row\">");
					else
						formArea.AppendLine("			<div class=\"layui-row\">");
					foreach (var item in list)
					{
						item.columnDescription = item.columnDescription ?? item.columnName;
						formArea.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2 layui-lb\">")
						.AppendLine($"					<label>{item.columnDescription}</label>")
						.AppendLine($"				</div>")
						.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2\">");

						if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("int"))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onkeypress=\"return (/^[\\d]*$/.test(String.fromCharCode(event.keyCode)))\" onchange=\"NumFormat(this, 0)\" oninput=\"NumFormat(this, 0)\" readonly>");
						else if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("date"))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onchange=\"this.value = this.value.replace(/\\s/g, '')\" oninput=\"this.value = this.value.replace(/\\s/g, '')\" readonly>");
						else if (!string.IsNullOrWhiteSpace(item.type) && (item.type.Contains("float") || item.type.Contains("numeric") || item.type.Contains("decimal")))
							formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" Onkeyup=\"this.value = this.value.toString().match(/^\\d{{0,14}}(?:\\.\\d{{0,5}})?/)\" onchange=\"NumFormat(this, 5)\" oninput=\"NumFormat(this, 5)\" readonly >");
						else
							formArea.AppendLine($"					<input maxlength='{item.length}' name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" readonly>");

						formArea.AppendLine($"				</div>");

					}
					formArea.AppendLine("			</div>").AppendLine();
				}

				//不足一行需占位
				var residulist = createlist.Skip(totoalrows - residu).Take(residu);
				formArea.AppendLine("			<div class=\"layui-row\">");
				foreach (var item in residulist)
				{
					item.columnDescription = item.columnDescription ?? item.columnName;
					formArea.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2 layui-lb\">")
					.AppendLine($"					<label>{item.columnDescription}</label>")
					.AppendLine($"				</div>")
					.AppendLine("				<div class=\"layui-col-sm2 layui-col-xs2\">");

					if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("int"))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onkeypress=\"return (/^[\\d]*$/.test(String.fromCharCode(event.keyCode)))\" onchange=\"NumFormat(this, 0)\" oninput=\"NumFormat(this, 0)\" readonly>");
					else if (!string.IsNullOrWhiteSpace(item.type) && item.type.Contains("date"))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" onchange=\"this.value = this.value.replace(/\\s/g, '')\" oninput=\"this.value = this.value.replace(/\\s/g, '')\" readonly>");
					else if (!string.IsNullOrWhiteSpace(item.type) && (item.type.Contains("float") || item.type.Contains("numeric") || item.type.Contains("decimal")))
						formArea.AppendLine($"					<input name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" Onkeyup=\"this.value = this.value.toString().match(/ ^\\d{{ 0,14}} (?:\\.\\d{{ 0,5}})?/)\" onchange=\"NumFormat(this, 5)\" oninput=\"NumFormat(this, 5)\" readonly>");
					else
						formArea.AppendLine($"					<input maxlength='{item.length}' name='{FiledUtil.GetField(item.columnName)}' value='@Model.{FiledUtil.GetField(item.columnName)}' id='{FiledUtil.GetField(item.columnName)}' type='text' class='layui-input' autocomplete=\"off\" >");

					formArea.AppendLine($"				</div>");

				}
				if (residu != 0 && residu != 3)
					formArea.AppendLine($"				<div class=\"layui-col-md{12 - residu * 4} layui-col-sm{12 - residu * 4} \"></div>");
				formArea.AppendLine($"			</div>").AppendLine();

			}

			//替换模板文本
			text = text.Replace("$title$", _viewtitle)
				.Replace("$formArea$", formArea.ToString())
				.Replace("$GenDate$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			//生成输出文件
			if (_outputtype == "项目")
			{
				if (!Directory.Exists($"{_buildpath}/Views/{_modelName}"))
					Directory.CreateDirectory($"{_buildpath}/Views/{_modelName}");
				File.WriteAllText($"{_buildpath}/Views/{_modelName}/Details.cshtml", text, Encoding.UTF8);
			}
			else
			{
				if (!Directory.Exists($"./Oupput/Views/{_modelName}"))
					Directory.CreateDirectory($"./Oupput/Views/{_modelName}");
				File.WriteAllText($"./Oupput/Views/{_modelName}/Details.cshtml", text, Encoding.UTF8);
			}
		}
		#endregion

		#region build xml for import
		/// <summary>
		/// BuildXmlForCms
		/// </summary>
		public void BuildImXmlForCms()
		{
			//生成输出文件
			if (_outputtype != "项目")
			{
				if (!Directory.Exists($"./Oupput/App_Data"))
					Directory.CreateDirectory($"./Oupput/App_Data");
				_xmlpath = "./Oupput/App_Data";
			}

			FileInfo fileInfo= new FileInfo($"{_xmlpath}/GenImport.xml");

			if (!fileInfo.Exists)
            {
				XDocument document = new XDocument();
				document.Declaration = new XDeclaration("1.0", "UTF-8", "");

				XElement root = new XElement("root");

				document.Add(root);

				document.Save($"{_xmlpath}/GenImport.xml");//保存xml到文件
			}


			using FileStream fileStream = new FileStream($"{_xmlpath}/GenImport.xml", FileMode.Open, FileAccess.ReadWrite,FileShare.ReadWrite);

			XElement xelement1 = XElement.Load(fileStream);
			var temp = xelement1.Elements("table").Where( x => x.Attribute("name").Value == _modelName.ToUpper() + "_IMPORT");
            foreach (XElement xelement2 in from m in xelement1.Elements("table")
                                           where m.Attribute("name").Value == _modelName.ToUpper() + "_IMPORT"
                                           select m)
            {
				xelement2.Remove();
            }

            XElement xelement3 = new XElement("table", new XAttribute("name", _modelName.ToUpper() + "_IMPORT"));
			foreach (DbTableInfo item in _list)
			{
				item.columnDescription = item.columnDescription ?? item.columnName;
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					xelement3.Add(new XElement("column", new object[]
					{
						new XAttribute("field", FiledUtil.GetField(item.columnName)),
						new XAttribute("view", item.columnDescription)
					}));
				}
			}
			xelement1.Add(xelement3);
			fileStream.Position = 0L;
			xelement1.Save(fileStream);

		}
		#endregion

		#region build xml for export
		/// <summary>
		/// BuildXmlForCms
		/// </summary>
		public void BuildEpXmlForCms()
		{
			//生成输出文件
			if (_outputtype != "项目")
			{
				if (!Directory.Exists($"./Oupput/App_Data"))
					Directory.CreateDirectory($"./Oupput/App_Data");
				_xmlpath = "./Oupput/App_Data";
			}

			FileInfo fileInfo = new FileInfo($"{_xmlpath}/GenExport.xml");

			if (!fileInfo.Exists)
			{
				XDocument document = new XDocument();
				document.Declaration = new XDeclaration("1.0", "UTF-8", "");

				XElement root = new XElement("root");

				document.Add(root);

				document.Save($"{_xmlpath}/GenExport.xml");//保存xml到文件
			}


			using FileStream fileStream = new FileStream($"{_xmlpath}/GenExport.xml", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

			XElement xelement1 = XElement.Load(fileStream);
			var temp = xelement1.Elements("table").Where(x => x.Attribute("name").Value == _modelName.ToUpper() + "_EXPORT");
			foreach (XElement xelement2 in from m in xelement1.Elements("table")
										   where m.Attribute("name").Value == _modelName.ToUpper() + "_EXPORT"
										   select m)
			{
				xelement2.Remove();
			}

			XElement xelement3 = new XElement("table", new XAttribute("name", _modelName.ToUpper() + "_EXPORT"));
			foreach (DbTableInfo item in _list)
			{
				item.columnDescription = item.columnDescription ?? item.columnName;
				if (!FiledUtil.modelSkipFileds.Contains(item.columnName))
				{
					xelement3.Add(new XElement("column", new object[]
					{
						new XAttribute("field", item.columnName),
						new XAttribute("view", item.columnDescription)
					}));
				}
			}
			xelement1.Add(xelement3);
			fileStream.Position = 0L;
			xelement1.Save(fileStream);

		}
		#endregion
	}
}
