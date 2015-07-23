using newDandi.Models.db;
using newDandi.Models.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace newDandi.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string userData = ticket.UserData;
                var userId = int.Parse(userData);

                var viewModel = new CommonViewModel();
                using (var db = new dandiEntities())
                {
                    var user = db.Users.FirstOrDefault(x => x.C_userid == userId);
                    if (user != null)
                    {
                        user.logined = DateTime.Now;
                        db.SaveChanges();
                    }

                    var requestSeller = db.RequestSellers.FirstOrDefault(x => x.User.C_userid == userId && x.for_projectid == 1);
                    if (requestSeller != null)
                    {
                        viewModel.isRequestSeller = true;
                        viewModel.RequestId = requestSeller.C_id;
                    }
                    else
                    {
                        viewModel.isRequestSeller = false;
                    }
                }

                return View(viewModel);
            }
            else
            {
                string strUserAgent = Request.UserAgent.ToString().ToLower();
                if (Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                    strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                    strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                    strUserAgent.Contains("palm"))
                {
                    return RedirectToAction("Mobile", "Home");

                }

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
