// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Domains.specialItemInfo
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using System.Xml.Serialization;

namespace dangdangSDK.Domains
{
    [XmlRoot("SpecialItemInfo")]
    public class specialItemInfo
    {
        public string outerItemID { get; set; }

        public string subItemID { get; set; }

        public string operCode { get; set; }

        public string operation { get; set; }
    }
}
