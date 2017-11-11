using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class DivFreElement
    {
        public string Description;
    }

    public class DivFreElementCollection : IList<DivFreElement> 
    {
        private List<DivFreElement> _List;

        public DivFreElementCollection()
        {
            _List = new List<DivFreElement>();
            if (DivFreAlarms != null)
            {
                _List.AddRange(DivFreAlarms);
            }
        }

        [XmlElement(ElementName = "DivFreElement")]
        public DivFreElement[] DivFreAlarms;

        public DivFreElement this[int index]
        {
            get  {  return _List[index];   }
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

        public int IndexOf(DivFreElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly  
        {
            get
            {
                return ((IList<DivFreElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, DivFreElement item)
        {
            _List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
        }

        public void Add(DivFreElement item)
        {
            _List.Add(item);
        }

        public void Clear()
        {
            _List.Clear();
        }

        public bool Contains(DivFreElement item)
        {
            return _List.Contains(item);
        }

        public bool Contains(string item)
        {
            return _List.Select(o=>o.Description).Contains(item);
        }

        public void CopyTo(DivFreElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(DivFreElement item)
        {
            var result = _List.Remove(item);
            return result;
        }

        public IEnumerator<DivFreElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
