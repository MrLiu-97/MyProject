// Decompiled with JetBrains decompiler
// Type: kaolaSDK.IRequest`1
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using kaolaSDK.Util;
using System.Collections.Generic;

namespace kaolaSDK
{
    public interface IRequest<out T> where T : KLResponse
    {
        string HttpMethod { get; }

        string ApiGateway { get; }

        string KLMethod { get; }

        bool needUpload { get; }

        KLVersion KLVersion { get; }

        Dictionary<string, FileItem> GetFilesParams();

        Dictionary<string, object> GetParams();
    }
}
