using newDandi.Models.db;
using newDandi.Models.VIEWMODEL;
using newDandi.Views.DandiMarket2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace newDandi.Controllers
{
    public class DandiMarket2Controller : Controller
    {
        dandiEntities db = new dandiEntities();

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
                    var requestSeller = db.RequestSellers.FirstOrDefault(x => x.User.C_userid == userId);
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

        [Authorize]
        public ActionResult RequestSeller(int? id)
        {
            var passedData = (RequestViewModel)TempData["data"];

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            var viewModel = new RequestViewModel();
            using (var db = new dandiEntities())
            {
                string userData = ticket.UserData;
                try
                {
                    var userId = int.Parse(userData);
                    var user = db.Users.FirstOrDefault(x => x.C_userid == userId);

                    viewModel.User = new Models.DTO.UserDTO();
                    viewModel.User.Id = user.C_userid;
                    viewModel.User.Username = user.username;
                    viewModel.User.Email = user.email;

                    if (id != null)
                    {
                        var requestSeller = db.RequestSellers.FirstOrDefault(x => x.C_id == id);
                        if (requestSeller != null)
                        {
                            viewModel.Id = requestSeller.C_id;
                            viewModel.Name = requestSeller.name;
                            viewModel.Phone = requestSeller.phone;
                            viewModel.AboutCulture = requestSeller.aboutculture;
                            viewModel.Info = requestSeller.info;
                            viewModel.Product = requestSeller.product;
                            viewModel.Email = requestSeller.email;
                            viewModel.isEdit = true;
                        }
                    }

                    if (passedData != null)
                    {
                        viewModel.Name = passedData.Name;
                        viewModel.Phone = passedData.Phone;
                        viewModel.AboutCulture = passedData.AboutCulture;
                        viewModel.Info = passedData.Info;
                        viewModel.Product = passedData.Product;
                        viewModel.Email = passedData.Email;
                    }
                    else 
                    {
                        viewModel.Email = viewModel.User.Email;
                    }

                    return View(viewModel);
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index", "Project");
                }
            }
        }

        [Authorize]
        public ActionResult SuccessRequest(int id)
        {
            var viewModel = new CommonViewModel
            {
                isRequestSeller = true,
                RequestId = id
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Band()
        {
            return View();
        }
        [Authorize]
        public ActionResult OnedayClass()
        {
            var passedData = (RequestViewModel)TempData["data"];

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            var viewModel = new RequestViewModel();
            using (var db = new dandiEntities())
            {
                string userData = ticket.UserData;
                try
                {
                    var userId = int.Parse(userData);
                    var user = db.Users.FirstOrDefault(x => x.C_userid == userId);

                    viewModel.User = new Models.DTO.UserDTO();
                    viewModel.User.Id = user.C_userid;
                    viewModel.User.Username = user.username;
                    viewModel.User.Email = user.email;
                }
                catch (Exception e)
                {
                }
            }

            if (passedData != null)
            {
                viewModel.Name = passedData.Name;
                viewModel.Phone = passedData.Phone;
                viewModel.AboutCulture = passedData.AboutCulture;
                viewModel.Info = passedData.Info;
            }

            return View(viewModel);
        }






        [HttpPost]
        public ActionResult SubmitRequestSeller(string email, string name, string phone, string product, string info, string aboutCulture, HttpPostedFileBase file)
        {
            var viewModel = new RequestViewModel()
            {
                Email = email,
                Name = name,
                Phone = phone,
                Info = info,
                Product = product,
                AboutCulture = aboutCulture,
            };

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string userData = ticket.UserData;
            var userId = int.Parse(userData);

            // 신청했는지 체크
            var isSubmitted = db.RequestSellers.FirstOrDefault(x => x.for_userid == userId);
            if (isSubmitted == null)
            {
                var requestSeller = new RequestSeller
                {
                    email = email,
                    name = name,
                    phone = phone,
                    info = info,
                    aboutculture = aboutCulture,
                    ischecked = false,
                    for_userid = userId,
                    product = product,
                    submitted = DateTime.Now,
                    //pic = memoryStream.ToArray()
                };

                if (file != null)
                {
                    var memoryStream = new MemoryStream();
                    try
                    {
                        file.InputStream.CopyTo(memoryStream);
                        requestSeller.pic = memoryStream.ToArray();
                    }
                    catch (Exception e)
                    {
                        var error = new Error
                        {
                            place = "셀러신청 memorystream",
                            contents = e.ToString(),
                            created = DateTime.Now,
                            for_userid = userId,
                            isdeplecated = false,
                        };
                        db.Errors.Add(error);
                        db.SaveChanges();

                        viewModel.Error = "사진을 저장하는데 오류가 발생했습니다!";
                        TempData["data"] = viewModel;   
                        return RedirectToAction("RequestSeller");
                    }
                }

                try
                {
                    db.RequestSellers.Add(requestSeller);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Exception test = e;

                    var error = new Error
                    {
                        place = "셀러신청 add",
                        contents = e.ToString(),
                        created = DateTime.Now,
                        for_userid = userId,
                        isdeplecated = false,
                    };
                    db.Errors.Add(error);
                    db.SaveChanges();

                    viewModel.Error = "셀러신청서를 저장하는데 오류가 발생했습니다!";
                    TempData["data"] = viewModel;

                    return RedirectToAction("RequestSeller");
                }


                return RedirectToAction("SuccessRequest", new { id = requestSeller.C_id });
            }
            else
            {
                var error = new Error
                {
                    place = "셀러신청 중복신청",
                    contents = "이미 신청한 사람",
                    created = DateTime.Now,
                    for_userid = userId,
                    isdeplecated = false,
                };
                db.Errors.Add(error);
                db.SaveChanges();

                viewModel.Error = "이미 셀러신청을 하셨습니다!!";
                TempData["data"] = viewModel;

                return RedirectToAction("RequestSeller");
            }
        }
        [HttpPost]
        public ActionResult EditRequestSeller(int id, string email, string name, string phone, string product, string info, string aboutCulture, HttpPostedFileBase file)
        {
            var viewModel = new RequestViewModel()
            {
                Email = email,
                Name = name,
                Phone = phone,
                Info = info,
                Product = product,
                AboutCulture = aboutCulture,
            };
            TempData["data"] = viewModel;

            var requestSeller = db.RequestSellers.FirstOrDefault(x => x.C_id == id);
            if (requestSeller != null)
            {
                requestSeller.name = name;
                requestSeller.phone = phone;
                requestSeller.product = product;
                requestSeller.info = info;
                requestSeller.aboutculture = aboutCulture;
                requestSeller.email = email;
                requestSeller.updated = DateTime.Now;

                var memoryStream = new MemoryStream();
                try
                {
                    file.InputStream.CopyTo(memoryStream);
                    requestSeller.pic = memoryStream.ToArray();
                }
                catch (Exception e)
                {
                }

                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index", "Project");
                }
                catch (Exception e)
                {
                }

                return RedirectToAction("RequestSeller");
            }
            else
            {
                return RedirectToAction("RequestSeller");
            }
        }
        [HttpPost]
        public ActionResult SubmitOnedayClass(string name, string email, string phone)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string userData = ticket.UserData;
            var userId = int.Parse(userData);

            var onedayClass = new OnedayClass
            {
                name = name,
                phone = phone,
                for_userid = userId,
                created = DateTime.Now,
                checkeddate = null
            };

            try
            {
                db.OnedayClasses.Add(onedayClass);
                db.SaveChanges();

                return RedirectToAction("Index", "DandiMarket2");
            }
            catch(Exception e)
            {
                return RedirectToAction("OnedayClass", "DandiMarket2");
            }
        }
    }
}
