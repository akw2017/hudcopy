using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class WaveElement : DesignElement
    {
        public DivFreElementCollection DivFreAlarms;
        public WaveElement()
        {
            DivFreAlarms = new DivFreElementCollection(); 
        }
    }

    public class WaveElementCollection : IList<WaveElement>
    {
        private List<WaveElement> _List;

        public WaveElementCollection()
        {
            _List = new List<WaveElement>();
            if (WaveElement != null)
            {
                _List.AddRange(WaveElement);
            }
        }

        [XmlElement(ElementName = "WaveElement")]
        public WaveElement[] WaveElement;

        public WaveElement this[int index]
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

        public int IndexOf(WaveElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<WaveElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, WaveElement item)
        {
            _List.Insert(index, item);
            WaveElement = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            WaveElement = _List.ToArray();
        }

        public void Add(WaveElement item)
        {
            _List.Add(item);
            WaveElement = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            WaveElement = _List.ToArray();
        }

        public bool Contains(WaveElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(WaveElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(WaveElement item)
        {
            var result = _List.Remove(item);
            WaveElement = _List.ToArray();
            return result;
        }

        public IEnumerator<WaveElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
