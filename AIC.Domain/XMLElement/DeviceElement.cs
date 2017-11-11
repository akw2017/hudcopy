using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class DeviceElement
    {
        [XmlAttribute]
        public string OrganizationName { get; set; }        

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public Guid Guid { get; set; }

        public ImageDesignElement ImageDesignElement;
        public DesignElementCollection DesignElements;

        public ConnectionElementCollection ConnectionElements;

        public DeviceElement()
        {
            ImageDesignElement = new ImageDesignElement() { BackgroundImage = "Null.gif" };
            DesignElements = new DesignElementCollection();

            ConnectionElements = new ConnectionElementCollection();
        }
    }

    public class DeviceElementCollection : IList<DeviceElement>
    {
        private List<DeviceElement> _List;

        public DeviceElementCollection()
        {
            _List = new List<DeviceElement>();
            if (Equipments != null)
            {
                _List.AddRange(Equipments);
            }
        }

        [XmlElement(ElementName = "DeviceElement")]
        public DeviceElement[] Equipments;

        public DeviceElement this[int index]
        {
            get { return _List[index]; }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException("The specified index is out of range.");
                var oldItem = _List[index];
                _List[index] = value;
            }
        }

        [XmlIgnore]
        public int Count { get { return _List.Count; } }

        public int IndexOf(DeviceElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<DeviceElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, DeviceElement item)
        {
            _List.Insert(index, item);
            Equipments = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            Equipments = _List.ToArray();
        }

        public void Add(DeviceElement item)
        {
            _List.Add(item);
            Equipments = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            Equipments = _List.ToArray();
        }

        public bool Contains(DeviceElement item)
        {
            return _List.Contains(item);
        }

        public bool Contains(string equipment)
        {
            return _List.Select(o => o.Name).ToArray().Contains(equipment);
        }

        public void CopyTo(DeviceElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(DeviceElement item)
        {
            var result = _List.Remove(item);
            Equipments = _List.ToArray();
            return result;
        }

        public IEnumerator<DeviceElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public DeviceElement GetOrAdd(string name, string sn)
        {
            if (Contains(name + sn))
            {
                return _List.Where(o => o.Name == name).Single();
            }
            else
            {
                DeviceElement equipment = new DeviceElement() { Name = name};
                _List.Add(equipment);
                Equipments = _List.ToArray();
                return equipment;
            }
        }
    }
}
