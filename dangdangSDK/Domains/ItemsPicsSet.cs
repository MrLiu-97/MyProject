// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Domains.ItemsPicsSet
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using System.Collections.Generic;
using System.Xml.Serialization;

namespace dangdangSDK.Domains
{
    [XmlRoot("request")]
    public class ItemsPicsSet
    {
        public string functionID { get; set; }

        public string time { get; set; }

        public List<ProductPicsInfo> PicsList { get; set; }
    }
}
