// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Domains.ItemAddInfo
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using dangdangSDK.Util;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace dangdangSDK.Domains
{
    public class ItemAddInfo
    {
        public CData itemBarcode { get; set; }

        public CData itemName { get; set; }

        public CData itemSubhead { get; set; }

        public CData classificationCode1 { get; set; }

        public CData classificationCode2 { get; set; }

        public CData brand { get; set; }

        public CData model { get; set; }

        public CData attribute { get; set; }

        public CData itemDetail { get; set; }

        public CData stockPrice { get; set; }

        public CData marketPrice { get; set; }

        public CData vipPriceType { get; set; }

        public CData shopCategoryID1 { get; set; }

        public CData shopCategoryID2 { get; set; }

        public CData shopCategoryID3 { get; set; }

        public CData shopCategoryID4 { get; set; }

        public CData shopCategoryID5 { get; set; }

        public CData itemState { get; set; }

        public CData guaranteeRepairType { get; set; }

        public CData guaranteeReturnType { get; set; }

        public CData is_cod { get; set; }

        public CData stockCount { get; set; }

        public CData unitPrice { get; set; }

        public CData outerItemID { get; set; }

        public CData bestPartnerList { get; set; }

        public CData isSubsidy { get; set; }

        public CData subsidyAmount { get; set; }

        public CData volume { get; set; }

        public CData weight { get; set; }

        public CData templateName { get; set; }

        [XmlElement("SpecilaItemInfo")]
        public List<dangdangSDK.Domains.SpecilaItemInfo> SpecilaItemInfo { get; set; }
    }
}
