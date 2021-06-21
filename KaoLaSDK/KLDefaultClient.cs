// Decompiled with JetBrains decompiler
// Type: kaolaSDK.KLDefaultClient
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using kaolaSDK.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace kaolaSDK
{
    public class KLDefaultClient : IClient
    {
        private string format = "json";
        private AppInfo _appinfo;

        public KLDefaultClient(AppInfo app)
        {
            this._appinfo = app;
        }

        public T Execute<T>(IRequest<T> request) where T : KLResponse, new()
        {
            string str1 = "";
            string klMethod = request.KLMethod;
            Dictionary<string, object> dictionary = request.GetParams() ?? new Dictionary<string, object>();
            Dictionary<string, string> dicDD = new Dictionary<string, string>();
            dicDD.Add("access_token", _appinfo.accessToken);
            dicDD.Add("app_key", _appinfo.AppKey);
            switch (request.KLVersion)
            {
                case KLVersion.v1:
                    str1 = "1.0";
                    using (Dictionary<string, object>.Enumerator enumerator = dictionary.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            KeyValuePair<string, object> current = enumerator.Current;
                            if (current.Value != null && !string.IsNullOrWhiteSpace(current.Value.ToString()))
                                dicDD.Add(current.Key, current.Value.ToString());
                        }
                        break;
                    }
                case KLVersion.v2:
                    str1 = "2.0";
                    if (dictionary.Count > 0)
                    {
                        dicDD.Add("biz_content", dictionary.ToJson());
                        break;
                    }
                    break;
            }
            dicDD.Add("method", klMethod);
            dicDD.Add("timestamp", DateTime.Now.AddMinutes(-1.0).ToString("yyyy-MM-dd HH:mm:ss"));
            dicDD.Add("v", str1);
            string sign = GetSign(dicDD);
            dicDD.Add("sign", sign);
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, string> keyValuePair in dicDD)
            {
                if (keyValuePair.Value != null && !(keyValuePair.Value == "") && !string.IsNullOrEmpty(keyValuePair.Key))
                {
                    string str2 = keyValuePair.Value;
                    stringList.Add(string.Format("{0}={1}", keyValuePair.Key, UrlEncode(keyValuePair.Value)));
                }
            }
            string str3;
            if (request.needUpload)
            {
                Dictionary<string, FileItem> filesParams = request.GetFilesParams();
                str3 = new HttpUtil().DoPost(request.ApiGateway, dicDD, filesParams, "utf-8");
            }
            else
                str3 = new HttpHelper().GetHtml(new HttpItem()
                {
                    URL = string.Format("{0}{1}", request.ApiGateway, string.Join("&", stringList)),
                    Method = request.HttpMethod,
                    Encoding = Encoding.UTF8
                }).Html;
            T obj = JsonConvert.DeserializeObject<T>(str3);
            obj.body = str3;
            return obj;
        }
         
        private string GetSign(Dictionary<string, string> dicDD)
        {
            dicDD = dicDD.OrderBy(_x => _x.Key).ToDictionary(k => k.Key, v => v.Value);
            List<string> stringList = new List<string>() { _appinfo.AppSecret };
            foreach (KeyValuePair<string, string> keyValuePair in dicDD)
                stringList.Add(string.Format("{0}{1}", keyValuePair.Key, keyValuePair.Value));
            stringList.Add(_appinfo.AppSecret);
            return string.Join("", stringList).ToMD5().ToUpper();
        }

        public string UrlEncode(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char ch in str)
            {
                string str1 = HttpUtility.UrlEncode(ch.ToString());
                stringBuilder.Append(str1.Length > 1 ? str1.ToUpper() : ch.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
