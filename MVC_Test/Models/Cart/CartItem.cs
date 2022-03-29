using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Test.Models
{
    [Serializable]// 可序列化
    public class CartItem // 購物車內單一商品類別
    {
        // 商品編號
        public int Id { get; set; }

        // 商品名稱
        public string Name { get; set; }

        // 商品購買時的價格
        public decimal Price { get; set; }

        // 商品購買數量
        public int Quantity { get; set; }

        // 商品小計
        public decimal Amount
        {
            get { return this.Price * this.Quantity; }
        }

        // 回傳對應商品圖片圖片
        public string DefaultImageURL(string name)
        {
            // 宣告回傳商品列表result
            List<Models.Product> result = new List<Models.Product>();
            using (Models.MVC_TestEntities db = new Models.MVC_TestEntities())
            {
                // 使用LinQ語法抓取目前Products資料庫中所有資料
                result = (from s in db.Products where s.Name == name select s).ToList();
                // 將result傳送給檢視
                return result[0].DefaultImageURL;
            }
        }
    }
}