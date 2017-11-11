using Prism.Mvvm;

namespace AIC.Domain
{
    public class BindableValue<T> : BindableBase
    {
        public BindableValue()
        {
            Value = default(T);
        }

        public BindableValue(T value)
        {
            Value = value;
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
    }
}
