using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KL = kaolaSDK;
using KLRq = kaolaSDK.Request;
using KLRp = kaolaSDK.Response;
using dangdangSDK;
using dangdangSDK.Domains;
using dangdangSDK.Request;


namespace ConsolePrintMain.practice
{
    class TestEveryPlatformSDK
    {
        public static void Testkl()
        {
            KL.AppInfo app = new KL.AppInfo()
            {

                accessToken = "77268e04-76ce-40c9-81a4-9125d8f15023",
                AppSecret = "6fb16f90504c8b81144eef40f97a94685440166157c40bac085868a8d1b416d6",
                AppKey = "3efb48ba6b0ac6a36a4c568314f8e03e"
            };
            KL.IClient client = new KL.KLDefaultClient(app);
            KLRq.ItemImgUploadRequest request = new KLRq.ItemImgUploadRequest();
            request.picPath = "E:\\Project\\ProductOnlineNew\\trunk\\ProductOnline\\Upload\\Images\\20201209\\6374310729717400089058996.jpg";
            KLRp.ItemImgUploadResponse response = client.Execute(request);
            Console.WriteLine(response.body);
        }

        public static void TestklAdd()
        {
            KL.AppInfo _app = new KL.AppInfo()
            {
                accessToken = "77268e04-76ce-40c9-81a4-9125d8f15023",
                AppSecret = "6fb16f90504c8b81144eef40f97a94685440166157c40bac085868a8d1b416d6",
                AppKey = "3efb48ba6b0ac6a36a4c568314f8e03e"
            };
            KL.IClient client = new KL.KLDefaultClient(_app);

            KLRq.ItemAddPartRequest itemAddModel = JsonConvert.DeserializeObject<KLRq.ItemAddPartRequest>(kaolaparam);
            Console.WriteLine();
            //KLRq.ItemAddPartRequest request = new KLRq.ItemAddPartRequest();
            //request.name =                 //商品名称
            //request.sub_title =           //副标题
            //request.short_title =        //短标题
            //request.ten_words_desc =          //十字描述
            //request.item_NO =         //商品货号
            //request.brand_id = brandId.ToInt32();       //品牌id
            //request.original_country_code_id = "142";   //原产国id                     
            //request.category_id = category.ToInt32();   //所属类目id
            //                                            //request.warehouse_id = "43003";
            //request.gross_weight = ;                 //商品毛重（单位kg）
            //request.item_outer_id = synchroInfos.FirstOrDefault(p => p.Prop_Id == "outer_id") == null ? "" : synchroInfos.FirstOrDefault(p => p.Prop_Id == "outer_id").Value;             //商品外键id
            //request.property_valueId_list = string.Join("|", listAttr);//attr.Substring(0, attr.Length - 1);       //商品对应的预定义(下拉/单选/多选)属性值列表
            //request.text_property_name_id = inputarrt.Substring(0, inputarrt.Length - 1);
            //request.sku_market_prices = sku_price.Substring(0, sku_price.Length - 1);           //Sku市场价
            //request.sku_sale_prices = sku_price.Substring(0, sku_price.Length - 1);            //Sku销售价
            //request.sku_barcode = sku_barcode.Substring(0, sku_barcode.Length - 1);            //Sku条形码
            //request.sku_stock = sku_stock.Substring(0, sku_stock.Length - 1);                   //Sku库存
            //request.sku_property_value = sale_arrt.Substring(0, sale_arrt.Length - 1);          //录入格式：(属性项id:属性值id:属性项中文:图片url|属性项id:-1:自定义属性值:图片url) 同一个sku不同的属性之间用;分隔，不同的sku属性之间用|分隔，只有颜色属性会有图片url
            //request.sku_outer_id = sku_outer_id.Substring(0, sku_outer_id.Length - 1);          //Sku外键id
            //request.description = uploaderPics.OnLine_Pic;
            ////LogHelper.Write("考拉铺货", ShopName + "铺货数据", "铺货前Json---：" + request.ToJson());
            KLRp.ItemAddPartResponse response = client.Execute(itemAddModel);
            Console.WriteLine(response.body);
        }
        public static void Testdd()
        {
            dangdangSDK.AppInfo app = new dangdangSDK.AppInfo()
            {
                accessToken = @"0B16038F681BA6D5000FEB74315DF13C23FC370B973E3267CCAAEDCE7DD7D46C",
                AppSecret = "AC3248BCD701C6EB2EAE9B8552710840",
                AppKey = "2100007472"
            };
            dangdangSDK.IClient client = new DDDefaultClient(app);
            JsonItem itemJson = JsonConvert.DeserializeObject<JsonItem>(dd);
            //string a = "ST&SAT锟斤拷锟斤拷锟斤拷";
            //String data = a;
            //byte[] vs = Encoding.GetEncoding(data).GetBytes("UTF-8");
            //String b = new string(Encoding.GetEncoding(a).GetBytes("GBK"),"UTF-8"); //new String(, "GBK");
            //dangdangSDK.Request.shopDDcategoryGetRequest ItemsGet = new dangdangSDK.Request.shopDDcategoryGetRequest();
            //dangdangSDK.Response.shopDDcategoryGetResponse response = client.Execute(ItemsGet);
            ItemAddRequest request = new dangdangSDK.Request.ItemAddRequest();
            {
                ItemAdd itemAdd = new ItemAdd();
                itemAdd.time = itemJson.addItems.time.ToString();//DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                itemAdd.functionID = itemJson.addItems.functionID.ToString();//"dangdang.item.add";
                List<ItemAddInfo> addInfos = new List<ItemAddInfo>();
                ItemAddInfo addInfo = new ItemAddInfo();
                itemJson.addItems.ItemsList.ForEach(x =>
                {

                    //addInfo.itemName=EventWaitHandle
                    // 标题
                    addInfo.itemName = x.itemName.Value;//x.itemName.Value;   //brand_n + product.Name;
                                                        // 货号              
                    addInfo.model = x.model.Value;     //product.Product_No;
                                                       // 获取当当那个标准品牌名称
                                                       //dangdangSDK.Request.shopDDbrandGetRequest shopDDbrandGetRequest = new dangdangSDK.Request.shopDDbrandGetRequest();
                                                       //dangdangSDK.Response.shopDDbrandGetResponse shopDDbrandGetResponse = client.Execute(shopDDbrandGetRequest);
                                                       //var FindBrand = shopDDbrandGetResponse.DDBrandList.Find(f => f.txt.Contains(x.brand.Value));
                                                       // 品牌
                    addInfo.brand = x.brand.Value;     //brand_n;
                                                       // 类目
                    addInfo.classificationCode1 = x.classificationCode1.Value;     //category;
                                                                                   // 商品描述
                    addInfo.itemDetail = x.itemDetail.Value;// online_pic;
                                                            // 进货价
                    addInfo.stockPrice = x.stockPrice.Value;// price_.ToString("f2");
                                                            // 市场价
                    addInfo.marketPrice = x.marketPrice.Value;// price_.ToString("f2");
                                                              // 商品状态
                    addInfo.itemState = "下架";
                    // 是否支持COD
                    addInfo.is_cod = "否";
                    // 一般属性
                    addInfo.attribute = x.attribute.Value; //attr.Substring(0, attr.Length - 1);

                    //销售属性
                    List<SpecilaItemInfo> itemInfos = new List<SpecilaItemInfo>();
                    x.SpecilaItemInfo.ForEach(s =>
                    {
                        SpecilaItemInfo itemInfo = new SpecilaItemInfo();

                        //尺码
                        itemInfo.specialAttributeClass = s.specialAttributeClass.Value;//  color_name + ";鞋码>>均码";
                        itemInfo.specialAttribute = s.specialAttribute == null ? null : s.specialAttribute.Value;//  color_name + ";鞋码>>" + details.PKey;
                        itemInfo.outerItemID = s.outerItemID.Value;//  product.Product_No + item.Sku_outerId;
                        itemInfo.unitPrice = s.unitPrice.Value;// price_.ToString("f2");
                        itemInfo.stockCount = "0";
                        itemInfos.Add(itemInfo);
                    });
                    addInfo.SpecilaItemInfo = itemInfos;
                    //运费模板
                    addInfo.templateName = "店铺运费";
                    addInfos.Add(addInfo);
                    itemAdd.ItemsList = addInfos;
                    request.addItems = itemAdd;
                    dangdangSDK.Response.ItemAddResponse response = client.Execute(request);
                    if (response.errorMessage != null)
                    {
                        return;
                    }
                    if (response.Result.ItemsIDList[0].operCode != "0")
                    {
                        return;
                    }
                    PicsSet picsSet = JsonConvert.DeserializeObject<PicsSet>(ddpic);
                    // 上传颜色图
                    dangdangSDK.Request.ItemsPicsSetRequest Request = new dangdangSDK.Request.ItemsPicsSetRequest();
                    ItemsPicsSetDic picsSetDic = new ItemsPicsSetDic();
                    picsSetDic.itemID = response.Result.ItemsIDList[0].itemID; // picsSet.pics.itemID;/* x.it.itemID*/;
                    picsSetDic.divPics = picsSet.pics.divPics;
                    Request.pics = picsSetDic;
                    dangdangSDK.Response.ItemsPicsSetResponse Response = client.Execute(Request);
                });


            };
        }

