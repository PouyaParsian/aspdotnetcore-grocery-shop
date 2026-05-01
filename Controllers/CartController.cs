using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Models;
using MyEshop.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MyEshop.Controllers;

public class CartController : Controller
{
    private MyEShopDbContext _myEShopDbContext;

    public CartController(ILogger<HomeController> logger, MyEShopDbContext myEShopDbContext)
    {
        _myEShopDbContext = myEShopDbContext;
    }

    [Authorize]
    public IActionResult AddToCart(int id)
    {
        var product = _myEShopDbContext.Products.Include(p => p.Items).SingleOrDefault(p => p.Id == id);

        if (product != null)
        {
            var item = product.Items.FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Cart");
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _myEShopDbContext.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);

            if (order != null)
            {
                var orderDetail = _myEShopDbContext.OrderDetails.FirstOrDefault(d => d.OrderId == order.Id && d.ProductId == product.Id);
                
                if (orderDetail != null)
                {
                    if (orderDetail.Count + 1 > item.QuantityInStock)
                    {
                        return RedirectToAction("Cart");
                    }

                    orderDetail.Count += 1;
                }

                else
                {
                    if (1 > item.QuantityInStock)
                    {
                        return RedirectToAction("Cart");
                    }

                    _myEShopDbContext.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Price = product.Items.FirstOrDefault().Price,
                        Count = 1
                    });
                }
            }

            else
            {
                if (1 > item.QuantityInStock)
                {
                    return RedirectToAction("Cart");
                }

                order = new Order()
                {
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    UserId = userId
                };

                _myEShopDbContext.Orders.Add(order);
                _myEShopDbContext.SaveChanges();

                _myEShopDbContext.OrderDetails.Add(new OrderDetail()
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Price = product.Items.FirstOrDefault().Price,
                    Count = 1
                });
            }

            _myEShopDbContext.SaveChanges();
        }
        
        return RedirectToAction("Cart");
    }

    [Authorize]
    public IActionResult Cart()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
        var order = _myEShopDbContext.Orders.Where(o => o.UserId == userId && !o.IsFinaly)
            .Include(o => o.OrderDetails)
            .ThenInclude(c => c.Product).FirstOrDefault();

        return View(order);
    }

    [Authorize]
    public IActionResult DecreaseItemFromCart(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var orderDetail = _myEShopDbContext.OrderDetails
            .Include(o => o.Order)
            .FirstOrDefault(o => o.Id == id && o.Order.UserId == userId);

        if (orderDetail != null)
        {
            if (orderDetail.Count > 1)
                orderDetail.Count -= 1;
            else
                _myEShopDbContext.Remove(orderDetail);

            _myEShopDbContext.SaveChanges();
        }

        return RedirectToAction("Cart");
    }

    [Authorize]
    public IActionResult RemoveItemFromCart(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var orderDetail = _myEShopDbContext.OrderDetails
            .Include(o => o.Order)
            .FirstOrDefault(o => o.Id == id && o.Order.UserId == userId);

        if (orderDetail != null)
        {
            _myEShopDbContext.Remove(orderDetail);
            _myEShopDbContext.SaveChanges();
        }

        return RedirectToAction("Cart");
    }

    [Authorize]
    public IActionResult Payment()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var order = _myEShopDbContext.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ThenInclude(p => p.Items)
            .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);

        if (order != null)
        {
            foreach (var detail in order.OrderDetails)
            {
                var item = detail.Product.Items.FirstOrDefault();
                if (item == null)
                    continue;

                item.QuantityInStock -= detail.Count;
            }

            order.IsFinaly = true;
            _myEShopDbContext.SaveChanges();
        }

        return View();
    }
}