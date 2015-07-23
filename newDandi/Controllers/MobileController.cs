using newDandi.Models.db;
using newDandi.Views.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace newDandi.Controllers
{
    public class MobileController : Controller
    {
        //
        // GET: /Mobile/

        public ActionResult Login()
        {
            string strUserAgent = Request.UserAgent.ToString().ToLower();
            if (Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                strUserAgent.Contains("palm"))
            {
                var passedData = (ResultViewModel)TempData["data"];
                var viewModel = new ResultViewModel();

                if (passedData != null)
                {
                    viewModel.Error = passedData.Error;
                };

                return View(viewModel);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var viewModel = new ResultViewModel();

            if (ModelState.IsValid)
            {
                using (var db = new dandiEntities())
                {
                    var user = db.Users.FirstOrDefault(p => p.email.Equals(email));

                    if (user != null)
                    {
                        if (user.password.Equals(password))
                        {
                            var ticket = new FormsAuthenticationTicket(1, "userId", DateTime.Now, DateTime.Now.AddYears(1), true, user.C_userid.ToString());

                            var encTicket = FormsAuthentication.Encrypt(ticket);
                            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

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
                            viewModel.Error = "비밀번호가 일치하지 않습니다!";
                            viewModel.Email = email;
                            TempData["data"] = viewModel;
                            return View();
                        }
                    }
                    else
                    {
                        viewModel.Error = "존재하지 않은 이메일 입니다!";
                        viewModel.Email = email;
                        TempData["data"] = viewModel;
                        return View(viewModel);
                    }
                }
            
            }
            else
            {
                viewModel.Error = "이메일과 비밀번호를 제대로 입력해주세요!";
                viewModel.Email = email;
                TempData["data"] = viewModel;
                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Sign()
        {
            string strUserAgent = Request.UserAgent.ToString().ToLower();
            if (Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                strUserAgent.Contains("palm"))
            {
                var passedData = (ResultViewModel)TempData["data"];
                var viewModel = new ResultViewModel();

                if (passedData != null)
                {
                    viewModel.Error = passedData.Error;
                };

                return View(viewModel);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

         [HttpPost]
        public ActionResult Sign(string email, string password, string confirmPassword)
        {
            var viewModel = new ResultViewModel();

            if (ModelState.IsValid)
            {
                using (var db = new dandiEntities())
                {
                    var user = db.Users.FirstOrDefault(p => p.email.Equals(email));

                    if (user != null)
                    {
                        viewModel.Error = "이미 존재하는 이메일 입니다!";
                        viewModel.Email = email;
                        TempData["data"] = viewModel;
                        return View(viewModel);
                    }
                    else
                    {
                        if (password.Equals(confirmPassword))
                        {
                            try
                            {
                                var db_user = new User
                                {
                                    username = "dandi",
                                    password = password,
                                    email = email,
                                    created = DateTime.Now,
                                    logined = DateTime.Now,
                                    isdeprecated = false
                                };

                                db.Users.Add(db_user);
                                db.SaveChanges();

                                return RedirectToAction("Login");
                            }
                            catch (Exception e)
                            {
                                viewModel.Error = "회원가입하는데 오류가 발생했습니다!";
                                viewModel.Email = email;
                                TempData["data"] = viewModel;
                                return View(viewModel);
                            }
                        }
                        else
                        {
                            viewModel.Error = "비밀번호가 일치하지 않습니다!";
                            viewModel.Email = email;
                            TempData["data"] = viewModel;
                            return View(viewModel);
                        }
                    }
                }
            }
            else
            {
                viewModel.Error = "이메일과 비밀번호를 제대로 입력해주세요!";
                viewModel.Email = email;
                TempData["data"] = viewModel;
                return View(viewModel);
            }
        }
    }
}
