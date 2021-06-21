// Decompiled with JetBrains decompiler
// Type: kaolaSDK.KLResponse
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using kaolaSDK.Domains;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace kaolaSDK
{
    [XmlRoot("error_response")]
    public class KLResponse
    {
        public string body { get; set; }

        [JsonProperty("error_response")]
        public ErrorResponse error { get; set; }
    }
}
