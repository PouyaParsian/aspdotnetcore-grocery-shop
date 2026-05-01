using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyEshop.Models
{
    public class AddEditCategoryViewModel
    {
        public int Id { get; set; }

        [MaxLength(15, ErrorMessage = "نام دسته‌بندی نمی‌تواند بیش از 15 کاراکتر باشد!")]
        [Display(Name = "نام دسته‌بندی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        public string Name { get; set; }

        [MaxLength(40, ErrorMessage = "توضیحات دسته‌بندی نمی‌تواند بیش از 40 کاراکتر باشد!")]
        [Display(Name = "توضیحات دسته‌بندی")]
        public string? Description { get; set; }
    }
}