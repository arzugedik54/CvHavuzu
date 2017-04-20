using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;
using Microsoft.AspNetCore.Hosting;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailSettingController : Controller
    {
        private Data.ApplicationDbContext context;
        private IHostingEnvironment env;
        public MailSettingController(IHostingEnvironment _env, Data.ApplicationDbContext _context)
        {
            this.context = _context;
            this.env = _env;
        }
        public IActionResult Index()
        {
            MailSetting cms;
            cms = context.MailSettings.FirstOrDefault();
            if (cms == null)
            {
                cms = new MailSetting();
                cms.FromAddress="denemecvhavuzu@gmail.com";
                cms.FromAddressPassword="123:Asdfg";
                cms.FromAddressTitle = "Cv Havuzu";
                cms.Subject = "�leti�im"; 
                cms.BodyContent = "Mesaj�n�z Bize �letilmi�tir. �lginiz ��in Te�ekk�r Ederiz";
                cms.SmptServer = "smtp.gmail.com";
                cms.SmptPortNumber = 587;
                context.Add(cms);
                context.SaveChanges();



            }
            return View(cms);
        }

        [HttpPost]
        public IActionResult Index(MailSetting mailSetting)
        {
            if (ModelState.IsValid)
            {
                MailSetting cms;
                if (context.MailSettings.Any())
                {
                    cms = context.MailSettings.FirstOrDefault();
                    cms.FromAddress = mailSetting.FromAddress;
                    cms.FromAddressTitle = mailSetting.FromAddressTitle;
                    cms.FromAddressPassword = mailSetting.FromAddressPassword;
                    cms.BodyContent = mailSetting.BodyContent;
                    cms.SmptPortNumber = mailSetting.SmptPortNumber;
                    cms.SmptServer = mailSetting.SmptServer;
                    cms.Subject = mailSetting.Subject;
                    context.SaveChanges();
                }
            }
            return View(mailSetting);
        }
    }
}