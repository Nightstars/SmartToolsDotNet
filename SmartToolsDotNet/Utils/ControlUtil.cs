using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartToolsDotNet.Utils
{
    public class ControlUtil
    {
        #region HideBoundingBox
        /// <summary>
        /// 隐藏控件获得焦点时的虚线框
        /// </summary>
        /// <param name="root"></param>
        public void HideBoundingBox(object root)
        {
            Control control = root as Control;
            if (control != null)
            {
                control.FocusVisualStyle = null;
            }

            if (root is DependencyObject)
            {
                foreach (object child in LogicalTreeHelper.GetChildren((DependencyObject)root))
                {
                    HideBoundingBox(child);
                }
            }
        }
        #endregion
    }
}
