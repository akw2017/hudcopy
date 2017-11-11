using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class CompositionElement : DesignElement
    {      
        public CompositionElement()
        {
        }
    }

    public class CompositionElementCollection : IList<CompositionElement>
    {
        private List<CompositionElement> _List;

        public CompositionElementCollection()
        {
            _List = new List<CompositionElement>();
            if (CompositionElements != null)
            {
                _List.AddRange(CompositionElements);
            }
        }

        [XmlElement(ElementName = "CompositionElement")]
        public CompositionElement[] CompositionElements;

        public CompositionElement this[int index]
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

        public int IndexOf(CompositionElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<CompositionElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, CompositionElement item)
        {
            _List.Insert(index, item);
            CompositionElements = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            CompositionElements = _List.ToArray();
        }

        public void Add(CompositionElement item)
        {
            _List.Add(item);
            CompositionElements = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            CompositionElements = _List.ToArray();
        }

        public bool Contains(CompositionElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(CompositionElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(CompositionElement item)
        {
            var result = _List.Remove(item);
            CompositionElements = _List.ToArray();
            return result;
        }

        public IEnumerator<CompositionElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
