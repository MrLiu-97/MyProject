// Decompiled with JetBrains decompiler
// Type: dangdangSDK.Request.ItemsPicsSetRequest
// Assembly: dangdangSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DDC70E8-6A22-4017-8668-FEFB5A3C20F2
// Assembly location: E:\Project\ProductOnlineNew\trunk\lib\dangdangSDK.dll

using dangdangSDK.Domains;
using dangdangSDK.Response;
using dangdangSDK.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace dangdangSDK.Request
{
    public class ItemsPicsSetRequest : BaseRequest<ItemsPicsSetResponse>
    {
        public override string DDMethod
        {
            get
            {
                return "dangdang.items.pics.set";
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

        public ItemsPicsSetDic pics { get; set; }

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
            ItemsPicsSet itemsPicsSet = new ItemsPicsSet();
            itemsPicsSet.functionID = this.DDMethod;
            itemsPicsSet.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemsPicsSet.PicsList = new List<ProductPicsInfo>();
            ProductPicsInfo productPicsInfo = new ProductPicsInfo();
            productPicsInfo.itemID = pics.itemID;
            productPicsInfo.SpecialPicList = new List<SpecialPicInfo>();
            int num = 0;
            foreach (KeyValuePair<string, string> divPic in pics.divPics)
            {
                string fileName = Path.GetFileName(divPic.Key);
                string str = divPic.Value;
                if (!string.IsNullOrEmpty(str))
                {
                    productPicsInfo.SpecialPicList.Add(new SpecialPicInfo()
                    {
                        ItemPic = fileName,
                        specialAttributeValue = str
                    });
                }
                else
                {
                    switch (num++)
                    {
                        case 0:
                            productPicsInfo.productPic1 = fileName;
                            break;
                        case 1:
                            productPicsInfo.productPic2 = fileName;
                            break;
                        case 2:
                            productPicsInfo.productPic3 = fileName;
                            break;
                        case 3:
                            productPicsInfo.productPic4 = fileName;
                            break;
                        case 4:
                            productPicsInfo.productPic5 = fileName;
                            break;
                        case 5:
                            productPicsInfo.productPic6 = fileName;
                            break;
                        case 6:
                            productPicsInfo.productPic7 = fileName;
                            break;
                        case 7:
                            productPicsInfo.productPic8 = fileName;
                            break;
                    }
                }
            }
            itemsPicsSet.PicsList.Add(productPicsInfo);
            string str1 = string.Format("{0}.xml", Guid.NewGuid().ToString("N"));
            string str2 = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string path = "\\ItemsPics\\" + str2 + "\\" + str1;
            itemsPicsSet.ToXml(ref path);
            foreach (string str3 in pics.divPics.Keys.ToList())
                File.Copy(str3, Path.GetDirectoryName(path) + "\\" + Path.GetFileName(str3));
            WinrarHelper.RAR(Path.GetDirectoryName(path), Path.GetDirectoryName(path), str2 + ".rar");
            dictionary.Add("setItemsPics", new FileItem(Path.GetDirectoryName(path) + "\\" + str2 + ".rar"));
            return dictionary;
        }
    }
}
