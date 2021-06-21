// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Domains.ItemIDInfo
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using System.Collections.Generic;
using System.Xml.Serialization;

namespace dangdangSDK.Domains
{
    public class ItemIDInfo
    {
        public string outerItemID { get; set; }

        public string itemID { get; set; }

        public string itemName { get; set; }

        public string subItemID { get; set; }

        public string operCode { get; set; }

        public string operation { get; set; }

        [XmlElement("SpecialItemInfo")]
        public List<specialItemInfo> SpecialItemInfo { get; set; }
    }
}
