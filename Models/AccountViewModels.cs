using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Models
{
    public class RegisterViewModel
    {
       
        [MaxLength(300, ErrorMessage = "ایمیل نمی‌تواند بیش از ۳۰۰ کاراکتر باشد!")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده معتبر نیست!")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "کلمه‌عبور نمی‌تواند بیش از ۵۰ کاراکتر باشد!")]
        [MinLength(4, ErrorMessage = "کلمه‌عبور نمی‌تواند کمتر از 4 کاراکتر باشد!")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه‌عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "تکرار کلمه‌عبور نمی‌تواند بیش از ۵۰ کاراکتر باشد!")]
        [MinLength(4, ErrorMessage = "کلمه‌عبور نمی‌تواند کمتر از 4 کاراکتر باشد!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه‌عبور و تکرار آن یکسان نیستند!")]
        [Display(Name = "تکرار کلمه‌عبور")]
        public string RePassword { get; set; }

    }

    public class LoginViewModel
    {
       
        [MaxLength(300, ErrorMessage = "ایمیل نمی‌تواند بیش از ۳۰۰ کاراکتر باشد!")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده معتبر نیست!")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "کلمه‌عبور نمی‌تواند بیش از ۵۰ کاراکتر باشد!")]
        [MinLength(4, ErrorMessage = "کلمه‌عبور نمی‌تواند کمتر از 4 کاراکتر باشد!")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه‌عبور")]
        public string Password { get; set; }

        [Display(Name = "من را به خاطر بسپار")]
        public bool RememberMe { get; set; }

    }

    public class UserViewModel
    {
        public int Id { get; set; }

        [MaxLength(300, ErrorMessage = "ایمیل نمی‌تواند بیش از ۳۰۰ کاراکتر باشد!")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده معتبر نیست!")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "کلمه‌عبور نمی‌تواند بیش از ۵۰ کاراکتر باشد!")]
        [MinLength(4, ErrorMessage = "کلمه‌عبور نمی‌تواند کمتر از 4 کاراکتر باشد!")]
        [Display(Name = "کلمه‌عبور")]
        public string Password { get; set; }

        [Display(Name = "سطح دسترسی ادمین")]
        public bool IsAdmin { get; set; }
    }
}
