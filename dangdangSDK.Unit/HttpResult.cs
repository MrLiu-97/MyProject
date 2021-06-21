// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Util.HttpResult
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using System.Net;

namespace dangdangSDK.Util
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
                return _Cookie;
            }
            set
            {
                _Cookie = value;
            }
        }

        public CookieCollection CookieCollection
        {
            get
            {
                return _CookieCollection;
            }
            set
            {
                _CookieCollection = value;
            }
        }

        public string Html
        {
            get
            {
                return _html;
            }
            set
            {
                _html = value;
            }
        }

        public byte[] ResultByte
        {
            get
            {
                return _ResultByte;
            }
            set
            {
                _ResultByte = value;
            }
        }

        public WebHeaderCollection Header
        {
            get
            {
                return _Header;
            }
            set
            {
                _Header = value;
            }
        }

        public string StatusDescription
        {
            get
            {
                return _StatusDescription;
            }
            set
            {
                _StatusDescription = value;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return _StatusCode;
            }
            set
            {
                _StatusCode = value;
            }
        }
    }
}
