using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Test.Controllers
{
    public class TestController : Controller
    {
        public ActionResult GetCart()
        {            
            // 取得目前購物車
            var cart = Models.Operation.GetCurrentCart();
            cart.AddProduct(1);
            //// 如果購物車內無任何商品，則新增一筆
            //if (cart.cartItems.Count == 0) 
            //{
            //    cart.cartItems.Add(
            //        new Models.CartItem()
            //        {
            //            Id = 1,
            //            Name = "測試",
            //            Quantity = 1,
            //            Price = 100m
            //        }) ;
            //}
            //else // 若有商品，則將第一筆商品數量+1
            //{
            //    cart.cartItems.First().Quantity++;
            //}

            // 回傳目前購物車中的商品總價
            return Content(string.Format("目前購物車總共： {0}元", cart.TotalAmount));
        }
    }
}