using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Test.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: ManageUser
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            using (Models.UserEntities db = new Models.UserEntities()) 
            {
                // 抓取所有AspNetUsers中的資料，並且放入Models.ManageUser模型中
                var result = (from s in db.AspNetUsers
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).ToList();
                return View(result);
            }
        }
        public ActionResult Edit(string id)
        {
            using (Models.UserEntities db = new Models.UserEntities()) 
            {
                var result = (from s in db.AspNetUsers where s.Id == id select s).FirstOrDefault();
                if (result != default(Models.AspNetUser)) // 判斷此id是否有資料
                {
                    // 如果有，則回傳會員管理編輯頁面
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
        public ActionResult Edit(Models.ManageUser postback)
        {
            // 判斷使用者輸入資料是否正確
            if (this.ModelState.IsValid)
            {
                using (Models.UserEntities db = new Models.UserEntities())
                {
                    // 抓取User.Id等於回傳postback.Id的資料
                    var result = (from s in db.AspNetUsers where s.Id == postback.Id select s).FirstOrDefault();
                    // 儲存使用者變更資料
                    result.Id = postback.Id;
                    result.UserName = postback.UserName;
                    result.Email = postback.Email;
                    // 儲存所有變更
                    db.SaveChanges();
                    // 設定成功訊息並導回至Index頁面
                    TempData["ResultMessage"] = String.Format("管理員[{0}]成功編輯", postback.UserName);
                    return RedirectToAction("Index");
                }
            }
            // 如果資料不正確則導向自己(Edit頁面)
            else
            {
                return View(postback);
            }
        }        
    }
}