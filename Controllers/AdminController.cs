using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Models;
using MyEshop.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyEshop.Controllers;

public class AdminController : Controller
{
    private MyEShopDbContext _myEShopDbContext;

    public AdminController(MyEShopDbContext myEShopDbContext)
    {
        _myEShopDbContext = myEShopDbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Product()
    {
        var products = _myEShopDbContext.Products.Include(p => p.Items).Include(p => p.Categories).ToList();
        return View(products);
    }

    public IActionResult User()
    {
        var users = _myEShopDbContext.Users.ToList();
        return View(users);
    }

    public IActionResult Category()
    {
        var categories = _myEShopDbContext.Categories.Include(c => c.Products).ToList();
        return View(categories);
    }

    private void LoadCategories(AddEditProductViewModel vm)
    {
        vm.Categories = _myEShopDbContext.Categories
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToList();
    }

    public IActionResult AddProduct()
    {
        var vm = new AddEditProductViewModel();
        LoadCategories(vm);
        return View(vm);
    }

    [HttpPost]
    public IActionResult AddProduct(AddEditProductViewModel addProduct)
    {
        if (!ModelState.IsValid)
        {
            LoadCategories(addProduct);
            return View(addProduct);
        }

        var category = _myEShopDbContext.Categories
            .FirstOrDefault(c => c.Id == addProduct.CategoryId);

        if (category == null)
        {
            LoadCategories(addProduct);
            ModelState.AddModelError("CategoryId", "دسته‌بندی انتخاب‌شده یافت نشد.");
            return View(addProduct);
        }

        if (addProduct.Picture?.Length > 0)
        {
            var ext = Path.GetExtension(addProduct.Picture.FileName).ToLower();
            var mime = addProduct.Picture.ContentType.ToLower();

            if (ext != ".webp" || mime != "image/webp")
            {
                LoadCategories(addProduct);
                ModelState.AddModelError("Picture", "فقط فایل‌های WebP قابل قبول هستند!");
                return View(addProduct);
            }
        }

        var product = new Product()
        {
            Name = addProduct.Name,
            Description = addProduct.Description,
            Items = new List<Item>()
            {
                new Item()
                {
                    Price = addProduct.Price,
                    QuantityInStock = addProduct.QuantityInStock
                }
            }
        };

        product.Categories.Add(category);

        _myEShopDbContext.Products.Add(product);
        _myEShopDbContext.SaveChanges();

        if (addProduct.Picture?.Length > 0)
        {
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                product.Id + ".webp");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                addProduct.Picture.CopyTo(stream);
            }
        }

