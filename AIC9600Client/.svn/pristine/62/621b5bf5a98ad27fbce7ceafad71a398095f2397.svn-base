using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Common
{
    public class TextLineConfigOperator
    {
        public string FileName { get; private set; }

        public List<string> Lines { get; private set; }
        public TextLineConfigOperator(string fileName)
        {
            this.FileName = fileName;
            if(!File.Exists(fileName))
            {
                var file = File.Create(fileName);
                file.Close();
            }
            Lines = File.ReadAllLines(fileName).ToList();
        }

        public void Add(string content)
        {
            lock (this)
            {
                if (!Lines.Contains(content))
                {
                    Lines.Add(content);
                    File.WriteAllLines(FileName, Lines.ToArray());
                }
            }
        }

        public void Remove(string content)
        {
            lock (this)
            {
                if (!Lines.Contains(content))
                {
                    Lines.Remove(content);
                    File.WriteAllLines(FileName, Lines.ToArray());
                }
            }
        }

        public void Clear()
        {
            lock (this)
            {
                Lines.Clear();
                File.WriteAllLines(FileName, new string[0]);
            }
        }
    }
}
