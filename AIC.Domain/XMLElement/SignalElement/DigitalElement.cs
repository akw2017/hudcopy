using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class DigitalElement : DesignElement
    {
        public DigitalElement()
        {
        }
    }

    public class DigitalElementCollection : IList<DigitalElement>
    {
        private List<DigitalElement> _List;

        public DigitalElementCollection()
        {
            _List = new List<DigitalElement>();
            if (DigitalElements != null)
            {
                _List.AddRange(DigitalElements);
            }
        }

        [XmlElement(ElementName = "DigitalElement")]
        public DigitalElement[] DigitalElements;

        public DigitalElement this[int index]
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

        public int IndexOf(DigitalElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<DigitalElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, DigitalElement item)
        {
            _List.Insert(index, item);
            DigitalElements = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            DigitalElements = _List.ToArray();
        }

        public void Add(DigitalElement item)
        {
            _List.Add(item);
            DigitalElements = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            DigitalElements = _List.ToArray();
        }

        public bool Contains(DigitalElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(DigitalElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(DigitalElement item)
        {
            var result = _List.Remove(item);
            DigitalElements = _List.ToArray();
            return result;
        }

        public IEnumerator<DigitalElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
