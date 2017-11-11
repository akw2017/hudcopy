using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class AnalogInElement : DesignElement
    {      
        public AnalogInElement()
        {
        }
    }

    public class AnalogInElementCollection : IList<AnalogInElement>
    {
        private List<AnalogInElement> _List;

        public AnalogInElementCollection()
        {
            _List = new List<AnalogInElement>();
            if (AnalogInElements != null)
            {
                _List.AddRange(AnalogInElements);
            }
        }

        [XmlElement(ElementName = "AnalogInElement")]
        public AnalogInElement[] AnalogInElements;

        public AnalogInElement this[int index]
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

        public int IndexOf(AnalogInElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<AnalogInElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, AnalogInElement item)
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

        public void Add(AnalogInElement item)
        {
            _List.Add(item);
            AnalogInElements = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            AnalogInElements = _List.ToArray();
        }

        public bool Contains(AnalogInElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(AnalogInElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(AnalogInElement item)
        {
            var result = _List.Remove(item);
            AnalogInElements = _List.ToArray();
            return result;
        }

        public IEnumerator<AnalogInElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
