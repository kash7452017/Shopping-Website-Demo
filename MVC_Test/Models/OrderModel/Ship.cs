using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Test.Models.OrderModel
{
    public class Ship
    {
        /// <summary>
        /// 收貨人姓名
        /// </summary>
        [Required]
        [Display(Name = "收貨人姓名")]
        [StringLength(30,ErrorMessage = "{0} 的長度必須至少為 {2} 個字元。",MinimumLength = 2)] // 字元長度8~256
        public string RecieverName { get; set; }

        ///<summary>
        ///收貨人電話
        ///</summary>
        [Required]
        [Display(Name = "收貨人電話")]
        [StringLength(15,ErrorMessage = "{0} 的長度必須至少為 {2} 個字元。",MinimumLength = 8)] // 字元長度8~15
        public string RecieverPhone { get; set; }

        ///<summary>
        ///收貨人住址
        ///</summary>
        [Required]
        [Display(Name = "收貨人住址")]
        [StringLength(256,ErrorMessage = "{0} 的長度必須至少為 {2} 個字元。",MinimumLength = 8)] // 字元長度8~256
        public String ReciecerAddress { get; set; }

    }
}