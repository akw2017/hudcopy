namespace AIC.Domain
{
    public class CorporationTreeModel : TreeViewItemModel
    {
        public CorporationTreeModel(string name): base(name)
        {
            IsExpanded = true;
        }
    }
}