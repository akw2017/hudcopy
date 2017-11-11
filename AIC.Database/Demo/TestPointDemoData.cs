

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class TestPointDemoData : IRepository<TestPoint>
    {
        public List<TestPoint> list;
        public TestPointDemoData()
        {
            list = new List<TestPoint>();
            list.Add(new TestPoint()
            {
                TestPointId = Guid.Parse("9851292A-BB57-43A8-B08B-928E725AC368"),
                GroupCOName = "总厂#1",
                CorporationName = "分厂#1",
                WorkShopName = "车间#1",
                DevName = "虚拟设备",
                DevSN = "#1",
                Name = "测点",
                MSSN = "#1",
                IP = "192.168.1.210",
                CardNum = "1",
                ChannelNum = "1",
            });

            list.Add(new TestPoint()
            {
                TestPointId = Guid.Parse("3E196A9F-A58C-475C-B79B-1408063A145B"),
                GroupCOName = "总厂#1",
                CorporationName = "分厂#1",
                WorkShopName = "车间#1",
                DevName = "虚拟设备",
                DevSN = "#1",
                Name = "测点",
                MSSN = "#2",
                IP = "192.168.1.210",
                CardNum = "1",
                ChannelNum = "2",
            });

            list.Add(new TestPoint()
            {
                TestPointId = Guid.Parse("556EC155-E87B-413B-9F0A-3E1722495F43"),
                GroupCOName = "总厂#1",
                CorporationName = "分厂#1",
                WorkShopName = "车间#1",
                DevName = "虚拟设备",
                DevSN = "#1",
                Name = "测点",
                MSSN = "#3",
                IP = "192.168.1.210",
                CardNum = "1",
                ChannelNum = "3",
            });

            list.Add(new TestPoint()
            {
                TestPointId = Guid.Parse("04AE1BC9-7F7C-432B-A6ED-6006651A4F83"),
                GroupCOName = "总厂#1",
                CorporationName = "分厂#1",
                WorkShopName = "车间#1",
                DevName = "虚拟设备",
                DevSN = "#1",
                Name = "测点",
                MSSN = "#4",
                IP = "192.168.1.210",
                CardNum = "1",
                ChannelNum = "4",
            });
        }

        public void Add(TestPoint newEntity)
        {
            list.Add(newEntity);
        }

        public void Delete(int id)
        {
            var item = list.Where(o => o.id == id).SingleOrDefault();
            if (list.Contains(item))
            {
                list.Remove(item);
            }
        }

        public IEnumerable<TestPoint> Find(Func<TestPoint, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<TestPoint> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<TestPoint> Query()
        {
            return list.ToArray();
            //throw new NotImplementedException();
        }

        public IEnumerable<TestPoint> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<TestPoint> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Remove(TestPoint entity)
        {
            if(list.Contains(entity))
            {
                list.Remove(entity);
            }
        }

        public void Update(int id, TestPoint entity)
        {
            throw new NotImplementedException();
        }

    }
}
