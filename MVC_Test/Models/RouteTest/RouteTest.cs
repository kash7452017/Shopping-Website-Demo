using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Test.Models.RouteTest
{
    public class RouteTest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        //取得目前所有商品的方法
        public static List<RouteTest> getAllProducts()
        {
            //初始化商品列表
            List<RouteTest> Result = new List<RouteTest>();
            //加入三項商品
            Result.Add(new RouteTest
            {
                ID = 1,
                Name = "自動鉛筆",
                Price = 30.0m
            });
            Result.Add(new RouteTest
            {
                ID = 2,
                Name = "筆記本",
                Price = 50.0m
            });
            Result.Add(new RouteTest
            {
                ID = 3,
                Name = "橡皮擦",
                Price = 10.0m
            });
            return Result; // 回傳商品列表
        }
    }
}