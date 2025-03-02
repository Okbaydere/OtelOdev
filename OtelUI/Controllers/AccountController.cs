using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Business.Concreate;
using Entities.Concreate;
using OtelUI.Models;

namespace OtelUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly EmployeeManager _employeeManager;

        public AccountController()
        {
            _employeeManager = new EmployeeManager(new Data.EntityFramwork.EfEmployeesDal());
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            // Eğer kullanıcı zaten giriş yapmışsa, ana sayfaya yönlendir
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kullanıcı adı ve şifre kontrolü
                    var employees = _employeeManager.Employeesliste();
                    
                    if (employees == null || !employees.Any())
                    {
                        ModelState.AddModelError("", "Çalışan listesi alınamadı. Lütfen sistem yöneticisiyle iletişime geçin.");
                        return View(model);
                    }
                    
                    var employee = employees.FirstOrDefault(e => 
                        e.EmployeeUserName == model.Username && 
                        e.EmployeePassword == model.Password &&
                        e.IsActive);

                    if (employee != null)
                    {
                        // Oturum bilgilerini ayarla
                        FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                        
                        // Kullanıcı bilgilerini Session'a kaydet
                        Session["EmployeeId"] = employee.EmployeeId;
                        Session["EmployeeName"] = employee.EmployeeFirstName + " " + employee.EmployeeLastName;
                        Session["EmployeeTask"] = employee.EmployeeTask;
                        
                        // Eğer Admin ise Admin paneline yönlendir
                        if (employee.EmployeeTask == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        
                        // Dönüş URL'i varsa oraya, yoksa ana sayfaya yönlendir
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Giriş sırasında bir hata oluştu: " + ex.Message);
                }
            }

            // Eğer buraya kadar geldiyse, giriş başarısız olmuştur
            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            // Oturumu sonlandır
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            // Ana sayfaya yönlendir
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/AccessDenied
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
