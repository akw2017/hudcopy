using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class ConnectorElement
    {
        public string ParentID { get; set; }
        public string Orientation { get; set; }
        public double XRatio { get; set; }
        public double YRatio { get; set; }
    }

    public class ConnectorElementCollection : IList<ConnectorElement>
    {
        private List<ConnectorElement> _List;

        public ConnectorElementCollection()
        {
            _List = new List<ConnectorElement>();
            if (ConnectorElements != null)
            {
                _List.AddRange(ConnectorElements);
            }
        }

        [XmlElement(ElementName = "ConnectorElement")]
        public ConnectorElement[] ConnectorElements;

        public ConnectorElement this[int index]
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

        public int IndexOf(ConnectorElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<ConnectorElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, ConnectorElement item)
        {
            _List.Insert(index, item);
            ConnectorElements = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            ConnectorElements = _List.ToArray();
        }

        public void Add(ConnectorElement item)
        {
            _List.Add(item);
            ConnectorElements = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            ConnectorElements = _List.ToArray();
        }

        public bool Contains(ConnectorElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(ConnectorElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(ConnectorElement item)
        {
            var result = _List.Remove(item);
            ConnectorElements = _List.ToArray();
            return result;
        }

        public IEnumerator<ConnectorElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
