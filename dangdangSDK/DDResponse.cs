﻿// Decompiled with JetBrains decompiler
// Type: dangdangSDK.DDResponse
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using System.Xml.Serialization;

namespace dangdangSDK
{
    [XmlRoot("errorResponse")]
    public class DDResponse
    {
        [XmlElement("body")]
        public string body { get; set; }

        [XmlElement("errorCode")]
        public string errorCode { get; set; }

        [XmlElement("errorMessage")]
        public string errorMessage { get; set; }
    }
}