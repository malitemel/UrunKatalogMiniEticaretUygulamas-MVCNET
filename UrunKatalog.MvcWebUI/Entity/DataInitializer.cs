using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrunKatalog.MvcWebUI.Entity
{
	public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
	{
		protected override void Seed(DataContext context)
		{
			List<Category> kategoriler = new List<Category>
			{
				new Category(){Name="Kamera",Description="Kamera Ürünleri"},
				new Category(){Name="Bilgisayar",Description="Bilgisayar Ürünleri"},
				new Category(){Name="Elektronik",Description="Elektronik Ürünleri"},
				new Category(){Name="Telefon",Description="Telefon Ürünleri"},
				new Category(){Name="Beyaz Eşya",Description="Beyaz Eşya Ürünleri"}
			};

            foreach (var kategori in kategoriler)
            {
				context.Categories.Add(kategori);
            }
			context.SaveChanges();
			var urunler = new List<Product>()
			{
				new Product(){Name="Canon Eos 1200D 18-55 mm DC Profesyonel Dijital Fotoğraf Mak.",Description="a",Price=1203,Stock=500,IsApproved=true,CategoryId=1,IsHome=true,Image="1.jpg" },
				new Product(){Name="Canon Eos 100D 18-55 mm DC Profesyonel Dijital Fotoğraf Mak.",Description="a",Price=1230,Stock=50,IsApproved=true,CategoryId=1,IsHome=true,Image="2.jpg" },
				new Product(){Name="Canon Eos 700D 18-55 DCLRS fotoğraf Mak.",Description="a",Price=1203,Stock=500,IsApproved=false,CategoryId=1,IsHome=true,Image="3.jpg" },
				new Product(){Name="Canon Eos 100D 18-55 mm IS STM Kit DSLR Fotoğraf Mak.",Description="a",Price=1023,Stock=500,IsApproved=true,CategoryId=1,IsHome=true,Image="4.jpg" },
				new Product(){Name="Canon Eos 700D + 18-55 mm Is Stm + Çanta + 16 Gb hafıza Kartı.",Description="a",Price=1230,Stock=250,IsApproved=false,CategoryId=1,Image="5.jpg" },

				new Product(){Name="Dell Inspiron 5567 Intel Core i5 7200U 8GB 1TB R7 M445 Windows",Description="b",Price=4500,Stock=1200,IsApproved=true,CategoryId=2,IsHome=true,Image="1.jpg" },
				new Product(){Name="Lenovo Ideapad 310 Intel Core i7 7500U 12GB 1TB GT920M Windows",Description="c",Price=3400,Stock=0,IsApproved=false,CategoryId=2,IsHome=true,Image="3.jpg" },
				new Product(){Name="Asus UX310UQ-FB418T Intel Core i7 7500U 8GB 512GB SSD GT940MX",Description="d",Price=2500,Stock=600,IsApproved=true,CategoryId=2,Image="2.jpg" },
				new Product(){Name="Asus UX310UQ-FB418T Intel Core i7 7500U 16GB 512GB SSD GT940MX",Description="e",Price=4000,Stock=505,IsApproved=true,CategoryId=2,Image="4.jpg" },
				new Product(){Name="Asus N%(=VD-DM160T Intel Core i7 7700HQ 16GB 1 TB + 128GB SSD",Description="f",Price=5200,Stock=500,IsApproved=true,CategoryId=2,Image="5.jpg" },

				new Product(){Name="LG 49UH610V 49 124 Ekran 4K Uydu Alıcısı Smnart LED TV",Description="b",Price=2400,Stock=1200,IsApproved=true,CategoryId=3,Image="1.jpg" },
				new Product(){Name="Vestel 40UB8300 49 124 Ekran 4K Smart Led Tv",Description="c",Price=3400,Stock=0,IsApproved=false,CategoryId=3,IsHome=true,Image="4.jpg" },
				new Product(){Name="Samsung 55KU7500 55 140 Ekran 4K Uydu Alıcılı Smart Tv",Description="d",Price=2700,Stock=500,IsApproved=true,CategoryId=3,IsHome=true,Image="2.jpg" },
				new Product(){Name="LG 55UH615V 55 140 Ekran 4K Uydu Alıcılı Smart Wi-fi",Description="e",Price=4000,Stock=500,IsApproved=true,CategoryId=3,Image="3.jpg" },
				new Product(){Name="Sony Kd-55Xd7005B 55 140 Ekran 4K Uydu Alıcılı Smart LED TV",Description="f",Price=5000,Stock=250,IsApproved=true,CategoryId=3,Image="5.jpg" },

				new Product(){Name="Apple iPhone 6 32GB",Price=5600,Stock=1200,IsApproved=true,CategoryId=4,IsHome=true,Image="1.jpg" },
				new Product(){Name="Apple iPhone 7 32GB",Price=3400,Stock=0,IsApproved=false,CategoryId=4,IsHome=true,Image="2.jpg" },
				new Product(){Name="Apple iPhone 6S 32GB",Price=2500,Stock=500,IsApproved=true,CategoryId=4,Image="3.jpg" },
				new Product(){Name="Case 4U Manyetik Mıknatıslı Araç İçi Telefon Tutucu",Price=4000,Stock=500,IsApproved=true,CategoryId=4,Image="4.jpg" },
				new Product(){Name="Xiamoi Mi 5S 64GB",Description="Kullanıma hazır",Price=50,Stock=25000,IsApproved=true,CategoryId=4,Image="5.jpg" }

			};
			foreach (var urun in urunler)
			{
				context.Products.Add(urun);
			}
			context.SaveChanges();
            base.Seed(context);
		}
	}
}