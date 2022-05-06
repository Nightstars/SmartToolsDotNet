using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UserUI.Utils
{
    public class ResourceUtil
    {
        #region GetImage
        /// <summary>
        /// GetImage
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static BitmapSource GetImage(string path)
        {
            var modulename = Assembly.GetExecutingAssembly().GetName().Name;
            //格式为：项目名称-文件夹地址-文件名称
            using Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{modulename}.Resources.{path}");
            //图片格式
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = myStream;
            image.EndInit();
            return image;
        }
        #endregion

        #region GetTmpContent
        /// <summary>
        /// GetTplContent
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetTplContent(string path)
        {
            var modulename = Assembly.GetExecutingAssembly().GetName().Name;
            string result;
            using Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{modulename}.Resources.{path}");
            using StreamReader streamReader = new StreamReader(manifestResourceStream);
            result = streamReader.ReadToEnd();

            return result;
        }
        #endregion
    }
}
