// Decompiled with JetBrains decompiler
// Type: kaolaSDK.Response.item_addPart_response
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using System;
using System.Collections.Generic;

namespace kaolaSDK.Response
{
    public class item_addPart_response
    {
        public string key { get; set; }

        public DateTime create_time { get; set; }

        public List<SkuOuterIdResult> sku_keys { get; set; }
    }
}
