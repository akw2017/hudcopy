using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class DesignElement
    {
        public string ID { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class DesignElementCollection : IList<DesignElement>
    {
        private List<DesignElement> _List;

        public DesignElementCollection()
        {
            _List = new List<DesignElement>();
            if (AnalogInElements != null)
            {
                _List.AddRange(AnalogInElements);
            }
        }

        [XmlElement(ElementName = "DesignElement")]
        public DesignElement[] AnalogInElements;

        public DesignElement this[int index]
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

        public int IndexOf(DesignElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<DesignElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, DesignElement item)
        {
            _List.Insert(index, item);
            AnalogInElements = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            AnalogInElements = _List.ToArray();
        }

        public void Add(DesignElement item)
        {
            _List.Add(item);
            AnalogInElements = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            AnalogInElements = _List.ToArray();
        }

        public bool Contains(DesignElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(DesignElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(DesignElement item)
        {
            var result = _List.Remove(item);
            AnalogInElements = _List.ToArray();
            return result;
        }

        public IEnumerator<DesignElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
