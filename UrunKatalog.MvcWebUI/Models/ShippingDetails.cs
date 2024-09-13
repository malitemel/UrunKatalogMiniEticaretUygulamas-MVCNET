using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrunKatalog.MvcWebUI.Models
{
	public class ShippingDetails
	{
		public string Username { get; set; }
		[Required(ErrorMessage ="Lütfen adres tanımını giriniz")]//boş geçilemez
        public string AdresBasligi { get; set; }

		[Required(ErrorMessage = "Lütfen bir Adres giriniz")]
		public string Adres { get; set; }
		[Required(ErrorMessage = "Lütfen Şehir giriniz")]
		public string Sehir { get; set; }
		[Required(ErrorMessage = "Lütfen Semt giriniz")]
		public string Semt { get; set; }
		[Required(ErrorMessage = "Lütfen Mahalle giriniz")]
		public string Mahalle { get; set; }
		[Required(ErrorMessage = "Lütfen Posta Kodu giriniz")]
		public string PostaKodu { get; set; }

    }
}