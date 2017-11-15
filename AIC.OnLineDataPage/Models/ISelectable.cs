using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.OnLineDataPage.Models
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
        double ItemWidth { get; set; }
        double ItemHeight { get; set; }
    }
}
