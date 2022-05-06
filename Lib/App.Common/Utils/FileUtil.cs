using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Utils
{
    public class FileUtil
    {
        #region 获取文件夹大小
        /// <summary>
        /// 获取某一文件夹的大小
        /// </summary>
        /// <param name="dir">文件夹目录</param>
        /// <param name="dirSize">文件夹大小</param>
        public static void GetDirSizeByPath(string dir, ref long dirSize)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                DirectoryInfo[] dirs = dirInfo.GetDirectories();
                FileInfo[] files = dirInfo.GetFiles();

                foreach (var item in dirs)
                {
                    GetDirSizeByPath(item.FullName, ref dirSize);
                }

                foreach (var item in files)
                {
                    dirSize += item.Length;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取文件大小失败" + ex.Message);
            }

        }
        #endregion

        #region 调用默认程序打开指定文件
        public static void OpenFileByDefaultApp(string fileName)
        {
            Process.Start("explorer.exe", fileName);
        }
        #endregion

    }
}
