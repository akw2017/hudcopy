using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AIC.Core
{
    public class DisposableUserControl : UserControl, IDisposable
    {
        protected DisposableUserControl()
        {

        }

        ~DisposableUserControl()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //执行基本的清理代码
            }
        }

        static DisposableUserControl()
        {
            //GCCollectChanged.Throttle(TimeSpan.FromMilliseconds(1000)).Subscribe(GCCollected);
            GCCollectChanged.Sample(TimeSpan.FromMilliseconds(1000)).Subscribe(GCCollected);
        }


        public void GCCollect()
        {
            if (gcCollectChanged != null)
            {
                gcCollectChanged(this, null);
            }
        }

        private static void GCCollected(EventArgs args)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static event EventHandler<EventArgs> gcCollectChanged;
        public static IObservable<EventArgs> GCCollectChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<EventArgs>(
                        h => gcCollectChanged += h,
                        h => gcCollectChanged -= h)
                   .Select(x => x.EventArgs);
            }
        }
    }
}
