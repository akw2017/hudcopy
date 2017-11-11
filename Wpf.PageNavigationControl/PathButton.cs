using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Wpf.PageNavigationControl
{
    public partial class PathButton : Button
    {
        public static readonly DependencyProperty PathDataProperty =
          DependencyProperty.Register("PathData", typeof(string), typeof(PathButton), new PropertyMetadata("123"));
        /// <summary>
        /// 按钮字体图标编码
        /// </summary>
        public string PathData
        {
            get { return (string)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }
    }
}
