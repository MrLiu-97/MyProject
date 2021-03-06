// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Util.Common
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace dangdangSDK.Util
{
    public static class Common
    {
        private static string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\APP_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            });
        }

        public static string ToXml(this object obj)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is XmlRootAttribute xmlRootAttribute)
                    namespaces.Add(string.Empty, xmlRootAttribute.Namespace);
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.GetEncoding("GBK");
            if (namespaces.Count == 0)
                namespaces.Add(string.Empty, string.Empty);
            MemoryStream memoryStream = new MemoryStream();
            using (XmlWriter xmlWriter = XmlWriter.Create((Stream)memoryStream, settings))
            {
                new XmlSerializer(obj.GetType()).Serialize(xmlWriter, obj, namespaces);
                xmlWriter.Close();
            }
            memoryStream.Position = 0L;
            StreamReader streamReader = new StreamReader((Stream)memoryStream);
            string end = streamReader.ReadToEnd();
            streamReader.Dispose();
            memoryStream.Dispose();
            return end;
        }

        public static void ToXml(this object obj, ref string path)
        {
            path = Common.basePath + path;
            string path1 = Path.GetDirectoryName(path) + "\\";
            Path.GetFileName(path);
            try
            {
                if (!Directory.Exists(path1))
                    Directory.CreateDirectory(path1);
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch (Exception ex)
            {
            }
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is XmlRootAttribute xmlRootAttribute)
                    namespaces.Add(string.Empty, xmlRootAttribute.Namespace);
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.GetEncoding("GBK");
            if (namespaces.Count == 0)
                namespaces.Add(string.Empty, string.Empty);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create((Stream)fileStream, settings))
                {
                    new XmlSerializer(obj.GetType()).Serialize(xmlWriter, obj, namespaces);
                    xmlWriter.Close();
                }
            }
        }

        public static void ToRar(this IList<string> obj, ref string path)
        {
            path = basePath + path;
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (ZipArchive destination = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    foreach (string str in obj)
                        destination.CreateEntryFromFile(str, Path.GetFileName(str));
                }
            }
        }

        public static T ToXmlObject<T>(this string xml) where T : class, new()
        {
            try
            {
                using (StringReader stringReader = new StringReader(xml))
                    return (T)new XmlSerializer(typeof(T)).Deserialize(stringReader);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static string ToMD5(this string clearCode)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(clearCode));
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x2"));
            return stringBuilder.ToString().ToUpper();
        }

        public static string JsapiSignToSha1(this string inStr)
        {
            try
            {
                return string.Join("", ((IEnumerable<byte>)SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(inStr))).Select(b => b.ToString("x2")).ToArray()).ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception("Sha1加密出错：" + ex.Message);
            }
        }
    }
}
