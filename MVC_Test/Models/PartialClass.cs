using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Test.Models
{
    public class PartialClass
    {
    }

    // 定義Models.Order的部分類別
    public partial class Order
    {
        // 取得訂單中的使用者暱稱
        public string GetUserName()
        {
            // 使用Order類別中的UserId到AspNetUsers資料表中搜尋出UserName
            using (Models.UserEntities db = new UserEntities()) 
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == this.UserId
                              select s.UserName).FirstOrDefault();

                // 回傳找到的UserName
                return result;
            }
        }
    }

    // 仿照Models.Order部分類別的作法
    // 定義Models.ProductComment的部分類別
    public partial class ProductComment
    {
        // 取得留言中的使用者暱稱
        public string GetUserName_For_message_board()
        {
            // 使用ProductComment類別中的UserId到AspNetUsers資料表中搜尋出UserName
            using (Models.UserEntities db = new UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == this.UserId
                              select s.UserName).FirstOrDefault();

                // 回傳找到的UserName
                return result;
            }
        }
    }
}