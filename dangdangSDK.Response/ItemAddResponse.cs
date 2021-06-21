// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Response.ItemAddResponse
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using dangdangSDK.Domains;
using System.Xml.Serialization;

namespace dangdangSDK.Response
{
    [XmlRoot("response")]
    public class ItemAddResponse : DDResponse
    {
        public string functionID { get; set; }

        public string time { get; set; }

        [XmlElement("Result")]
        public ItemAddResult Result { get; set; }
    }
}