        #region 当当
        public static readonly string dd = "{\"DDMethod\":\"dangdang.item.add\",\"HttpMethod\":\"POST\",\"ApiGateway\":\"http://api.open.dangdang.com/openapi/rest?\",\"needUpload\":true,\"addItems\":{\"functionID\":{\"Value\":\"dangdang.item.add\"},\"time\":{\"Value\":\"2021-03-05 17:00:54\"},\"ItemsList\":[{\"itemBarcode\":null,\"itemName\":{\"Value\":\"ST&SAT星期六扣饰2020尖头优雅浅口单鞋\"},\"itemSubhead\":null,\"classificationCode1\":{\"Value\":\"4002853\"},\"classificationCode2\":null,\"brand\":{\"Value\":\"ST&SAT星期六\"},\"model\":{\"Value\":\"SS03111210\"},\"attribute\":{\"Value\":\"风格>>优雅;材质>>PU/革;单鞋开口>>浅口(7cm以下);女鞋头款>>尖头;跟高>>高跟（6-8cm）;跟型>>细跟;流行元素>>金属装饰;闭合方式>>套脚;图案>>纯色;适用对象>>青年（16-44岁）;种类>>正装;分色分码换货>>支持;皮质特征>>修面\"},\"itemDetail\":{\"Value\":\"<p><img src=\\\"http://img51.ddimg.cn/215060211035911.jpg\\\" title=\\\"6372964461485156251604708.jpg\\\" alt=\\\"6372964461485156251604708.jpg\\\"/><img src=\\\"http://img53.ddimg.cn/215060211035913.jpg\\\" title=\\\"6372792682003515627811732.jpg\\\" alt=\\\"6372792682003515627811732.jpg\\\"/></p>\"},\"stockPrice\":{\"Value\":\"1029.00\"},\"marketPrice\":{\"Value\":\"1029.00\"},\"vipPriceType\":null,\"shopCategoryID1\":null,\"shopCategoryID2\":null,\"shopCategoryID3\":null,\"shopCategoryID4\":null,\"shopCategoryID5\":null,\"itemState\":{\"Value\":\"下架\"},\"guaranteeRepairType\":null,\"guaranteeReturnType\":null,\"is_cod\":{\"Value\":\"否\"},\"stockCount\":null,\"unitPrice\":null,\"outerItemID\":null,\"bestPartnerList\":null,\"isSubsidy\":null,\"subsidyAmount\":null,\"volume\":null,\"weight\":null,\"templateName\":{\"Value\":\"店铺运费\"},\"SpecilaItemInfo\":[{\"specialAttributeClass\":{\"Value\":\"颜色>>紫色;鞋码>>均码\"},\"specialAttribute\":{\"Value\":\"颜色>>紫色;鞋码>>34\"},\"specialAttributeSeq\":null,\"stockCount\":{\"Value\":\"0\"},\"unitPrice\":{\"Value\":\"1029.00\"},\"outerItemID\":{\"Value\":\"SS0311121031220\"}},{\"specialAttributeClass\":{\"Value\":\"颜色>>紫色;鞋码>>36以下\"},\"specialAttribute\":{\"Value\":\"颜色>>紫色;鞋码>>35\"},\"specialAttributeSeq\":null,\"stockCount\":{\"Value\":\"0\"},\"unitPrice\":{\"Value\":\"1029.00\"},\"outerItemID\":{\"Value\":\"SS0311121031225\"}},{\"specialAttributeClass\":{\"Value\":\"颜色>>紫色;鞋码>>36\"},\"specialAttribute\":null,\"specialAttributeSeq\":null,\"stockCount\":{\"Value\":\"0\"},\"unitPrice\":{\"Value\":\"1029.00\"},\"outerItemID\":{\"Value\":\"SS0311121031230\"}},{\"specialAttributeClass\":{\"Value\":\"颜色>>紫色;鞋码>>37\"},\"specialAttribute\":null,\"specialAttributeSeq\":null,\"stockCount\":{\"Value\":\"0\"},\"unitPrice\":{\"Value\":\"1029.00\"},\"outerItemID\":{\"Value\":\"SS0311121031235\"}},{\"specialAttributeClass\":{\"Value\":\"颜色>>紫色;鞋码>>38\"},\"specialAttribute\":null,\"specialAttributeSeq\":null,\"stockCount\":{\"Value\":\"0\"},\"unitPrice\":{\"Value\":\"1029.00\"},\"outerItemID\":{\"Value\":\"SS0311121031240\"}},{\"specialAttributeClass\":{\"Value\":\"颜色>>紫色;鞋码>>39\"},\"specialAttribute\":null,\"specialAttributeSeq\":null,\"stockCount\":{\"Value\":\"0\"},\"unitPrice\":{\"Value\":\"1029.00\"},\"outerItemID\":{\"Value\":\"SS0311121031245\"}}]}]}}";

