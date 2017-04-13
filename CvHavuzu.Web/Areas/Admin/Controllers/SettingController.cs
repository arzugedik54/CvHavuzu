using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CvHavuzu.Web.Models;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private Data.ApplicationDbContext context;
        public SettingController(Data.ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {
            Setting setting = new Setting();
            setting.WelcomeText = "�rnek metin";
            return View(setting);
        }

        [HttpPost]
        public IActionResult Index(Setting setting)
        {
            if (ModelState.IsValid)
            {
                Setting s;
                if (context.Settings.Any())
                {
                    s = context.Settings.FirstOrDefault();
                    s.WelcomeText = setting.WelcomeText;
                    s.MembershipAgreement = setting.MembershipAgreement;
                    s.ResumeDownloadSecurity = setting.ResumeDownloadSecurity;
                    s.LastResumeCreateDate = setting.LastResumeCreateDate;
                    s.FooterText = setting.FooterText;
                    s.CustomHtml = setting.CustomHtml;
                    s.UpdateDate = DateTime.Now;
                    context.SaveChanges();
                } else
                {
                    context.Settings.Add(setting);
                    context.SaveChanges();
                }
                
                ViewBag.Message = "Ayarlar ba�ar�yla kaydedildi.";
            }
            return View(setting);
        }
    }
}