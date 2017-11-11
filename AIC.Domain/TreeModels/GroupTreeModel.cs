namespace AIC.Domain
{
    public class GroupTreeModel : TreeViewItemModel
    {
        public GroupTreeModel(string name): base(name)
        {
            IsExpanded = true;
        }
    }
}