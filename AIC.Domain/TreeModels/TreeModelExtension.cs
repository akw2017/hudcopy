using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Domain.TreeModel
{
    public static class TreeModelExtension
    {
        public static void BuildGroupTrees(this IEnumerable<TestPointTreeModel> testPoints)
        {
            //var groupTMs = testPoints.GroupBy(o => o.Location[0], o => o);
            //foreach (var g in groupTMs)
            //{
            //    GroupTreeModel group = groupTMList.Where(o => o.Name == g.Key).SingleOrDefault();
            //    if (group == null)
            //    {
            //        group = new GroupTreeModel(g.Key);
            //        group.TreeViewItemNameChanged += TreeViewItemNameChanged;
            //        groupTMList.Add(group);
            //    }

            //    var corporations = g.GroupBy(o => o.Location[1], o => o);
            //    foreach (var corp in corporations)
            //    {
            //        CorporationTreeModel corporation = group.Children.Where(o => o.Name == corp.Key).SingleOrDefault() as CorporationTreeModel;
            //        if (corporation == null)
            //        {
            //            corporation = new CorporationTreeModel(corp.Key);
            //            corporation.TreeViewItemNameChanged += TreeViewItemNameChanged;
            //            group.AddChild(corporation);
            //        }
            //        var worpshops = corp.GroupBy(o => o.Location[2], o => o);
            //        foreach (var ws in worpshops)
            //        {
            //            WorkShopTreeModel workshop = corporation.Children.Where(o => o.Name == ws.Key).SingleOrDefault() as WorkShopTreeModel;
            //            if (workshop == null)
            //            {
            //                workshop = new WorkShopTreeModel(ws.Key);
            //                workshop.TreeViewItemNameChanged += TreeViewItemNameChanged;
            //                corporation.AddChild(workshop);
            //            }

            //            var equips = ws.OrderBy(o => OrderFunc(o)).OrderBy(o => o.Location[3]).GroupBy(o => o.Location[3] + "|" + o.Location[4], o => o);
            //            foreach (var equip in equips)
            //            {
            //                EquipmentTreeModel equipment = workshop.Children.Where(o => o.Name == equip.Key.Split('|')[0] && ((EquipmentTreeModel)o).MSSN == equip.Key.Split('|')[1]).SingleOrDefault() as EquipmentTreeModel;
            //                if (equipment == null)
            //                {
            //                    equipment = new EquipmentTreeModel(equip.Key.Split('|')[0], equip.Key.Split('|')[1]);
            //                    equipment.TreeViewItemNameChanged += TreeViewItemNameChanged;
            //                    workshop.AddChild(equipment);
            //                }
            //                equipment.AddChildren(equip.ToArray());
            //            }
            //        }
            //    }
            //}
        }
    }
}
