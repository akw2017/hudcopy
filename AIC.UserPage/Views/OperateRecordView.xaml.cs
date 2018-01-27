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

namespace AIC.UserPage.Views
{
    /// <summary>
    /// Interaction logic for ServerSetView.xaml
    /// </summary>
    public partial class OperateRecord : UserControl, ICloseable
    {
        public OperateRecord()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuManageLog");
            this.Closer = new CloseableHeader("menuManageLog", menu.Name, true, menu.IconPath);
        }
        public CloseableHeader Closer { get; private set; }
    }
}
