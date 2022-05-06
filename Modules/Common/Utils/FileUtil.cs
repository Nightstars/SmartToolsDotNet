using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SmartToolsDotNet.Utils
{
    public static class FileUtil
    {
        #region initialize
        public static string ResourcesPath = $"AppCommonModule.Resources.";
		#endregion

		#region GetTmpContent
		/// <summary>
		/// GetTplContent
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string GetTplContent(string path)
		{
			string result;
			//using StreamReader streamReader = new StreamReader(path);
			//result = streamReader.ReadToEnd();
			using Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{ResourcesPath}{path}");
				using StreamReader streamReader = new StreamReader(manifestResourceStream);
					result = streamReader.ReadToEnd();

			return result;
		}
		#endregion

		#region GetImage
		/// <summary>
		/// GetImage
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static BitmapSource GetImage(string path)
		{
			//格式为：项目名称-文件夹地址-文件名称
			using Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{ResourcesPath}{path}");
			//图片格式
			BitmapImage image = new BitmapImage();
			image.BeginInit();
			image.StreamSource = myStream;
			image.EndInit();
			return image;
		}
		#endregion
	}
}
