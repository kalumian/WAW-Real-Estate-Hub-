﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WAW.Data;
using WAW.Models;

namespace WAW.Controllers
{
    public class AdvertiserController : Controller
    {
        private readonly AppDbContext _context;

        public AdvertiserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult MyAds(string search = "")
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // جلب المعلن الحالي
            var advertiser = _context.Advertisers
                .Include(a => a.Ads)
                .FirstOrDefault(a => a.UserId == userId);

            if (advertiser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // الإعلانات
            var ads = advertiser.Ads.AsQueryable();

            // تطبيق البحث
            if (!string.IsNullOrEmpty(search))
            {
                ads = ads.Where(ad => ad.Title.Contains(search) || ad.Description.Contains(search));
            }

            // الإحصائيات
            ViewBag.TotalAds = advertiser.Ads.Count();
            ViewBag.ActiveAds = advertiser.Ads.Count(ad => ad.Status);
            ViewBag.InactiveAds = advertiser.Ads.Count(ad => !ad.Status);

            return View(ads.ToList());
        }
        public IActionResult Delete(int id)
        {
            var ad = _context.Ads.FirstOrDefault(a => a.AdId == id);
            if (ad != null)
            {
                _context.Ads.Remove(ad);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "İlan başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "İlan bulunamadı.";
            }
            return RedirectToAction("MyAds");
        }
        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // استعلام عن المعلن بناءً على UserId
            var advertiser = _context.Advertisers.FirstOrDefault(a => a.UserId == userId);
            if (advertiser == null)
            {
                TempData["ErrorMessage"] = "Advertiser bulunamadı.";
                return RedirectToAction("Index", "Home");
            }

            // إنشاء نموذج الإعلان مع تحديد AdvertiserId
            Ad ad = new Ad
            {
                AdvertiserId = advertiser.AdvertiserId,
                ImageURL = "g"
            };

            return View(ad);
        }

        [HttpPost]
        public IActionResult Create(Ad ad, IFormFile ImageFile)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // حفظ الصورة إذا تم رفعها
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/images/ads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                ad.ImageURL = $"/images/ads/{fileName}";
            }
            else
            {
                TempData["ErrorMessage"] = "İlan eklenirken bir hata oluştu. Lütfen bilgileri kontrol edin.";
                return View(ad);
            }

            // تعيين القيم الافتراضية
            ad.AdvertiserId = _context.Advertisers.FirstOrDefault(a => a.UserId == userId).AdvertiserId;
            ad.DatePosted = DateTime.Now;
            ad.Status = false; // افتراضيًا نشط

            _context.Ads.Add(ad);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "İlan başarıyla eklendi.";
            return RedirectToAction("MyAds");
         
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // التحقق من أن الإعلان يخص المعلن الحالي
            var ad = _context.Ads.FirstOrDefault(a => a.AdId == id && a.Advertiser.UserId == userId);
            if (ad == null)
            {
                TempData["ErrorMessage"] = "İlan bulunamadı veya bu ilana erişim izniniz yok.";
                return RedirectToAction("MyAds");
            }

            return View(ad);
        }
        [HttpPost]
        public IActionResult Edit(Ad ad, IFormFile ImageFile)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            // استرجاع الإعلان
            var existingAd = _context.Ads.FirstOrDefault(a => a.AdId == ad.AdId && a.Advertiser.UserId == userId);
            if (existingAd == null)
            {
                TempData["ErrorMessage"] = "İlan bulunamadı veya bu ilana erişim izniniz yok.";
                return RedirectToAction("MyAds");
            }

            try
            {
                // تحديث البيانات
                existingAd.Title = ad.Title;
                existingAd.Description = ad.Description;
                existingAd.Address = ad.Address;
                existingAd.Price = ad.Price;
                existingAd.LocationURL = ad.LocationURL;
                ad.Status = false;
                // تحديث الصورة إذا تم رفع واحدة جديدة
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/images/ads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    // تحديث الرابط للصورة
                    existingAd.ImageURL = $"/images/ads/{fileName}";
                }
                else
                {
                    TempData["ErrorMessage"] = "Bilgiler eksik veya hatalı. Lütfen tekrar deneyin.";
                    return View(ad);
                }

                _context.SaveChanges();

                TempData["SuccessMessage"] = "İlan başarıyla güncellendi.";
                return RedirectToAction("MyAds");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                TempData["ErrorMessage"] = "İlan güncellenirken bir hata oluştu.";
                return View(ad);
            }
        }

    }
}
