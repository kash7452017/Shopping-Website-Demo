using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Test.Controllers
{
    public class ProductController : Controller
    {
        // 建立商品頁面
        public ActionResult Create()
        {
            return View();
        }

        //建立商品頁面 - 資料回傳處理
        [HttpPost]//指定只有使用POST方法才可進入
        public ActionResult Create(Models.Product postback)
        {
            if (this.ModelState.IsValid) // 如果資料驗證成功
            {
                using (Models.MVC_TestEntities db = new Models.MVC_TestEntities())
                {

                    // 將回傳資料postback加入至Products
                    db.Products.Add(postback);
                    // 儲存異動資料
                    db.SaveChanges();
                    // 設定成功訊息
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功建立", postback.Name);
                    //轉導Product/Index頁面
                    return RedirectToAction("Index");
                }
            }
            // 失敗訊息
            ViewBag.ResultMessage = "資料有誤，請檢查";
            //停留在Create頁面
            return View(postback);
        }

        public ActionResult Index()
        {
            // 宣告回傳商品列表result
            List<Models.Product> result = new List<Models.Product>();
            // 接收轉導的成功訊息
            ViewBag.ResultMessage = TempData["ResultMessage"];
            // 使用MVC_TestEntities類別，名稱為db
            using (Models.MVC_TestEntities db = new Models.MVC_TestEntities())
            {
                // 使用LinQ語法抓取目前Products資料庫中所有資料
                result = (from s in db.Products select s).ToList();
                // 將result傳送給檢視
                return View(result);
            }
        }
        public ActionResult Edit(int id)
        {
            using (Models.MVC_TestEntities db = new Models.MVC_TestEntities())
            {
                // 抓取Product.id等於輸入id的資料
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if (result != default(Models.Product)) // 判斷此id是否有資料
                {
                    // 如果有，則回傳編輯商品頁面
                    return View(result);
                }
                else
                {
                    // 如果沒有資料，則顯示錯誤訊息並導回至Index頁面
                    TempData["resultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost] // 編輯商品頁面 - 資料回傳處理
        public ActionResult Edit(Models.Product postback)
        {
            // 判斷使用者輸入資料是否正確
            if (this.ModelState.IsValid)
            {
                using (Models.MVC_TestEntities db = new Models.MVC_TestEntities())
                {
                    // 抓取Product.Id等於回傳postback.Id的資料
                    var result = (from s in db.Products where s.Id == postback.Id select s).FirstOrDefault();
                    // 儲存使用者變更資料
                    result.Name = postback.Name;
                    result.PublishDate = postback.PublishDate;
                    result.Status = postback.Status;
                    result.DefaultImageId = postback.DefaultImageId;
                    result.Price = postback.Price;
                    result.Quantity = postback.Quantity;
                    result.CategoryId = postback.CategoryId;
                    result.Description = postback.Description;
                    result.DefaultImageURL = postback.DefaultImageURL;
                    // 儲存所有變更
                    db.SaveChanges();
                    // 設定成功訊息並導回至Index頁面
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功編輯", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            // 如果資料不正確則導向自己(Edit頁面)
            else
            {
                return View(postback);
            }
        }

        // 刪除商品
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Models.MVC_TestEntities db = new Models.MVC_TestEntities())
            {
                // 抓取Product.Id等於輸入Id資料
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if (result != default(Models.Product)) // 判斷此Id是否有資料
                {
                    db.Products.Remove(result);
                    // 儲存所有變更
                    db.SaveChanges();

                    // 設定成功訊息並導回至Index頁面
                    TempData["ResultMessage"] = string.Format("商品[{0}]成功刪除", result.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    // 如果沒有資料則顯示錯誤訊息並導回至Index頁面
                    TempData["ResultMessage"] = "指定資料不存在，無法刪除，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}