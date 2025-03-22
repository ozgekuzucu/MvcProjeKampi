using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class IstatistikController : Controller
    {
		// GET: Istatistik
		private readonly Context _context = new Context();
		public ActionResult Index()
        {
			// 1) Toplam kategori sayısı
			int toplamKategori = _context.Categories.Count();

			// 2) Başlık tablosunda "Yazılım" kategorisine ait başlık sayısı
			int yazilimBaslikSayisi = _context.Headings.Count(h => h.Category.CategoryName == "Yazılım");

			// 3) Yazar adında 'a' harfi geçen yazar sayısı
			int aHarfliYazarSayisi = _context.Writers.Count(w => w.WriterName.Contains("a") || w.WriterName.Contains("A"));

			// 4) En fazla başlığa sahip kategori adı
			string enFazlaBaslikKategori = _context.Categories
				.OrderByDescending(c => c.Headings.Count())
				.Select(c => c.CategoryName)
				.FirstOrDefault();

			// 5) Durumu true olan ve false olan kategoriler arasındaki fark
			int aktifKategori = _context.Categories.Count(c => c.CategoryStatus == true);
			int pasifKategori = _context.Categories.Count(c => c.CategoryStatus == false);
			int kategoriFarki = aktifKategori - pasifKategori;

			// ViewBag ile verileri View'e göndermek
			ViewBag.ToplamKategori = toplamKategori;
			ViewBag.YazilimBaslikSayisi = yazilimBaslikSayisi;
			ViewBag.AHarfliYazarSayisi = aHarfliYazarSayisi;
			ViewBag.EnFazlaBaslikKategori = enFazlaBaslikKategori;
			ViewBag.KategoriFarki = kategoriFarki;

			return View();
		}
    }
}