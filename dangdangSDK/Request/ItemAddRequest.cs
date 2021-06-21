// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Request.ItemAddRequest
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using dangdangSDK.Domains;
using dangdangSDK.Response;
using dangdangSDK.Util;
using System;
using System.Collections.Generic;

namespace dangdangSDK.Request
{
    public class ItemAddRequest : BaseRequest<ItemAddResponse>
    {
        public override string DDMethod
        {
            get
            {
                return "dangdang.item.add";
            }
        }

        public override string HttpMethod
        {
            get
            {
                return "POST";
            }
        }

        public override string ApiGateway
        {
            get
            {
                return base.ApiGateway;
            }
        }

        public override Dictionary<string, object> GetParams()
        {
            return new Dictionary<string, object>();
        }

        public override bool needUpload
        {
            get
            {
                return true;
            }
        }

        public override Dictionary<string, FileItem> GetFilesParams()
        {
            Dictionary<string, FileItem> dictionary = new Dictionary<string, FileItem>();
            string path = "\\ItemAdd\\" + string.Format("{0}.xml", (object)Guid.NewGuid().ToString("N"));
            this.addItems.ToXml(ref path);
            dictionary.Add("addItems", new FileItem(path));
            return dictionary;
        }

        public ItemAdd addItems { get; set; }
    }
}
