﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunKatalog.MvcWebUI.Entity;
using UrunKatalog.MvcWebUI.Models;

namespace UrunKatalog.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
		private DataContext db = new DataContext();
		// GET: Cart
		public ActionResult Index()
        {
            return View(GetCart());
        }
		public ActionResult AddToCart(int Id)
		{
            var product = db.Products.Where(i => i.Id == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().AddProduct(product,1);
            }
			return RedirectToAction("Index");
		}
		public ActionResult RemoveFromCart(int Id)
		{
			var product = db.Products.Where(i => i.Id == Id).FirstOrDefault();
			if (product != null)
			{
				GetCart().DeleteProduct(product);
			}
			return RedirectToAction("Index");
		}
		public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"]; //her kullanıcıya özel oluşturulan depo(kart) *Session* denir.
            if (cart == null) 
            {
                cart= new Cart();
                Session["Cart"] = cart;

            }
            return cart;
        }
        public PartialViewResult Summary() 
        { 
            return PartialView(GetCart());
        }
        [Authorize]
        public ActionResult Checkout() 
        { 
            return View(new ShippingDetails());
        }
        [HttpPost]
		public ActionResult Checkout(ShippingDetails entity)
		{
            var cart = GetCart();

            if(cart.Cartlines.Count==0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır.");
            }
            if(ModelState.IsValid)
            {
                //siparişi vertabınına kaydet
                //cartı sıfırla
                SaveOrder(cart, entity);
                cart.Clear();
                return View("Completed");
            }
            else
            {
				return View(entity);
			}
            
		}

		private void SaveOrder(Cart cart, ShippingDetails entity)
		{
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            order.Username = User.Identity.Name;
            order.AdresBasligi = entity.AdresBasligi;
            order.Adres = entity.Adres;
            order.Sehir = entity.Sehir;
            order.Semt = entity.Semt;
            order.Mahalle = entity.Mahalle;
            order.PostaKodu = entity.PostaKodu;

            order.OrderLines = new List<OrderLine>();
            foreach(var pr in cart.Cartlines)
            {
                var orderline=new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price = pr.Quantity * pr.Product.Price;
                orderline.ProductId = pr.Product.Id;

                order.OrderLines.Add(orderline);//orderline a ekleme
            }
            db.Orders.Add(order);
            db.SaveChanges();
		}
	}
}