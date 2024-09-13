using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrunKatalog.MvcWebUI.Models
{
	public class Register
	{
        [Required]//kullanıcı buraya mutlaka isim girmeli alternatifi yok
        [DisplayName("Adınız")]
        public string Name { get; set; }

		[Required]
		[DisplayName("Soyadınız")]
		public string Surname { get; set; }

		[Required]
		[DisplayName("Kullanıcı Adı")]
		public string Username { get; set; }

		[Required]
		[DisplayName("Eposta")]
		[EmailAddress(ErrorMessage = "Eposta adresinizi düzgün giriniz. ")]
		public string Email { get; set; }

		[Required]
		[DisplayName("Şifre")]
		public string Password { get; set; }

		[Required]
		[DisplayName("Şifre Tekrar")]
		[Compare("Password",ErrorMessage ="Şifreniz uyuşmuyor.")]
		public string Repassword { get; set; }
    }
}