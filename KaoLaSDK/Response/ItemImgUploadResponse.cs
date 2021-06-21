// Decompiled with JetBrains decompiler
// Type: kaolaSDK.Response.ItemImgUploadResponse
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using Newtonsoft.Json;

namespace kaolaSDK.Response
{
    public class ItemImgUploadResponse : KLResponse
    {
        [JsonProperty("kaola_item_img_upload_response")]
        public kaola_item_img_upload_response Response { get; set; }
    }
}
