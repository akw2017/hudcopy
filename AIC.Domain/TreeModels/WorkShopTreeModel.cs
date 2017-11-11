namespace AIC.Domain
{
    public class WorkShopTreeModel : TreeViewItemModel
    {
        public WorkShopTreeModel(string name)
            : base(name)
        {
            IsExpanded = true;
        }
    }
}