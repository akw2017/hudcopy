using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Domain
{
    //public class ChannelSampleData
    //{
    //    private List<ChannelDataContract2> list = new List<ChannelDataContract2>();

    //    public IEnumerable<ChannelDataContract2> Data { get { return list; } }
    //    public ChannelSampleData()
    //    {
    //        list.Add(new ChannelDataContract2()
    //        {
    //            Id = Guid.Parse("9851292A-BB57-43A8-B08B-928E725AC368"),
    //            GroupCOName = "总厂#1",
    //            CorporationName = "分厂#1",
    //            WorkShopName = "车间#1",
    //            T_Device_Name = "虚拟设备",
    //            DevSN = "#1",
    //            Name = "测点",
    //            MSSN = "#1",
    //            IP = "192.168.1.210",
    //            SlotNum = 1,
    //            ChannelNum = 1,
    //        });

    //        list.Add(new ChannelDataContract2()
    //        {
    //            Id = Guid.Parse("3E196A9F-A58C-475C-B79B-1408063A145B"),
    //            GroupCOName = "总厂#1",
    //            CorporationName = "分厂#1",
    //            WorkShopName = "车间#1",
    //            T_Device_Name = "虚拟设备",
    //            DevSN = "#1",
    //            Name = "测点",
    //            MSSN = "#2",
    //            IP = "192.168.1.210",
    //            SlotNum = 1,
    //            ChannelNum = 2,
    //        });

    //        list.Add(new ChannelDataContract2()
    //        {
    //            Id = Guid.Parse("556EC155-E87B-413B-9F0A-3E1722495F43"),
    //            GroupCOName = "总厂#1",
    //            CorporationName = "分厂#1",
    //            WorkShopName = "车间#1",
    //            T_Device_Name = "虚拟设备",
    //            DevSN = "#1",
    //            Name = "测点",
    //            MSSN = "#3",
    //            IP = "192.168.1.210",
    //            SlotNum = 1,
    //            ChannelNum = 3,
    //        });

    //        list.Add(new ChannelDataContract2()
    //        {
    //            Id = Guid.Parse("04AE1BC9-7F7C-432B-A6ED-6006651A4F83"),
    //            GroupCOName = "总厂#1",
    //            CorporationName = "分厂#1",
    //            WorkShopName = "车间#1",
    //            T_Device_Name = "虚拟设备",
    //            DevSN = "#1",
    //            Name = "测点",
    //            MSSN = "#4",
    //            IP = "192.168.1.210",
    //            SlotNum = 1,
    //            ChannelNum = 4,
    //        });

    //        //list.Add(new ChannelDataContract2()
    //        //{
    //        //    Id = Guid.NewGuid(),
    //        //    GroupCOName = "总厂",
    //        //    CorporationName = "分厂",
    //        //    WorkShopName = "车间",
    //        //    T_Device_Name = "设备",
    //        //    DevSN = "1",
    //        //    Name = "测点",
    //        //    MSSN = "2",
    //        //    IP = "192.168.1.110",
    //        //    SlotNum = 1,
    //        //    ChannelNum = 2,
    //        //});

    //        //list.Add(new ChannelDataContract2()
    //        //{
    //        //    Id = Guid.NewGuid(),
    //        //    GroupCOName = "总厂",
    //        //    CorporationName = "分厂",
    //        //    WorkShopName = "车间1",
    //        //    T_Device_Name = "设备",
    //        //    DevSN = "1",
    //        //    Name = "测点",
    //        //    MSSN = "2",
    //        //    IP = "192.168.1.110",
    //        //    SlotNum = 1,
    //        //    ChannelNum = 2,
    //        //});

    //        //list.Add(new ChannelDataContract2()
    //        //{
    //        //    Id = Guid.NewGuid(),
    //        //    GroupCOName = "总厂",
    //        //    CorporationName = "分厂",
    //        //    WorkShopName = "车间",
    //        //    T_Device_Name = "设备1",
    //        //    DevSN = "1",
    //        //    Name = "测点",
    //        //    MSSN = "1",
    //        //    IP = "192.168.1.110",
    //        //    SlotNum = 1,
    //        //    ChannelNum = 3,
    //        //});

    //        //list.Add(new ChannelDataContract2()
    //        //{
    //        //    Id = Guid.NewGuid(),
    //        //    GroupCOName = "总厂",
    //        //    CorporationName = "分厂",
    //        //    WorkShopName = "车间",
    //        //    T_Device_Name = "设备1",
    //        //    DevSN = "1",
    //        //    Name = "测点",
    //        //    MSSN = "2",
    //        //    IP = "192.168.1.110",
    //        //    SlotNum = 2,
    //        //    ChannelNum = 1,
    //        //});

    //        //list.Add(new ChannelDataContract2()
    //        //{
    //        //    Id = Guid.NewGuid(),
    //        //    GroupCOName = "总厂",
    //        //    CorporationName = "分厂",
    //        //    WorkShopName = "车间",
    //        //    T_Device_Name = "设备3",
    //        //    DevSN = "1",
    //        //    Name = "测点",
    //        //    MSSN = "1",
    //        //    IP = "192.168.1.110",
    //        //    SlotNum = 2,
    //        //    ChannelNum = 2,
    //        //});

    //        //list.Add(new ChannelDataContract2()
    //        //{
    //        //    Id = Guid.NewGuid(),
    //        //    GroupCOName = "总厂",
    //        //    CorporationName = "分厂",
    //        //    WorkShopName = "车间",
    //        //    T_Device_Name = "设备3",
    //        //    DevSN = "1",
    //        //    Name = "测点1",
    //        //    MSSN = "1",
    //        //    IP = "192.168.1.110",
    //        //    SlotNum = 2,
    //        //    ChannelNum = 3,
    //        //});
    //        //list.Add(new ChannelDataContract2()
    //        //{
    //        //    Id = Guid.NewGuid(),
    //        //    GroupCOName = "总厂",
    //        //    CorporationName = "分厂",
    //        //    WorkShopName = "车间",
    //        //    T_Device_Name = "设备3",
    //        //    DevSN = "1",
    //        //    Name = "测点3",
    //        //    MSSN = "1",
    //        //    IP = "192.168.1.110",
    //        //    SlotNum = 2,
    //        //    ChannelNum = 4,
    //        //});
    //    }

    //}
}
