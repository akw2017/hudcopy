using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIC.Core.ControlModels
{
    public class ContextMenuViewModel
    {
        public string Header { get; set; }

        public string Icon { get; set; }

        public ICommand Command { get; set; }
    }
}
