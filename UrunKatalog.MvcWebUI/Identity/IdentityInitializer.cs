using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UrunKatalog.MvcWebUI.Entity;

namespace UrunKatalog.MvcWebUI.Identity
{
	public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
	{
		protected override void Seed(IdentityDataContext context)
		{
			//Rolleri Oluşturalım
			if (!context.Roles.Any(i => i.Name =="admin"))
			{
				var store=new RoleStore<ApplicationRole>(context);
				var manager=new RoleManager<ApplicationRole>(store);
				var role = new ApplicationRole() { Name = "admin", Description = "admin rolü" };
				manager.Create(role);
			}
			if (!context.Roles.Any(i => i.Name == "user"))
			{
				var store = new RoleStore<ApplicationRole>(context);
				var manager = new RoleManager<ApplicationRole>(store);
				var role = new ApplicationRole() { Name = "user", Description = "user rolü" }; 
				manager.Create(role);
			}
			//User Oluşturalım
			if (!context.Roles.Any(i => i.Name == "alitemel"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser() { Name ="ali", Surname="temel",UserName="alitemel",Email="m.alitemel@gmail.com"};

				manager.Create(user,"1234567");
				manager.AddToRole(user.Id, "admin");
				manager.AddToRole(user.Id, "user");
			}
			if (!context.Roles.Any(i => i.Name == "sadikturan"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser() { Name = "sadik", Surname = "turan", UserName = "sadikturan", Email = "sadikturan@gmail.com" };

				manager.Create(user, "1234567");
				manager.AddToRole(user.Id, "user");
			}
			
			base.Seed(context);
		}
	}
}