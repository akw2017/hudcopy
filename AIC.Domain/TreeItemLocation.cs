using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Domain
{
    public class TreeItemLocation : IReadOnlyDictionary<int, BindableValue<string>>
    {
        private IDictionary<int, BindableValue<string>> dict;
        public TreeItemLocation(params BindableValue<string>[] args)
        {
            Deepth = args.Length;
            dict = new Dictionary<int, BindableValue<string>>();
            for (int i = 0; i < args.Length; i++)
            {
                dict.Add(i, args[i]);
            }
        }

        public int Deepth { get; }

        public BindableValue<string> this[int key]
        {
            get { return dict[key]; }
        }

        public int Count
        {
            get { return dict.Count; }
        }

        public IEnumerable<int> Keys
        {
            get { return dict.Keys; }
        }

        public IEnumerable<BindableValue<string>> Values
        {
            get { return dict.Values; }
        }

        public bool ContainsKey(int key)
        {
            return dict.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<int, BindableValue<string>>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        public bool TryGetValue(int key, out BindableValue<string> value)
        {
            return dict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
          return  dict.GetEnumerator();
        }
    }

    public static class TreeItemLocationExtension
    {
        public static TreeItemLocation Merge(this TreeItemLocation location1, TreeItemLocation location2)
        {
            return new TreeItemLocation(location1.Values.Concat(location2.Values).ToArray());
        }
    }
}
