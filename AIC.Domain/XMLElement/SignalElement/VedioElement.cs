using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class VedioElement : DesignElement
    {
        public VedioElement()
        {
        }
    }

    public class VedioElementCollection : IList<VedioElement>
    {
        private List<VedioElement> _List;

        public VedioElementCollection()
        {
            _List = new List<VedioElement>();
            if (VedioElements != null)
            {
                _List.AddRange(VedioElements);
            }
        }

        [XmlElement(ElementName = "VedioElement")]
        public VedioElement[] VedioElements;

        public VedioElement this[int index]
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

        public int IndexOf(VedioElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<VedioElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, VedioElement item)
        {
            _List.Insert(index, item);
            VedioElements = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            VedioElements = _List.ToArray();
        }

        public void Add(VedioElement item)
        {
            _List.Add(item);
            VedioElements = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            VedioElements = _List.ToArray();
        }

        public bool Contains(VedioElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(VedioElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(VedioElement item)
        {
            var result = _List.Remove(item);
            VedioElements = _List.ToArray();
            return result;
        }

        public IEnumerator<VedioElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
