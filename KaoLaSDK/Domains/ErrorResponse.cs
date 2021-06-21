// Decompiled with JetBrains decompiler
// Type: kaolaSDK.Domains.ErrorResponse
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using System.Collections.Generic;

namespace kaolaSDK.Domains
{
    public class ErrorResponse
    {
        public string code { get; set; }

        public string msg { get; set; }

        public List<ErrorResponse> subErrors { get; set; }
    }
}
