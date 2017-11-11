using System.Linq;
using AIC.CoreType;
using AIC.Domain;

namespace AIC.Domain
{
    public class CardTreeModel : TreeViewItemModel
    {
        public CardTreeModel(string ip, string slotNum): base("采集卡")
        {
            IP = ip;
            SlotNum = slotNum;
            IsExpanded = false;
        }

        public string IP { get;}

        private string _slotNum;
        public string SlotNum
        {
            get { return _slotNum; }
            set
            {
                _slotNum = value;
                Name.Value = string.Format("采集卡{0}", _slotNum);
                //if (value == 0)
                //{
                //    Name.Value = "虚拟卡";
                //}
                //else
                //{
                //    Name.Value = string.Format("采集卡{0}", _slotNum);
                //}
            }
        }
    }
}