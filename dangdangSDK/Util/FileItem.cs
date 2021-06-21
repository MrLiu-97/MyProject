// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Util.FileItem
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using System;
using System.IO;

namespace dangdangSDK.Util
{
    public class FileItem
    {
        private string fileName;
        private string mimeType;
        private byte[] content;
        private FileInfo fileInfo;

        public FileItem(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
                throw new ArgumentException("fileInfo is null or not exists!");
            this.fileInfo = fileInfo;
        }

        public FileItem(string filePath)
          : this(new FileInfo(filePath))
        {
        }

        public FileItem(string fileName, byte[] content)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (content == null || content.Length == 0)
                throw new ArgumentNullException(nameof(content));
            this.fileName = fileName;
            this.content = content;
        }

        public FileItem(string fileName, byte[] content, string mimeType)
          : this(fileName, content)
        {
            if (string.IsNullOrEmpty(mimeType))
                throw new ArgumentNullException(nameof(mimeType));
            this.mimeType = mimeType;
        }

        public string GetFileName()
        {
            if (this.fileName == null && this.fileInfo != null && this.fileInfo.Exists)
                this.fileName = this.fileInfo.FullName;
            return this.fileName;
        }

        public string GetMimeType()
        {
            if (this.mimeType == null)
                this.mimeType = FileItem.GetMimeType(this.GetContent());
            return this.mimeType;
        }

        public static string GetMimeType(byte[] fileData)
        {
            string fileSuffix = FileItem.GetFileSuffix(fileData);
            return fileSuffix == "JPG" ? "image/jpeg" : (fileSuffix == "GIF" ? "image/gif" : (fileSuffix == "PNG" ? "image/png" : (fileSuffix == "BMP" ? "image/bmp" : (fileSuffix == "XML" ? "text/xml" : (fileSuffix == "RAR" ? "application/x-rar-compressed" : "application/octet-stream")))));
        }

        public static string GetFileSuffix(byte[] fileData)
        {
            if (fileData == null || fileData.Length < 10)
                return (string)null;
            if (fileData[0] == (byte)71 && fileData[1] == (byte)73 && fileData[2] == (byte)70)
                return "GIF";
            if (fileData[1] == (byte)80 && fileData[2] == (byte)78 && fileData[3] == (byte)71)
                return "PNG";
            if (fileData[6] == (byte)74 && fileData[7] == (byte)70 && fileData[8] == (byte)73 && fileData[9] == (byte)70)
                return "JPG";
            return fileData[0] == (byte)66 && fileData[1] == (byte)77 ? "BMP" : (string)null;
        }

        public byte[] GetContent()
        {
            if (this.content == null && this.fileInfo != null && this.fileInfo.Exists)
            {
                using (Stream stream = (Stream)this.fileInfo.OpenRead())
                {
                    this.content = new byte[stream.Length];
                    stream.Read(this.content, 0, this.content.Length);
                }
            }
            return this.content;
        }
    }
}
