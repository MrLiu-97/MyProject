// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Domains.SpecilaItemInfo
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using dangdangSDK.Util;
using System.Xml.Serialization;

namespace dangdangSDK.Domains
{
    [XmlRoot("SpecilaItemInfo")]
    public class SpecilaItemInfo
    {
        public CData specialAttributeClass { get; set; }

        public CData specialAttribute { get; set; }

        public CData specialAttributeSeq { get; set; }

        public CData stockCount { get; set; }

        public CData unitPrice { get; set; }

        public CData outerItemID { get; set; }
    }
}
