using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIC.Core
{
    interface IFluidWrapPanel: IInputElement
    {
        void BeginFluidDrag(UIElement child, Point position);
        void FluidDrag(UIElement child, Point position, Point positionInParent);
        void EndFluidDrag(UIElement child, Point position, Point positionInParent);
    }
}
