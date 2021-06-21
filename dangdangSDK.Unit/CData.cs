// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Util.CData
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace dangdangSDK.Util
{
    public class CData : IXmlSerializable
    {
        private string m_Value;

        public CData()
        {
        }

        public CData(string p_Value)
        {
            this.m_Value = p_Value;
        }

        public string Value
        {
            get
            {
                return this.m_Value;
            }
        }

        public void ReadXml(XmlReader reader)
        {
            this.m_Value = reader.ReadElementContentAsString();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteCData(this.m_Value);
        }

        public XmlSchema GetSchema()
        {
            return (XmlSchema)null;
        }

        public override string ToString()
        {
            return this.m_Value;
        }

        public static implicit operator string(CData element)
        {
            return element == null ? (string)null : element.m_Value;
        }

        public static implicit operator CData(string text)
        {
            return new CData(text);
        }
    }
}
