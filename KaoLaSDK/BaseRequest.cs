// Decompiled with JetBrains decompiler
// Type: kaolaSDK.BaseRequest`1
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using kaolaSDK.Util;
using System.Collections.Generic;

namespace kaolaSDK
{
    public abstract class BaseRequest<T> : IRequest<T> where T : KLResponse
    {
        public virtual string HttpMethod
        {
            get
            {
                return "GET";
            }
        }

        public virtual string ApiGateway
        {
            get
            {
                return "https://openapi.kaola.com/router?";
            }
        }

        public virtual string KLMethod
        {
            get
            {
                return "";
            }
        }

        public virtual bool needUpload
        {
            get
            {
                return false;
            }
        }

        public virtual KLVersion KLVersion
        {
            get
            {
                return KLVersion.v2;
            }
        }

        public virtual Dictionary<string, FileItem> GetFilesParams()
        {
            return new Dictionary<string, FileItem>();
        }

        public virtual Dictionary<string, object> GetParams()
        {
            return new Dictionary<string, object>();
        }
    }
}
