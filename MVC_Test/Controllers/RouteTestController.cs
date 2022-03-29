using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Test.Controllers
{
    //此為測試路由器(Route)基本操作，不帶參數&附帶多參數
    public class RouteTestController : Controller
    {
        // GET: RouteTest
        //這是路由器不帶參數方式
        public ActionResult Index()
        {
            // 利用自定義Models中的類別方式取得所有商品資訊
            var Result = Models.RouteTest.RouteTest.getAllProducts();
            //將所有商品資訊(Result)傳送給View顯示
            return View(Result);
        }

        //這是路由器帶一參數
        public ActionResult Index2(string id)
        {
            return Content(string.Format("這是Index2，id值為：{0}", id));
        }
        //這是路由器帶多參數
        public ActionResult Index3(String id,string page)
        {
            return Content(string.Format("這是Index3，id的值為：{0}；page的值為{1}", id, page));
        }
    }
}