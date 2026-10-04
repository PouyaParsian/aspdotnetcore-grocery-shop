using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Models;
using MyEshop.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MyEshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyEShopDbContext _myEShopDbContext;

    public HomeController(ILogger<HomeController> logger, MyEShopDbContext myEShopDbContext)
    {
        _logger = logger;
        _myEShopDbContext = myEShopDbContext;
    }

    public IActionResult Index()
    {
        var products = _myEShopDbContext.Products.Include(p => p.Items).ToList();
        return View(products);
    }

    public IActionResult SearchProduct(string value)
    {
        var products = _myEShopDbContext.Products
            .Include(p => p.Items)
            .Where(p => p.Name.Contains(value) || p.Description.Contains(value))
            .ToList();

        return View(products);
    }


    public IActionResult ProductDetails(int id)
    {
        var product = _myEShopDbContext.Products
            .Include(p => p.Items)
            .Include(p => p.Categories)
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        var vm = new DetailsViewModel
        {
            Product = product,
            Categories = product.Categories
        };

        return View(vm);
    }

    [Route("Group/{id}")]
    public IActionResult ShowProductByGroupId(int id)
    {
        var products = _myEShopDbContext.Products
            .Where(p => p.Categories.Any(c => c.Id == id))
            .Include(p => p.Items)
            .ToList();

        return View(products);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
