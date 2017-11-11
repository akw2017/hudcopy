using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class ConnectionElement
    {
        public string SourceID { get; set; }
        public string SinkID { get; set; }
        public string SourceOrientation { get; set; }
        public string SinkOrientation { get; set; }
        public double SourceXRatio { get; set; }
        public double SourceYRatio { get; set; }
        public double SinkXRatio { get; set; }
        public double SinkYRatio { get; set; }
    }

    public class ConnectionElementCollection : IList<ConnectionElement>
    {
        private List<ConnectionElement> _List;

        public ConnectionElementCollection()
        {
            _List = new List<ConnectionElement>();
            if (ConnectionElements != null)
            {
                _List.AddRange(ConnectionElements);
            }
        }

        [XmlElement(ElementName = "ConnectionElement")]
        public ConnectionElement[] ConnectionElements;

        public ConnectionElement this[int index]
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

        public int IndexOf(ConnectionElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<ConnectionElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, ConnectionElement item)
        {
            _List.Insert(index, item);
            ConnectionElements = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            ConnectionElements = _List.ToArray();
        }

        public void Add(ConnectionElement item)
        {
            _List.Add(item);
            ConnectionElements = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            ConnectionElements = _List.ToArray();
        }

        public bool Contains(ConnectionElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(ConnectionElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(ConnectionElement item)
        {
            var result = _List.Remove(item);
            ConnectionElements = _List.ToArray();
            return result;
        }

        public IEnumerator<ConnectionElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
