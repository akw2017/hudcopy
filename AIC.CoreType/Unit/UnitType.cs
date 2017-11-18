using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.CoreType
{
    public enum UnitType//  "m/s^2", "mm/s", "um", "Pa", "RPM", "°C", "Unit"
    {

        [Description("m/s^2")]
        Acceleration = 0,
        [Description("mm/s")]
        Velocity,
        [Description("um")]
        Displacement,
        [Description("Pa")]
        Tempature,
        [Description("RPM")]
        Pressure,
        [Description("°C")]
        RPM,
        [Description("Unit")]
        Unit
    }
}
