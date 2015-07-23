using newDandi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace newDandi.Controllers
{
    public class HomeController : Controller
    {
        dandiEntities db = new dandiEntities();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Project");
            }
            else
            {
                string strUserAgent = Request.UserAgent.ToString().ToLower();
                if (Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                    strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                    strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                    strUserAgent.Contains("palm"))
                {
                    return RedirectToAction("Mobile");

                }

                return View();
            }
        }

        public ActionResult Project()
        {
            return View();
        }

        public ActionResult Mobile()
        {
            string strUserAgent = Request.UserAgent.ToString().ToLower();
            if (Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                strUserAgent.Contains("palm"))
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult CheckEmail(string email)
        {
            var check = db.Users.FirstOrDefault(x => x.email.Equals(email));
            if (check != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Sign(string email, string password, string confirmPassword)
        {
            if (password.Equals(confirmPassword))
            {
                try
                {
                    var user = new User
                    {
                        username = "dandi",
                        password = password,
                        email = email,
                        created = DateTime.Now,
                        logined = DateTime.Now,
                        isdeprecated = false
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    return Json(user.C_userid, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    var error = e;
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