        //public static object JsonParms { get; private set; }
        // 图片上传
        public static readonly string ddpic = "{\"DDMethod\":\"dangdang.items.pics.set\",\"HttpMethod\":\"POST\",\"ApiGateway\":\"http://api.open.dangdang.com/openapi/rest?\",\"pics\":{\"itemID\":\"1842204156\",\"divPics\":{\"E:\\\\Project\\\\ProductOnlineNew\\\\trunk\\\\ProductOnline\\\\/Upload/Images/20210222/202102221707337670_DD_YS.jpg\":\"紫色\",\"E:\\\\Project\\\\ProductOnlineNew\\\\trunk\\\\ProductOnline\\\\/Upload/Images/20210305/202103051657099492_DD_ZT.jpg\":null}},\"needUpload\":true}";
        #endregion

        #region 考拉
        public static readonly string kaolaparam = "{\"HttpMethod\":\"GET\",\"KLMethod\":\"kaola.item.addPart\",\"name\":\"ST&SAT 星期六单鞋2021春新尖头浅口细高跟通勤女鞋子SS11111800\",\"sub_title\":\"单鞋2021春新尖头浅口细高跟通勤女鞋子\",\"short_title\":\"星期六尖头浅口细高跟通勤女鞋\",\"ten_words_desc\":\"尖头浅口细高跟\",\"item_NO\":\"SS11111800\",\"original_country_code_id\":\"142\",\"description\":\"<p><img src=\\\"http://kaola-pop.oss.kaolacdn.com/463a9882-aaf9-441c-8adc-d4b82d50d526\\\" title=\\\"9.jpg\\\"/></p><p><img src=\\\"http://kaola-pop.oss.kaolacdn.com/246d05b7-0164-496a-8221-bff12c409c56\\\" title=\\\"10.jpg\\\"/></p><p><img src=\\\"http://kaola-pop.oss.kaolacdn.com/130cb755-bc31-4aaf-ab71-177470a814dd\\\" title=\\\"11.jpg\\\"/></p><p><img src=\\\"http://kaola-pop.oss.kaolacdn.com/6787e55e-e4ba-4523-94da-1ac8dc425bdd\\\" title=\\\"12.jpg\\\"/></p><p><img src=\\\"http://kaola-pop.oss.kaolacdn.com/c496ebda-ebda-4795-9274-c33f1faf2b22\\\" title=\\\"13.jpg\\\"/></p><p><img src=\\\"http://kaola-pop.oss.kaolacdn.com/08e82152-ca5b-4bf8-b441-31d7333a8b75\\\" title=\\\"14.jpg\\\"/></p><p><img src=\\\"http://kaola-pop.oss.kaolacdn.com/366f5e8c-10ec-4c4b-b10c-91e24856d3ef\\\" title=\\\"15.jpg\\\"/></p><p><br/></p>\",\"warehouse_id\":null,\"tax_code\":null,\"hs_code\":null,\"express_fee\":null,\"gross_weight\":\"0.7\",\"item_outer_id\":\"SS11111800-1\",\"unit_code\":null,\"property_valueId_list\":\"1004403|67539|67554|1004396|1004413|1004417|1004402|1004422|441620|1004423|3752579|1003330|1004167|4111421|1003218|1007984|3749667\",\"text_property_name_id\":\"100237^SS11111800\",\"image_urls\":null,\"sku_market_prices\":\"969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00\",\"sku_sale_prices\":\"969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00|969.00\",\"sku_barcode\":\"SS1111180041220|SS1111180041225|SS1111180041230|SS1111180041235|SS1111180041240|SS1111180041245|SS1111180070220|SS1111180070225|SS1111180070230|SS1111180070235|SS1111180070240|SS1111180070245\",\"sku_stock\":\"0|0|0|0|0|0|0|0|0|0|0|0\",\"sku_property_value\":\"100231:-1:41米白色:http://kaola-pop.oss.kaolacdn.com/7c76fca4-8f4e-4580-b9d4-827eb8a35570;100251:1003898:34|100231:-1:41米白色:http://kaola-pop.oss.kaolacdn.com/7c76fca4-8f4e-4580-b9d4-827eb8a35570;100251:1003893:35|100231:-1:41米白色:http://kaola-pop.oss.kaolacdn.com/7c76fca4-8f4e-4580-b9d4-827eb8a35570;100251:1003895:36|100231:-1:41米白色:http://kaola-pop.oss.kaolacdn.com/7c76fca4-8f4e-4580-b9d4-827eb8a35570;100251:1003900:37|100231:-1:41米白色:http://kaola-pop.oss.kaolacdn.com/7c76fca4-8f4e-4580-b9d4-827eb8a35570;100251:1003902:38|100231:-1:41米白色:http://kaola-pop.oss.kaolacdn.com/7c76fca4-8f4e-4580-b9d4-827eb8a35570;100251:1003899:39|100231:-1:70深黄色:http://kaola-pop.oss.kaolacdn.com/48e60ccb-9cdc-4ec8-83c3-0124ab26d2c7;100251:1003898:34|100231:-1:70深黄色:http://kaola-pop.oss.kaolacdn.com/48e60ccb-9cdc-4ec8-83c3-0124ab26d2c7;100251:1003893:35|100231:-1:70深黄色:http://kaola-pop.oss.kaolacdn.com/48e60ccb-9cdc-4ec8-83c3-0124ab26d2c7;100251:1003895:36|100231:-1:70深黄色:http://kaola-pop.oss.kaolacdn.com/48e60ccb-9cdc-4ec8-83c3-0124ab26d2c7;100251:1003900:37|100231:-1:70深黄色:http://kaola-pop.oss.kaolacdn.com/48e60ccb-9cdc-4ec8-83c3-0124ab26d2c7;100251:1003902:38|100231:-1:70深黄色:http://kaola-pop.oss.kaolacdn.com/48e60ccb-9cdc-4ec8-83c3-0124ab26d2c7;100251:1003899:39\",\"sku_outer_id\":\"SS1111180041220|SS1111180041225|SS1111180041230|SS1111180041235|SS1111180041240|SS1111180041245|SS1111180070220|SS1111180070225|SS1111180070230|SS1111180070235|SS1111180070240|SS1111180070245\",\"category_id\":1079,\"brand_id\":11825,\"KLVersion\":0,\"ApiGateway\":\"https://openapi.kaola.com/router?\",\"needUpload\":false}";
        #endregion
    }
}
