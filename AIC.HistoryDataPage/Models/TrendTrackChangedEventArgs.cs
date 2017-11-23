using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HistoryDataPage.Models
{
    class TrendTrackChangedEventArgs : EventArgs
    {
        private IEnumerable<SignalToken> _tokens;

        public TrendTrackChangedEventArgs(IEnumerable<SignalToken> tokens)
        {
            _tokens = tokens;
        }
        public IEnumerable<SignalToken> Tokens { get { return _tokens; } }
    }
}
