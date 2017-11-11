using System.Linq;
using AIC.CoreType;

namespace AIC.Domain
{
    public class PDATreeModel : TreeViewItemModel
    {
        public PDATreeModel(string ip) : base(ip)
        {
            IsExpanded = false;
            IP = ip;
        }

        #region Property IP
        private string _ip = "0.0.0.0";
        public string IP
        {
            get { return _ip; }
            set
            {
                if (value != _ip)
                {
                    _ip = value;
                    OnPropertyChanged("IP");
                    
                }
            }
        }
        #endregion
    }
}