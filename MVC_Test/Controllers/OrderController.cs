using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Test.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.OrderModel.Ship postback)
        {
            if (this.ModelState.IsValid) 
            {
                // 取得目前購物車
                var currentcart = Models.Operation.GetCurrentCart();
                // 取得目前登入使用者Id
                var userId = HttpContext.User.Identity.GetUserId();

                using (Models.MVC_TestEntities db = new Models.MVC_TestEntities()) 
                {
                    // 建立Order物件
                    var order = new Models.Order()
                    {
                        UserId = userId,
                        RecieverName = postback.RecieverName,
                        RecieverPhone = postback.RecieverPhone,
                        RecieverAddress = postback.RecieverPhone
                    };
                    // 加入Orders資料表後，儲存變更
                    db.Order.Add(order);
                    db.SaveChanges();

                    // 取得購物車中OrderDetail物件
                    foreach (var item in currentcart)
                    {
                        var orderDetails = new Models.OrderDetail()
                        {
                            Id = item.Id,
                            OrderId = order.Id,
                            Name = item.Name,
                            Price = item.Price,
                            Quantity = item.Quantity
                        };
                        db.OrderDetail.Add(orderDetails);
                    }
                    db.SaveChanges();
                }
                Response.Write("<script language=javascript>alert('訂購成功')</script>");
            }
            return View();
        }

        public ActionResult MyOrder()
        {
            // 取得目前登入使用者Id
            var userId = HttpContext.User.Identity.GetUserId();
            using (Models.MVC_TestEntities db = new Models.MVC_TestEntities()) 
            {
                var result = (from s in db.Order
                              where s.UserId == userId
                              select s).ToList();

                return View(result);
            }
        }

        public ActionResult MyOrderDetail(int id)
        {
            using (Models.MVC_TestEntities db = new Models.MVC_TestEntities()) 
            {
                var result = (from s in db.OrderDetail
                              where s.OrderId == id
                              select s).ToList();

                if (result.Count == 0) 
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }
    }
}