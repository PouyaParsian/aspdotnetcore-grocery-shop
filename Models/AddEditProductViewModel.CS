using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyEshop.Models
{
    public class AddEditProductViewModel
    {
        public int Id { get; set; }

        [MaxLength(15, ErrorMessage = "نام محصول نمی‌تواند بیش از 15 کاراکتر باشد!")]
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        public string Name { get; set; }

        [MaxLength(40, ErrorMessage = "توضیحات محصول نمی‌تواند بیش از 40 کاراکتر باشد!")]
        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        public string Description { get; set; }

        [Display(Name = "دسته‌بندی محصول")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [Range(1, int.MaxValue, ErrorMessage = "لطفا یک دسته‌بندی معتبر انتخاب کنید!")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [Range(1, 10000000, ErrorMessage = "قیمت باید بزرگ‌تر از 0 و کم‌تر از 10,000,000 تومان باشد!")]
        public decimal Price { get; set; }

        [Display(Name = "موجودی محصول")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [Range(1, int.MaxValue, ErrorMessage = "موجودی باید بزرگ‌تر از ۰ باشد!")]
        public int QuantityInStock { get; set; }

        [Display(Name = "تصویر محصول (پسوند WebP)")]
        [Required(ErrorMessage = "لطفاً {0} را انتخاب کنید.")]
        public IFormFile Picture { get; set; }

    }
}