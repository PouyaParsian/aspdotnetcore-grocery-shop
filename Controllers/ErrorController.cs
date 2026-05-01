using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Models;
using MyEshop.Data;

namespace MyEshop.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        switch (statusCode)
        {
            case 404:
                ViewBag.ErrorMessage = "صفحه مورد نظر یافت نشد!";
                return View("NotFound");

            case 500:
                ViewBag.ErrorMessage = "سرور در دسترس نمی‌باشد!";
                return View("ServerError");

            default:
                ViewBag.ErrorMessage = "مشکلی پیش آمده است!";
                return View("GeneralError");
        }
    }
}

