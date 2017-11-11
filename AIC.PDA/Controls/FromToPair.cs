using Prism.Mvvm;

namespace AIC.PDA.Controls
{
    public class FromToPair:BindableBase
    {
        private double from;
        public double From
        {
            get { return from; }
            set
            {
                if (from != value)
                {
                    from = value;

                    OnPropertyChanged("From");
                }
            }
        }

        private double to;
        public double To
        {
            get { return to; }
            set
            {
                if (to != value)
                {
                    to = value;
                    OnPropertyChanged("To");
                }
            }
        }
    }
}
