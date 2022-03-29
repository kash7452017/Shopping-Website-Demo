using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Test.Models
{
    public class ManageUser
    {
        [Required] // 必要欄位
        public string Id { get; set; }

        [Required] // 必要欄位
        [StringLength(256, ErrorMessage = "{0} 的長度必須至少為 {2} 個字元。", MinimumLength = 1)] //字元長度1~256
        [Display(Name = "暱稱")] // 欄位顯示文字
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }
}