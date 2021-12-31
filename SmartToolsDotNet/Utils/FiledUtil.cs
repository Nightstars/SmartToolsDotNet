using SmartToolsDotNet.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartToolsDotNet.Utils
{
    public static class FiledUtil
    {
		#region modelSkipFileds
		public static List<string> modelSkipFileds = new() {
			"UPDATE_DATE",
			"UPDATE_USER_NAME",
			"UPDATE_USER",
			"CREATE_DATE",
			"CREATE_USER_NAME",
			"CREATE_USER",
			"SITE",
			"REF_SEQ_NO",
			"SEQ_NO"
		};
		#endregion

		#region insertSkipFileds
		public static List<string> insertSkipFileds = new()
		{
			"UPDATE_DATE",
			"UPDATE_USER_NAME",
			"UPDATE_USER"
		};
		#endregion

		#region updateSkipFileds
		public static List<string> updateSkipFileds = new()
		{
			"CREATE_DATE",
			"CREATE_USER_NAME",
			"CREATE_USER",
			"SITE",
			"REF_SEQ_NO",
			"SEQ_NO"
		};
		#endregion

		#region GetFiledType
		/// <summary>
		/// GetFiledType
		/// </summary>
		/// <param name="dbType"></param>
		/// <returns></returns>
		public static string GetFiledType(string dbType)
        {
			if(dbType == "int")
            {
				return "int?";
            }
			else if(dbType == "datetime")
            {
				return "DateTime?";
			}
			else if(dbType == "float" || dbType == "numeric" ||dbType == "decimal")
            {
				return "Decimal?";

			}
            else
            {
				return "string";
            }
        }
		#endregion

		#region GetField
		/// <summary>
		/// GetField
		/// </summary>
		/// <param name="filed"></param>
		/// <returns></returns>
		public static string GetField(string filed)
		{
			if (filed == null)
			{
				return null;
			}
			string[] array = filed.Split(new char[]
			{
				'_'
			});
			List<string> list = (array != null) ? array.ToList<string>() : null;
			StringBuilder sb = new StringBuilder();
			list.ForEach(x => sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.ToLower())));
			return sb.ToString();
		}
		#endregion

		#region GetModelName
		/// <summary>
		/// FieldHandler
		/// </summary>
		/// <param name="tbname"></param>
		/// <returns></returns>
		public static string GetModelName(string tbname)
		{
			if (tbname == null)
			{
				return null;
			}
			if (tbname.Contains("_"))
			{
				string[] array = tbname.Split(new char[]
				{
					'_'
				});
				List<string> list = (array != null) ? array.ToList<string>() : null;
				StringBuilder sb = new StringBuilder();
				list.ForEach(delegate (string m)
				{
					sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(m.ToLower()));
				});
				return sb.ToString();
			}
			else
			{
				return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbname.ToLower());
			}

		}
		#endregion

		#region GetRows
		/// <summary>
		/// GetRows
		/// </summary>
		/// <param name="list"></param>
		/// <param name="totoalrows"></param>
		/// <param name="residu"></param>
		public static void GetRows(List<DbTableInfo> list, out int totoalrows,out int residu)
        {
			if(list.Count != 0)
            {
				totoalrows = int.Parse(Math.Ceiling((double)list.Count / 4).ToString());
				residu = list.Count % 4;

            }
            else
            {
				totoalrows = 0;
				residu = 0;
            }
        }
        #endregion
    }
}
