﻿using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Common
{
    public class LogFactory
    {
        public static ILog Get()
        {
            return LogManager.GetLogger(new StackFrame(1).GetMethod().DeclaringType);
        }
    }
}
