// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Response.ItemsPicsSetResponse
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: C:\Users\TaoQu\Desktop\dangdangSDK.dll

using dangdangSDK.Domains;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace dangdangSDK.Response
{
    [XmlRoot("response")]
    public class ItemsPicsSetResponse : DDResponse
    {
        public string functionID { get; set; }

        public string time { get; set; }

        [XmlArrayItem("itemIDInfo")]
        public List<ItemIDInfo> ItemsIDList { get; set; }
    }
}
