// Decompiled with JetBrains decompiler
// Type: dangdangSDK.BaseRequest`1
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using dangdangSDK.Util;
using System.Collections.Generic;

namespace dangdangSDK
{
    public abstract class BaseRequest<T> : IRequest<T> where T : DDResponse
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
                return "http://api.open.dangdang.com/openapi/rest?";
            }
        }

        public virtual string DDMethod
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
