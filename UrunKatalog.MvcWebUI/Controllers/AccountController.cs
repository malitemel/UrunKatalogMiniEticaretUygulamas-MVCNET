﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunKatalog.MvcWebUI.Entity;
using UrunKatalog.MvcWebUI.Identity;
using UrunKatalog.MvcWebUI.Models;

namespace UrunKatalog.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
		private DataContext db = new DataContext();
		private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager= new RoleManager<ApplicationRole>(roleStore);
        }
        [Authorize]//Authorize işaretlememizin sebebi login olmadan ulaşılamaz, login olduktan sonra ulaşabilir
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders = db.Orders
                .Where(i => i.Username == username)
                .Select(i => new UserOrderModel()
                {
                    Id = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Total = i.Total
                }).OrderByDescending(i => i.OrderDate).ToList();
            return View(orders); 
        }
		[Authorize]
		public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(i => i.Id == id)
                                   .Select(i => new OrderDetailsModel()
                                   {
                                       OrderId = i.Id,
                                       OrderNumber= i.OrderNumber,
                                       Total = i.Total,
                                       OrderDate= i.OrderDate,
                                       OrderState = i.OrderState,
                                       AdresBasligi = i.AdresBasligi,
                                       Adres= i.Adres,
                                       Sehir = i.Sehir,
                                       Semt = i.Semt,
                                       Mahalle = i.Mahalle,
                                       PostaKodu = i.PostaKodu,
                                       OrderLines=i.OrderLines.Select(a=> new OrderLineModel()
                                       {
                                           ProductId = a.ProductId,
                                           ProductName=a.Product.Name.Length>50?a.Product.Name.Substring(0,47)+"...":a.Product.Name,//ürün 50 karakterden büyükse sonuna .. koy değilse direkt hepsini yaz koşulunu ekledik, nameyi alırken
                                           Image=a.Product.Image,
                                           Quantity=a.Quantity,
                                           Price=a.Price
                                       }).ToList()
                                   }).FirstOrDefault();
            return View(entity);
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Register(Register model)
		{
            if (ModelState.IsValid) 
            {
                //Kayıt işlemleri
                ApplicationUser user = new ApplicationUser();
                user.Name=model.Name;
                user.Surname=model.Surname;
                user.Email=model.Email;
                user.UserName = model.Username;

                IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded) 
                {
                    //kullanıcı oluştu ve kullanıcıyı bi role atayabilirsiniz.
                    if(RoleManager.RoleExists("User"))
                    {
						UserManager.AddToRole(user.Id, "User");
					}
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError","Kullanıcı oluşturma hatası.");
                }

            }
			return View(model);
		}

		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(Login model, string ReturnUrl)
		{
			if (ModelState.IsValid)
			{
                //Giriş işlemleri
                var user = UserManager.Find(model.Username, model.Password);
                if (user != null) 
                {
                    //varolan kullanıcıyı sisteme dahil et!
                    //Application Cookie oluşturup sisteme bırak.
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var auhtProperties = new AuthenticationProperties();
                    (new AuthenticationProperties()).IsPersistent = model.RememberMe;
                    authManager.SignIn(auhtProperties, identityclaims);
                    if(String.IsNullOrEmpty(ReturnUrl))
                    {
                        Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
				else
				{
					ModelState.AddModelError("LoginUserError", "Kullanıcı giriş hatası.");
				}

			}
			return View(model);
		}

		public ActionResult Logout()
		{

			var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

			return RedirectToAction("Index","Home");
		}
	}
}