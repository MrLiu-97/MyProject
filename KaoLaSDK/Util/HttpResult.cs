// Decompiled with JetBrains decompiler
// Type: kaolaSDK.Util.HttpResult
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using System.Net;

namespace kaolaSDK.Util
{
    public class HttpResult
    {
        private string _html = string.Empty;
        private string _Cookie;
        private CookieCollection _CookieCollection;
        private byte[] _ResultByte;
        private WebHeaderCollection _Header;
        private string _StatusDescription;
        private HttpStatusCode _StatusCode;

        public string Cookie
        {
            get
            {
                return this._Cookie;
            }
            set
            {
                this._Cookie = value;
            }
        }

        public CookieCollection CookieCollection
        {
            get
            {
                return this._CookieCollection;
            }
            set
            {
                this._CookieCollection = value;
            }
        }

        public string Html
        {
            get
            {
                return this._html;
            }
            set
            {
                this._html = value;
            }
        }

        public byte[] ResultByte
        {
            get
            {
                return this._ResultByte;
            }
            set
            {
                this._ResultByte = value;
            }
        }

        public WebHeaderCollection Header
        {
            get
            {
                return this._Header;
            }
            set
            {
                this._Header = value;
            }
        }

        public string StatusDescription
        {
            get
            {
                return this._StatusDescription;
            }
            set
            {
                this._StatusDescription = value;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this._StatusCode;
            }
            set
            {
                this._StatusCode = value;
            }
        }
    }
}