        return RedirectToAction("Product");
    }


    public IActionResult EditProduct(int id)
    {
        var product = _myEShopDbContext.Products
            .Include(p => p.Categories)
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        var item = _myEShopDbContext.Items
            .FirstOrDefault(i => i.ProductId == id);

        if (item == null)
            return NotFound();

        var categoryId = product.Categories.FirstOrDefault()?.Id ?? 0;

        var vm = new AddEditProductViewModel()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = categoryId,
            Price = item.Price,
            QuantityInStock = item.QuantityInStock
        };

        vm.Categories = _myEShopDbContext.Categories
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToList();

        return View(vm);
    }

    [HttpPost]
    public IActionResult EditProduct(AddEditProductViewModel editProduct)
    {
        if (!ModelState.IsValid)
        {
            LoadCategories(editProduct);
            return View(editProduct);
        }

        var product = _myEShopDbContext.Products
            .Include(p => p.Categories)
            .FirstOrDefault(p => p.Id == editProduct.Id);

        if (product == null)
            return NotFound();

        var item = _myEShopDbContext.Items
            .FirstOrDefault(i => i.ProductId == editProduct.Id);

        if (item == null)
            return NotFound();

        var category = _myEShopDbContext.Categories
            .FirstOrDefault(c => c.Id == editProduct.CategoryId);

        if (category == null)
        {
            LoadCategories(editProduct);
            ModelState.AddModelError("CategoryId", "دسته‌بندی انتخاب‌شده یافت نشد.");
            return View(editProduct);
        }

        if (editProduct.Picture?.Length > 0)
        {
            var ext = Path.GetExtension(editProduct.Picture.FileName).ToLower();
            var mime = editProduct.Picture.ContentType.ToLower();

            if (ext != ".webp" || mime != "image/webp")
            {
                LoadCategories(editProduct);
                ModelState.AddModelError("Picture", "فقط فایل‌های WebP قابل قبول هستند!");
                return View(editProduct);
            }
        }

        product.Name = editProduct.Name;
        product.Description = editProduct.Description;

        item.Price = editProduct.Price;
        item.QuantityInStock = editProduct.QuantityInStock;

        product.Categories.Clear();
        product.Categories.Add(category);

        _myEShopDbContext.SaveChanges();

        if (editProduct.Picture?.Length > 0)
        {
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                product.Id + ".webp");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                editProduct.Picture.CopyTo(stream);
            }
        }

        return RedirectToAction("Product");
    }

    public IActionResult DeleteProduct(int id)
    {
        var product = _myEShopDbContext.Products
            .Include(p => p.Categories)
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        var item = _myEShopDbContext.Items
            .FirstOrDefault(i => i.ProductId == id);

        if (item == null)
            return NotFound();

        _myEShopDbContext.Items.Remove(item);
        _myEShopDbContext.Products.Remove(product);

        _myEShopDbContext.SaveChanges();

        string filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "images",
            product.Id + ".webp");
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        return RedirectToAction("Product");

    }

    public IActionResult AddCategory()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddCategory(AddEditCategoryViewModel addCategory)
    {
        if (!ModelState.IsValid)
        {
            return View(addCategory);
        }

        var category = new Category()
        {
            Name = addCategory.Name,
            Description = addCategory.Description ?? ""
        };

        _myEShopDbContext.Categories.Add(category);
        _myEShopDbContext.SaveChanges();

        return RedirectToAction("Category");
    }

    public IActionResult DeleteCategory(int id)
    {
        var category = _myEShopDbContext.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        _myEShopDbContext.Categories.Remove(category);
        _myEShopDbContext.SaveChanges();

        return RedirectToAction("Category");
    }

    public IActionResult EditCategory(int id)
    {
        var category = _myEShopDbContext.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        var vm = new AddEditCategoryViewModel()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult EditCategory(AddEditCategoryViewModel editCategory)
    {
        if (!ModelState.IsValid)
        {
            return View(editCategory);
        }

        var category = _myEShopDbContext.Categories.Find(editCategory.Id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = editCategory.Name;
        category.Description = editCategory.Description ?? "";

        _myEShopDbContext.SaveChanges();
        
        return RedirectToAction("Category");
    }

    public IActionResult AddUser()
    {
        return View();
    }

    public IActionResult DeleteUser(int id)
    {
        var user = _myEShopDbContext.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        var orders = _myEShopDbContext.Orders
            .Where(o => o.UserId == id)
            .ToList();

        foreach (var o in orders)
        {
            var details = _myEShopDbContext.OrderDetails
                .Where(od => od.OrderId == o.Id)
                .ToList();

            _myEShopDbContext.OrderDetails.RemoveRange(details);
        }

        _myEShopDbContext.Orders.RemoveRange(orders);
        _myEShopDbContext.Users.Remove(user);

        _myEShopDbContext.SaveChanges();

        return RedirectToAction("User");
    }

    public IActionResult EditUser(int id)
    {
        var user = _myEShopDbContext.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        var vm = new UserViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
            IsAdmin = user.IsAdmin
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult EditUser(UserViewModel editUser)
    {
        if (!ModelState.IsValid)
        {
            return View(editUser);
        }

        var user = _myEShopDbContext.Users.Find(editUser.Id);
        if (user == null)
        {
            return NotFound();
        }

        user.Email = editUser.Email;
        user.Password = editUser.Password;
        user.IsAdmin = editUser.IsAdmin;

        _myEShopDbContext.SaveChanges();
        
        return RedirectToAction("User");
    }

    [HttpPost]
    public IActionResult AddUser(UserViewModel editUser)
    {
        if (!ModelState.IsValid)
        {
            return View(editUser);
        }

        var user = new User()
        {
            Id = editUser.Id,
            Email = editUser.Email.ToLower().Trim(),
            Password = editUser.Password,
            RegisterDate = DateTime.Now,
            IsAdmin = editUser.IsAdmin
        };

        _myEShopDbContext.Users.Add(user);
        _myEShopDbContext.SaveChanges();

        return RedirectToAction("User");
    }

}