using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HistoryDataPage.Models
{
    class TrendTrackChangedEventArgs : EventArgs
    {
        private IEnumerable<BaseWaveSignalToken> _tokens;

        public TrendTrackChangedEventArgs(IEnumerable<BaseWaveSignalToken> tokens)
        {
            _tokens = tokens;
        }
        public IEnumerable<BaseWaveSignalToken> Tokens { get { return _tokens; } }
    }
}
