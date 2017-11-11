using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class EquipmentElement
    {
        [XmlAttribute]
        public string GroupCOName { get; set; }

        [XmlAttribute]
        public string CorproationName { get; set; }

        [XmlAttribute]
        public string WorkshopName { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string SN { get; set; }

        public ImageDesignElement ImageDesignElement;
        public DesignElementCollection DesignElements;

        public ConnectionElementCollection ConnectionElements;
        public VibrationElementCollection VibrationElements;
        public AnalogInElementCollection AnalogInElements;
        public DigitalElementCollection DigitalElements;
        public CompositionElementCollection CompositionElements;
        public VedioElementCollection VedioElements;

        public EquipmentElement()
        {
            ImageDesignElement = new ImageDesignElement() { BackgroundImage = "Null.gif" };
            DesignElements = new DesignElementCollection();

            ConnectionElements = new ConnectionElementCollection();
            VibrationElements = new VibrationElementCollection();
            AnalogInElements = new AnalogInElementCollection();
            DigitalElements = new DigitalElementCollection();
            CompositionElements = new CompositionElementCollection();
            VedioElements = new VedioElementCollection();
        }
    }

    public class EquipmentElementCollection:IList<EquipmentElement>
    {
        private List<EquipmentElement> _List;

        public EquipmentElementCollection()
        {
            _List = new List<EquipmentElement>();
            if (Equipments != null)
            {
                _List.AddRange(Equipments);
            }
        }

        [XmlElement(ElementName = "EquipmentElement")]
        public EquipmentElement[] Equipments;

        public EquipmentElement this[int index]
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

        public int IndexOf(EquipmentElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<EquipmentElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, EquipmentElement item)
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

        public void Add(EquipmentElement item)
        {
            _List.Add(item);
            Equipments = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            Equipments = _List.ToArray();
        }

        public bool Contains(EquipmentElement item)
        {
            return _List.Contains(item);
        }

        public bool Contains(string equipment)
        {
            return _List.Select(o => o.Name + o.SN).ToArray().Contains(equipment);
        }

        public void CopyTo(EquipmentElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(EquipmentElement item)
        {
            var result = _List.Remove(item);
            Equipments = _List.ToArray();
            return result;
        }

        public IEnumerator<EquipmentElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public EquipmentElement GetOrAdd(string name, string sn)
        {
            if (Contains(name + sn))
            {
                return _List.Where(o => o.Name == name && o.SN == sn).Single();
            }
            else
            {
                EquipmentElement equipment = new EquipmentElement() { Name = name, SN = sn };
                _List.Add(equipment);
                Equipments = _List.ToArray();
                return equipment;
            }
        }
    }
}
