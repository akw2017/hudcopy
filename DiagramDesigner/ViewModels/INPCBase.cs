using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reactive.Linq;

namespace DiagramDesigner
{
    public abstract class INPCBase : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { this.propertyChanged += value; }
            remove { this.propertyChanged -= value; }
        }

        protected event PropertyChangedEventHandler propertyChanged;

        public IObservable<string> WhenPropertyChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        h => this.propertyChanged += h,
                        h => this.propertyChanged -= h)
                    .Select(x => x.EventArgs.PropertyName);
            }
        }

        public virtual void NotifyChanged(params string[] propertyNames)
        {
            foreach (string name in propertyNames)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(name));
            }
        }


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eventHandler = this.propertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }
    }
}
