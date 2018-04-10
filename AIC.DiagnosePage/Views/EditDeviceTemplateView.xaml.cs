using AIC.Core;
using AIC.Core.UserManageModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Wpf.CloseTabControl;

namespace AIC.DiagnosePage.Views
{
    /// <summary>
    /// EditDeviceTemplateView.xaml 的交互逻辑
    /// </summary>
    public partial class EditDeviceTemplateView : DisposableUserControl, ICloseable
    {
        public EditDeviceTemplateView()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuEditDeviceTemplate");
            this.Closer = new CloseableHeader("menuEditDeviceTemplate", menu.Name, true, menu.IconPath);
        }

        public CloseableHeader Closer { get; private set; }
    }
}
