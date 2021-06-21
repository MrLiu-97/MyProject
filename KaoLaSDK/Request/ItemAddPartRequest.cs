// Decompiled with JetBrains decompiler
// Type: kaolaSDK.Request.ItemAddPartRequest
// Assembly: kaolaSDK, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 27A838E9-01B4-45C5-A3A7-14195802563E
// Assembly location: C:\Users\TaoQu\Desktop\kaolaSDK.dll

using kaolaSDK.Response;
using System.Collections.Generic;

namespace kaolaSDK.Request
{
    public class ItemAddPartRequest : BaseRequest<ItemAddPartResponse>
    {
        public override string HttpMethod
        {
            get
            {
                return "GET";
            }
        }

        public override string KLMethod
        {
            get
            {
                return "kaola.item.addPart";
            }
        }

        public string name { get; set; }

        public string sub_title { get; set; }

        public string short_title { get; set; }

        public string ten_words_desc { get; set; }

        public string item_NO { get; set; }

        public string original_country_code_id { get; set; }

        public string description { get; set; }

        public string warehouse_id { get; set; }

        public string tax_code { get; set; }

        public string hs_code { get; set; }

        public string express_fee { get; set; }

        public string gross_weight { get; set; }

        public string item_outer_id { get; set; }

        public string unit_code { get; set; }

        public string property_valueId_list { get; set; }

        public string text_property_name_id { get; set; }

        public string image_urls { get; set; }

        public string sku_market_prices { get; set; }

        public string sku_sale_prices { get; set; }

        public string sku_barcode { get; set; }

        public string sku_stock { get; set; }

        public string sku_property_value { get; set; }

        public string sku_outer_id { get; set; }

        public long category_id { get; set; }

        public long brand_id { get; set; }

        public override KLVersion KLVersion
        {
            get
            {
                return KLVersion.v1;
            }
        }

        public override Dictionary<string, object> GetParams()
        {
            return new Dictionary<string, object>()
      {
        {
          "name",
           name
        },
        {
          "sub_title",
           sub_title
        },
        {
          "short_title",
           short_title
        },
        {
          "ten_words_desc",
          ten_words_desc
        },
        {
          "item_NO",
          item_NO
        },
        {
          "brand_id",
          brand_id
        },
        {
          "original_country_code_id",
          original_country_code_id
        },
        {
          "description",
          description
        },
        {
          "category_id",
          category_id
        },
        {
          "warehouse_id",
          warehouse_id
        },
        {
          "tax_code",
          tax_code
        },
        {
          "hs_code",
          hs_code
        },
        {
          "express_fee",
          express_fee
        },
        {
          "gross_weight",
          gross_weight
        },
        {
          "item_outer_id",
          item_outer_id
        },
        {
          "unit_code",
          unit_code
        },
        {
          "property_valueId_list",
          property_valueId_list
        },
        {
          "text_property_name_id",
          text_property_name_id
        },
        {
          "image_urls",
          image_urls
        },
        {
          "sku_market_prices",
          sku_market_prices
        },
        {
          "sku_sale_prices",
          sku_sale_prices
        },
        {
          "sku_barcode",
          sku_barcode
        },
        {
          "sku_stock",
          sku_stock
        },
        {
          "sku_property_value",
          sku_property_value
        },
        {
          "sku_outer_id",
          sku_outer_id
        }
      };
        }
    }
}
