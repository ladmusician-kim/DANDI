using newDandi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace newDandi.Controllers
{
    public class AccountController : Controller
    {
        dandiEntities db = new dandiEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(p => p.email.Equals(email));

                if (user != null)
                {
                    if (user.password.Equals(password))
                    {
                        //FormsAuthentication.SetAuthCookie(user.C_userid.ToString(), false);

                        var ticket = new FormsAuthenticationTicket(1, "userId", DateTime.Now, DateTime.Now.AddYears(1), true, user.C_userid.ToString());

                        // Encrypt the ticket.
                        var encTicket = FormsAuthentication.Encrypt(ticket);

                        // Create the cookie.
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));



                        //FormsAuthentication.SetAuthCookie(user.C_userid.ToString(), false);

                        //Response.Cookies["userId"].Value = user.C_userid.ToString();
                        //Response.Cookies["domain"].Expires = DateTime.Now.AddDays(1);

                        if (user.email.Equals("admin@gmail.com"))
                        {
                            return RedirectToAction("User", "Management");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Project");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
