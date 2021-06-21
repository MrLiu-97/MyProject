// Decompiled with JetBrains decompiler
// Type: kaolaSDK.Util.HttpUtil
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace kaolaSDK.Util
{
    public class HttpUtil
    {
        private int _timeout = 100000;

        public int Timeout
        {
            get
            {
                return this._timeout;
            }
            set
            {
                this._timeout = value;
            }
        }

        public HttpWebRequest GetWebRequest(string url, string method)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.Method = method;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.86 Safari/537.36";
            httpWebRequest.Timeout = this._timeout;
            return httpWebRequest;
        }

        public string DoPost(
          string url,
          IDictionary<string, string> textParams,
          IDictionary<string, FileItem> fileParams,
          string charset)
        {
            if (fileParams == null || fileParams.Count == 0)
                return "";
            string str = DateTime.Now.Ticks.ToString("X");
            HttpWebRequest webRequest = this.GetWebRequest(url, "POST");
            webRequest.ContentType = "multipart/form-data;charset=" + charset + ";boundary=" + str;
            Stream requestStream = webRequest.GetRequestStream();
            byte[] bytes1 = Encoding.GetEncoding(charset).GetBytes("\r\n--" + str + "\r\n");
            byte[] bytes2 = Encoding.GetEncoding(charset).GetBytes("\r\n--" + str + "--\r\n");
            string format1 = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> enumerator1 = textParams.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                string s = string.Format(format1, enumerator1.Current.Key, enumerator1.Current.Value);
                byte[] bytes3 = Encoding.GetEncoding(charset).GetBytes(s);
                requestStream.Write(bytes1, 0, bytes1.Length);
                requestStream.Write(bytes3, 0, bytes3.Length);
            }
            string format2 = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, FileItem>> enumerator2 = fileParams.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                KeyValuePair<string, FileItem> current = enumerator2.Current;
                string key = current.Key;
                current = enumerator2.Current;
                FileItem fileItem = current.Value;
                string s = string.Format(format2, (object)key, (object)fileItem.GetFileName(), (object)fileItem.GetMimeType());
                byte[] bytes3 = Encoding.GetEncoding(charset).GetBytes(s);
                requestStream.Write(bytes1, 0, bytes1.Length);
                requestStream.Write(bytes3, 0, bytes3.Length);
                byte[] content = fileItem.GetContent();
                requestStream.Write(content, 0, content.Length);
            }
            requestStream.Write(bytes2, 0, bytes2.Length);
            requestStream.Close();
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(response.CharacterSet);
            return GetResponseAsString(response, encoding);
        }

        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Stream stream = (Stream)null;
            StreamReader streamReader = (StreamReader)null;
            try
            {
                stream = rsp.GetResponseStream();
                streamReader = new StreamReader(stream, encoding);
                int num;
                while ((num = streamReader.Read()) > -1)
                {
                    char ch = (char)num;
                    if (ch > char.MinValue)
                        stringBuilder.Append(ch);
                }
            }
            finally
            {
                streamReader?.Close();
                stream?.Close();
                rsp?.Close();
            }
            return stringBuilder.ToString();
        }
    }
}
