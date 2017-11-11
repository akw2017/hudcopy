using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    public class VibrationElement : DesignElement
    {
        public DivFreElementCollection DivFreAlarms;
        public VibrationElement()
        {
            DivFreAlarms = new DivFreElementCollection(); 
        }
    }

    public class VibrationElementCollection : IList<VibrationElement>
    {
        private List<VibrationElement> _List;

        public VibrationElementCollection()
        {
            _List = new List<VibrationElement>();
            if (VibrationElements != null)
            {
                _List.AddRange(VibrationElements);
            }
        }

        [XmlElement(ElementName = "VibrationElement")]
        public VibrationElement[] VibrationElements;

        public VibrationElement this[int index]
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

        public int IndexOf(VibrationElement item)
        {
            return _List.IndexOf(item);
        }

        [XmlIgnore]
        public bool IsReadOnly
        {
            get
            {
                return ((IList<VibrationElement>)_List).IsReadOnly;
            }
        }

        public void Insert(int index, VibrationElement item)
        {
            _List.Insert(index, item);
            VibrationElements = _List.ToArray();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("The specified index is out of range.");
            var oldItem = _List[index];
            _List.RemoveAt(index);
            VibrationElements = _List.ToArray();
        }

        public void Add(VibrationElement item)
        {
            _List.Add(item);
            VibrationElements = _List.ToArray();
        }

        public void Clear()
        {
            _List.Clear();
            VibrationElements = _List.ToArray();
        }

        public bool Contains(VibrationElement item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(VibrationElement[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public bool Remove(VibrationElement item)
        {
            var result = _List.Remove(item);
            VibrationElements = _List.ToArray();
            return result;
        }

        public IEnumerator<VibrationElement> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
