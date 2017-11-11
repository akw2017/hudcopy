

using AIC.CoreType;

namespace AIC.Domain
{
    public class DivFreTreeModel : TreeViewItemModel
    {
        public DivFreTreeModel(string name) : base(name)
        {
        }

        //private string name;
        //public override string Name
        //{
        //    get { return name; }
        //    set
        //    {
        //        if (name != value)
        //        {
        //            AdvancedPropertyChangedEventArgs args = new AdvancedPropertyChangedEventArgs(this, "Name", name, value);
        //            name = value;
        //            OnPropertyChanged("Name");
        //            RaiseTreeViewItemNameChanged(args);

        //            Location[7] = value;
        //        }
        //    }
        //}

        private AlarmGrade alarm = AlarmGrade.HighNormal;
        public override AlarmGrade Alarm
        {
            get { return alarm; }
            set
            {
                if (alarm != value)
                {
                    alarm = value;
                    OnPropertyChanged("Alarm");
                }
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    OnPropertyChanged("IsConnected");
                }
            }
        }
    }
}