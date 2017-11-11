using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    public class ExceptionModel
    {
        private string _target;
        private string _exceptionType;
        private string _message;
        private string _stackTrace;
        private string _data;
        private string _targetSite;
        private string _source;

        public ExceptionModel(string target, string exceptionType, string message, string stackTrace, string data, string targetSite, string source)
        {
            _target = target;
            _exceptionType = exceptionType;
            _message = message;
            _stackTrace = stackTrace;
            _data = data;
            _targetSite = targetSite;
            _source = source;
        }
        public string Target { get { return _target; } }
        public string ExceptionType { get { return _exceptionType; } }
        public string Message { get { return _message; } }
        public string StackTrace { get { return _stackTrace; } }
        public string Data { get { return _data; } }
        public string TargetSite { get { return _targetSite; } }
        public string Source { get { return _source; } }
    }
}
