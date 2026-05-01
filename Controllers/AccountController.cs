using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Models;
using MyEshop.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MyEshop.Controllers;

public class AccountController : Controller
{
    private MyEShopDbContext _myEShopDbContext;

    public AccountController(MyEShopDbContext myEShopDbContext)
    {
        _myEShopDbContext = myEShopDbContext;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel register)
    {
        if (!ModelState.IsValid)
        {
            return View(register);
        }

        bool isExistUserByEmail = _myEShopDbContext.Users.Any(u => u.Email == register.Email);

        if (isExistUserByEmail)
        {
            ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است.");
            return View(register);
        }
       
        User user = new User()
        {
            Email = register.Email.ToLower().Trim(),
            Password = register.Password,
            RegisterDate = DateTime.Now,
            IsAdmin = false
        };
        _myEShopDbContext.Users.Add(user);
        _myEShopDbContext.SaveChanges();

        TempData["RegisterSuccess"] = true;
        
        return RedirectToAction("SuccessRegister", new { email = register.Email });
    }

    [Authorize]
    public IActionResult SuccessRegister(string email)
    {
        if (TempData["RegisterSuccess"] == null)
        {
            return RedirectToAction("Register");
        }

        var model = new RegisterViewModel { Email = email };
        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }

        var user = _myEShopDbContext.Users
            .SingleOrDefault(u => u.Email == login.Email.ToLower().Trim() && u.Password == login.Password);

        if (user == null)
        {
            ModelState.AddModelError("Email", "اطلاعات صحیح نمی‌باشد!");
            ModelState.AddModelError("Password", "اطلاعات صحیح نمی‌باشد!");
            return View(login);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("IsAdmin", user.IsAdmin.ToString())

        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties
        {
            IsPersistent = login.RememberMe
        };

        HttpContext.SignInAsync(principal, properties);

        return Redirect("/");
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/Account/Login");
    }

    [Authorize]
    [Route("Account")]
    public IActionResult Account()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
        
        var user = _myEShopDbContext.Users
            .Include(o => o.Orders)
            .ThenInclude(od => od.OrderDetails)
            .SingleOrDefault(u => u.Id == userId);

        return View(user);
    }

    [Authorize]
    public IActionResult RemoveItemFromPaymentHistory(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var order = _myEShopDbContext.Orders.FirstOrDefault(o => o.Id == id && o.UserId == userId);

        if (order != null)
        {
            _myEShopDbContext.Orders.Remove(order);
            _myEShopDbContext.SaveChanges();
        }

        return RedirectToAction("Account");
    }

    [Authorize]
    public IActionResult OrderDetails(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var order = _myEShopDbContext.Orders.Where(o => o.Id == id && o.UserId == userId)
            .Include(o => o.OrderDetails)
            .ThenInclude(c => c.Product).FirstOrDefault();

        if (order == null)
        {
            return BadRequest();
        }

        return View(order);
    }

}