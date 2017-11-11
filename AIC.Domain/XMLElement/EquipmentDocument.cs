using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AIC.Domain
{
    [XmlRootAttribute(Namespace = EquipmentDocument.XMLNS, IsNullable = false)]
    public class EquipmentDocument
    {
        public const string XMLNS = "http://www.AIC.com.cn/AIC/DesignLayout";
        private readonly object saveLock = new Object();

        public EquipmentElementCollection Equipments;

        public EquipmentDocument()
        {
            Equipments = new EquipmentElementCollection();
        }

        public EquipmentDocument(EquipmentElement equipment)
            : this()
        {
            Equipments.Add(equipment);
        }

        public EquipmentDocument(EquipmentElement[] equipments)
            : this()
        {
            foreach (var item in equipments)
            {
                Equipments.Add(item);
            }
        }

        public bool IsEmpty
        {
            get { return Equipments.Equipments == null; }
        }

        public void Save(FileInfo designFile)
        {
            lock (saveLock)
            {

                FileStream streamToUse;
                XmlSerializer serializer = new XmlSerializer(typeof(EquipmentDocument));

                if (designFile.Exists)
                {
                    File.Delete(designFile.FullName);
                }
                streamToUse = designFile.Open(FileMode.OpenOrCreate, FileAccess.Write);

                try
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    XmlWriter writer = XmlWriter.Create(streamToUse, settings);
                    serializer.Serialize(writer, this);
                }
                finally
                {
                    streamToUse.Close();
                }
            }
        }
    }
}
