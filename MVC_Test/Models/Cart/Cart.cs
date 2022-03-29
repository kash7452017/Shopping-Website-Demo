using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Test.Models
{
    [Serializable] // 可序列化
    public class Cart : IEnumerable<CartItem> // 購物車類別
    {
        // 建構值
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        // 儲存所有商品
        private List<CartItem> cartItems;

        /// <summary>
        /// 取得購物車內商品的總數量
        /// </summary>
        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }
        // 取得商品總價
        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach (var cartitem in this.cartItems)
                {
                    totalAmount = totalAmount + cartitem.Amount;
                }
                return totalAmount;
            }
        }

        // 新增一筆Product，使用ProductId
        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems.Where(s => s.Id == ProductId).Select(s => s).FirstOrDefault();

            // 判斷相同Id的CartItem是否已經存在購物車內
            if (findItem == default(Models.CartItem))
            {
                // 不存在購物車內，則新增一筆
                using (Models.MVC_TestEntities db = new MVC_TestEntities())
                {
                    var product = (from s in db.Products
                                   where s.Id == ProductId
                                   select s).FirstOrDefault();
                    if (product != default(Models.Product))
                    {
                        this.AddProduct(product);
                    }
                }
            }
            else
            {
                // 存在購物車內，則將商品數量累加
                findItem.Quantity++;
            }
            return true;
        }

        private bool AddProduct(Product product)
        {
            // 將Product轉為CartItem
            var cartItem = new Models.CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1
        };

            // 加入CartItem至購物車
            this.cartItems.Add(cartItem);
            return true;
        }

        // 移除一筆Product，使用ProductId
        public bool RemoveProduct(int ProductId)
        {
            var findItem = this.cartItems
                .Where(s => s.Id == ProductId)
                .Select(s => s)
                .FirstOrDefault();

            // 判斷相同Id的CartItem是否已經存在購物車內
            if (findItem == default(Models.CartItem)) 
            {
                // 不存在購物車內，不需要做任何動作
            }
            else
            {
                // 存在購物車內，則將商品移除
                this.cartItems.Remove(findItem);
            }
            return true;
        }

        // 清空購物車
        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }
        public IEnumerator<CartItem> GetEnumerator()
        {
            return ((IEnumerable<CartItem>)cartItems).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)cartItems).GetEnumerator();
        }
    }
}