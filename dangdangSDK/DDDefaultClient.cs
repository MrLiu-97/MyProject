// Decompiled with JetBrains decompiler
// Type: dangdangSDK.DDDefaultClient
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using dangdangSDK.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dangdangSDK
{
    public class DDDefaultClient : IClient
    {
        private string format = "xml";
        private string version = "1.0";
        private AppInfo _appinfo;

        public DDDefaultClient(AppInfo app)
        {
            this._appinfo = app;
        }

        public T Execute<T>(IRequest<T> request) where T : DDResponse, new()
        {
            string ddMethod = request.DDMethod;
            string sign = this.GetSign(ddMethod);
            Dictionary<string, object> dictionary = request.GetParams() ?? new Dictionary<string, object>();
            dictionary.Add("method", ddMethod);
            dictionary.Add("timestamp", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dictionary.Add("format", format);
            dictionary.Add("app_key", _appinfo.AppKey);
            dictionary.Add("v", version);
            dictionary.Add("sign_method", "md5");
            dictionary.Add("session", _appinfo.accessToken);
            dictionary.Add("sign", sign);
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, object> keyValuePair in dictionary)
            {
                if (keyValuePair.Value != null && keyValuePair.Value != (object)"" && !string.IsNullOrEmpty(keyValuePair.Key))
                    stringList.Add(string.Format("{0}={1}", keyValuePair.Key, keyValuePair.Value));
            }
            string xml;
            if (request.needUpload)
            {
                Dictionary<string, FileItem> filesParams = request.GetFilesParams();
                xml = new HttpUtil().DoPost(string.Format("{0}{1}", request.ApiGateway, string.Join("&", stringList)), new Dictionary<string, string>(), filesParams, "GBK");     //  "utf-8"
            }
            else
                xml = new HttpHelper().GetHtml(new HttpItem()
                {
                    URL = string.Format("{0}{1}", request.ApiGateway, string.Join("&", stringList)),
                    Method = request.HttpMethod,
                    Encoding = Encoding.GetEncoding("GBK")
                }).Html;
            T obj = xml.ToXmlObject<T>();
            if (obj == null)
            {
                DDResponse xmlObject = xml.ToXmlObject<DDResponse>();
                obj = new T();
                obj.errorCode = xmlObject.errorCode;
                obj.errorMessage = xmlObject.errorMessage;
            }
            obj.body = xml;
            return obj;
        }

        private string GetSign(string method)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          nameof (method),
          method
        },
        {
          "timestamp",
          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        },
        {
          "format",
          format
        },
        {
          "app_key",
          _appinfo.AppKey
        },
        {
          "v",
          version
        },
        {
          "sign_method",
          "md5"
        },
        {
          "session",
          _appinfo.accessToken
        }
      }.OrderBy(_x => _x.Key).ToDictionary(k => k.Key, v => v.Value);
            List<string> stringList = new List<string>()
      {
        _appinfo.AppSecret
      };
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                stringList.Add(string.Format("{0}{1}", keyValuePair.Key, keyValuePair.Value));
            stringList.Add(_appinfo.AppSecret);
            return string.Join("", stringList).ToMD5().ToUpper();
        }
    }

}
