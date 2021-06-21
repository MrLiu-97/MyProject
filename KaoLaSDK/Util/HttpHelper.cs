// Decompiled with JetBrains decompiler
// Type: kaolaSDK.Util.HttpHelper
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace kaolaSDK.Util
{
    public class HttpHelper
    {
        private Encoding encoding = Encoding.Default;
        private Encoding postencoding = Encoding.Default;
        private HttpWebRequest request = (HttpWebRequest)null;
        private HttpWebResponse response = (HttpWebResponse)null;

        public HttpResult GetHtml(HttpItem item)
        {
            HttpResult result = new HttpResult();
            try
            {
                this.SetRequest(item);
            }
            catch (Exception ex)
            {
                result.Cookie = string.Empty;
                result.Header = (WebHeaderCollection)null;
                result.Html = ex.Message;
                result.StatusDescription = "配置参数时出错：" + ex.Message;
                return result;
            }
            try
            {
                using (this.response = (HttpWebResponse)this.request.GetResponse())
                    this.GetData(item, result);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (this.response = (HttpWebResponse)ex.Response)
                        this.GetData(item, result);
                }
                else
                    result.Html = ex.Message;
            }
            catch (Exception ex)
            {
                result.Html = ex.Message;
            }
            if (item.IsToLower)
                result.Html = result.Html.ToLower();
            return result;
        }

        private void GetData(HttpItem item, HttpResult result)
        {
            result.StatusCode = this.response.StatusCode;
            result.StatusDescription = this.response.StatusDescription;
            result.Header = this.response.Headers;
            if (this.response.Cookies != null)
                result.CookieCollection = this.response.Cookies;
            if (this.response.Headers["set-cookie"] != null)
                result.Cookie = this.response.Headers["set-cookie"];
            byte[] numArray = this.GetByte();
            if (numArray != null & (uint)numArray.Length > 0U)
            {
                this.SetEncoding(item, result, numArray);
                result.Html = this.encoding.GetString(numArray);
            }
            else
                result.Html = string.Empty;
        }

        private void SetEncoding(HttpItem item, HttpResult result, byte[] ResponseByte)
        {
            if (item.ResultType == ResultType.Byte)
                result.ResultByte = ResponseByte;
            if (this.encoding != null)
                return;
            System.Text.RegularExpressions.Match match = Regex.Match(Encoding.Default.GetString(ResponseByte), "<meta[^<]*charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
            string str = string.Empty;
            if (match != null && match.Groups.Count > 0)
                str = match.Groups[1].Value.ToLower().Trim();
            if (str.Length > 2)
            {
                try
                {
                    this.encoding = Encoding.GetEncoding(str.Replace("\"", string.Empty).Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk").Trim());
                }
                catch
                {
                    this.encoding = !string.IsNullOrEmpty(this.response.CharacterSet) ? Encoding.GetEncoding(this.response.CharacterSet) : Encoding.UTF8;
                }
            }
            else
                this.encoding = !string.IsNullOrEmpty(this.response.CharacterSet) ? Encoding.GetEncoding(this.response.CharacterSet) : Encoding.UTF8;
        }

        private byte[] GetByte()
        {
            MemoryStream memoryStream1 = new MemoryStream();
            MemoryStream memoryStream2 = this.response.ContentEncoding == null || !this.response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase) ? this.GetMemoryStream(this.response.GetResponseStream()) : this.GetMemoryStream((Stream)new GZipStream(this.response.GetResponseStream(), CompressionMode.Decompress));
            byte[] array = memoryStream2.ToArray();
            memoryStream2.Close();
            return array;
        }

        private MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream memoryStream = new MemoryStream();
            int count1 = 256;
            byte[] buffer = new byte[count1];
            for (int count2 = streamResponse.Read(buffer, 0, count1); count2 > 0; count2 = streamResponse.Read(buffer, 0, count1))
                memoryStream.Write(buffer, 0, count2);
            return memoryStream;
        }

        private void SetRequest(HttpItem item)
        {
            this.SetCer(item);
            if (item.Header != null && item.Header.Count > 0)
            {
                foreach (string allKey in item.Header.AllKeys)
                    this.request.Headers.Add(allKey, item.Header[allKey]);
            }
            this.SetProxy(item);
            if (item.ProtocolVersion != (Version)null)
                this.request.ProtocolVersion = item.ProtocolVersion;
            this.request.ServicePoint.Expect100Continue = item.Expect100Continue;
            this.request.Method = item.Method;
            this.request.Timeout = item.Timeout;
            this.request.KeepAlive = item.KeepAlive;
            this.request.ReadWriteTimeout = item.ReadWriteTimeout;
            if (item.IfModifiedSince.HasValue)
                this.request.IfModifiedSince = Convert.ToDateTime((object)item.IfModifiedSince);
            this.request.Accept = item.Accept;
            this.request.ContentType = item.ContentType;
            this.request.UserAgent = item.UserAgent;
            this.encoding = item.Encoding;
            this.request.Credentials = item.ICredentials;
            this.SetCookie(item);
            this.request.Referer = item.Referer;
            this.request.AllowAutoRedirect = item.Allowautoredirect;
            if (item.MaximumAutomaticRedirections > 0)
                this.request.MaximumAutomaticRedirections = item.MaximumAutomaticRedirections;
            this.SetPostData(item);
            if (item.Connectionlimit <= 0)
                return;
            this.request.ServicePoint.ConnectionLimit = item.Connectionlimit;
        }

        private void SetCer(HttpItem item)
        {
            if (!string.IsNullOrEmpty(item.CerPath))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
                this.request = (HttpWebRequest)WebRequest.Create(item.URL);
                this.SetCerList(item);
                this.request.ClientCertificates.Add(new X509Certificate(item.CerPath));
            }
            else
            {
                this.request = (HttpWebRequest)WebRequest.Create(item.URL);
                this.SetCerList(item);
            }
        }

        private void SetCerList(HttpItem item)
        {
            if (item.ClentCertificates == null || item.ClentCertificates.Count <= 0)
                return;
            foreach (X509Certificate clentCertificate in item.ClentCertificates)
                this.request.ClientCertificates.Add(clentCertificate);
        }

        private void SetCookie(HttpItem item)
        {
            if (!string.IsNullOrEmpty(item.Cookie))
                this.request.Headers[HttpRequestHeader.Cookie] = item.Cookie;
            if (item.ResultCookieType != ResultCookieType.CookieCollection)
                return;
            this.request.CookieContainer = new CookieContainer();
            if (item.CookieCollection != null && item.CookieCollection.Count > 0)
                this.request.CookieContainer.Add(item.CookieCollection);
        }

        private void SetPostData(HttpItem item)
        {
            if (this.request.Method.Trim().ToLower().Contains("get"))
                return;
            if (item.PostEncoding != null)
                this.postencoding = item.PostEncoding;
            byte[] buffer = (byte[])null;
            if (item.PostDataType == PostDataType.Byte && item.PostdataByte != null && (uint)item.PostdataByte.Length > 0U)
                buffer = item.PostdataByte;
            else if (item.PostDataType == PostDataType.FilePath && !string.IsNullOrEmpty(item.Postdata))
            {
                StreamReader streamReader = new StreamReader(item.Postdata, this.postencoding);
                buffer = this.postencoding.GetBytes(streamReader.ReadToEnd());
                streamReader.Close();
            }
            else if (!string.IsNullOrEmpty(item.Postdata))
                buffer = this.postencoding.GetBytes(item.Postdata);
            if (buffer != null)
            {
                this.request.ContentLength = (long)buffer.Length;
                this.request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }
        }

        private void SetProxy(HttpItem item)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(item.ProxyIp))
                flag = item.ProxyIp.ToLower().Contains("ieproxy");
            if (!string.IsNullOrEmpty(item.ProxyIp) && !flag)
            {
                if (item.ProxyIp.Contains(":"))
                {
                    string[] strArray = item.ProxyIp.Split(':');
                    this.request.Proxy = (IWebProxy)new WebProxy(strArray[0].Trim(), Convert.ToInt32(strArray[1].Trim()))
                    {
                        Credentials = (ICredentials)new NetworkCredential(item.ProxyUserName, item.ProxyPwd)
                    };
                }
                else
                    this.request.Proxy = (IWebProxy)new WebProxy(item.ProxyIp, false)
                    {
                        Credentials = (ICredentials)new NetworkCredential(item.ProxyUserName, item.ProxyPwd)
                    };
            }
            else
            {
                if (flag)
                    return;
                this.request.Proxy = (IWebProxy)item.WebProxy;
            }
        }

        private bool CheckValidationResult(
          object sender,
          X509Certificate certificate,
          X509Chain chain,
          SslPolicyErrors errors)
        {
            return true;
        }
    }
}
