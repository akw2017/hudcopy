using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HomePage.Models
{
    class SlideChangedEventArgs : EventArgs
    {
        private double _value;

        public SlideChangedEventArgs(double value)
        {
            _value = value;
        }
        public double Value { get { return _value; } }
    }
}
