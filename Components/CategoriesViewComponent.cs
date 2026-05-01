using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Models;
using MyEshop.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class CategoriesViewComponent : ViewComponent
{
    private MyEShopDbContext _myEShopDbContext;

    public CategoriesViewComponent(MyEShopDbContext myEShopDbContext)
    {
        _myEShopDbContext = myEShopDbContext;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _myEShopDbContext.Categories
            .Include(c => c.Products)
            .ToList();
            
        return View(categories);
    }
}