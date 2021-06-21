// Decompiled with JetBrains decompiler
// Type: kaolaSDK.Request.ItemImgUploadRequest
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using kaolaSDK.Response;
using kaolaSDK.Util;
using System.Collections.Generic;

namespace kaolaSDK.Request
{
    public class ItemImgUploadRequest : BaseRequest<ItemImgUploadResponse>
    {
        public override string HttpMethod
        {
            get
            {
                return "POST";
            }
        }

        public override string KLMethod
        {
            get
            {
                return "kaola.item.img.upload";
            }
        }

        public override Dictionary<string, object> GetParams()
        {
            return new Dictionary<string, object>();
        }

        public override KLVersion KLVersion
        {
            get
            {
                return KLVersion.v1;
            }
        }

        public string picPath { get; set; }

        public override Dictionary<string, FileItem> GetFilesParams()
        {
            return new Dictionary<string, FileItem>() { { picPath, new FileItem(picPath) } };
        }

        public override bool needUpload
        {
            get
            {
                return true;
            }
        }
    }
}
