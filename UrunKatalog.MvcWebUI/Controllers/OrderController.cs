﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunKatalog.MvcWebUI.Entity;
using UrunKatalog.MvcWebUI.Models;

namespace UrunKatalog.MvcWebUI.Controllers
{
	[Authorize(Roles = "admin")]//admin rolü olanlar bu action methodu görebilir
	public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Select(i => new AdminOrderModel()
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Total = i.Total,
                Count = i.OrderLines.Count()
            }).OrderByDescending(i => i.OrderDate).ToList();


            return View(orders);
        }
        public ActionResult Details(int id) 
        {
			var entity = db.Orders.Where(i => i.Id == id)
								  .Select(i => new OrderDetailsModel()
								  {
									  OrderId = i.Id,
									  UserName=i.Username,
									  OrderNumber = i.OrderNumber,
									  Total = i.Total,
									  OrderDate = i.OrderDate,
									  OrderState = i.OrderState,
									  AdresBasligi = i.AdresBasligi,
									  Adres = i.Adres,
									  Sehir = i.Sehir,
									  Semt = i.Semt,
									  Mahalle = i.Mahalle,
									  PostaKodu = i.PostaKodu,
									  OrderLines = i.OrderLines.Select(a => new OrderLineModel()
									  {
										  ProductId = a.ProductId,
										  ProductName = a.Product.Name.Length > 50 ? a.Product.Name.Substring(0, 47) + "..." : a.Product.Name,//ürün 50 karakterden büyükse sonuna .. koy değilse direkt hepsini yaz koşulunu ekledik, nameyi alırken
										  Image = a.Product.Image,
										  Quantity = a.Quantity,
										  Price = a.Price
									  }).ToList()
								  }).FirstOrDefault();
			return View(entity);
		}
		public ActionResult UpdateOrderState(int OrderId, EnumOrderState OrderState)
		{
			var order = db.Orders.Where(i => i.Id == OrderId).FirstOrDefault();
			if (order != null) 
			{ 
				order.OrderState= OrderState;
				db.SaveChanges();
				TempData["message"] = "Bilgileriniz Kayıt Edildi"; //Kullanıcıya mesaj gönderiyoruyz işlem gerçekleştikten sonra
				return RedirectToAction("Details", new { id = OrderId });
			}
			return RedirectToAction("Index");
		}
	}
}