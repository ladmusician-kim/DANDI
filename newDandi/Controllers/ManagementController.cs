using newDandi.Models.db;
using newDandi.Service;
using newDandi.Views.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace newDandi.Controllers
{
    public class ManagementController : Controller
    {

        dandiEntities db = new dandiEntities();
        ManagementService service = new ManagementService();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var userData = ticket.UserData;
                var userId = int.Parse(userData);
                var user = db.Users.FirstOrDefault(x => x.C_userid == userId);

                if (user != null)
                {
                    if (user.email.Equals("admin@gmail.com"))
                    {
                        return RedirectToAction("User");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Project");
                    }
                }

                return RedirectToAction("Index", "Project");
            }

            return RedirectToAction("Index", "Home");
        }

        #region Market2
        // RequestSeller
        public ActionResult DandiMarket2Seller()
        {
            if (isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }

        public ActionResult DandiMarket2SellerDetail(int id)
        {
            if (isAdmin())
            {
                var seller = service.GetRequestSellerById(id);

                if (seller.Id != 0)
                {
                    if (seller.Picture != null && seller.Picture.Length > 0)
                    {
                        seller.PictureFile = File(seller.Picture, "image");
                    }
                    var viewModel = new DandiMarket2SellerDetailViewModel
                    {
                        Seller = seller
                    };

                    return View(viewModel);
                }

                return RedirectToAction("DandiMarket2Seller", "Management");
            }

            return RedirectToAction("Index", "Project");
        }

        public FileResult GetSellerImage(int id)
        {
            var image = service.GetSellerImage(id);

            if (image != null)
            {
                return File(image.Content, "image/png");
            }
            else
            {
                return File(new byte[0], "image/png");
            }
        }

        public ActionResult SelectSeller (int id, bool isChecked)
        {
            var rtv = service.SelectSeller(id, isChecked);

            return RedirectToAction("DandiMarket2SellerDetail", new { id = id });
        }

        // Picture
        public ActionResult DandiMarket2Picture()
        {
            if(isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }
        public ActionResult AddPicture(int projectId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DandiMarket2AddPicture(string title, HttpPostedFileBase file, int projectId)
        {
            var daypic = new Daypic
            {
                title = title,
                created = DateTime.Now,
                updated = null,
                for_projectid = projectId,
                isdeprecated = false,
            };

            var memoryStream = new MemoryStream();
            try
            {
                file.InputStream.CopyTo(memoryStream);
                daypic.pic = memoryStream.ToArray();
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    place = "DandiMarket2AddPitcture-memorystream",
                    contents = e.ToString(),
                    for_projectid = projectId,
                    created = DateTime.Now,
                    isdeplecated = false
                };
                db.Errors.Add(error);
                db.SaveChanges();
            }

            try
            {
                db.Daypics.Add(daypic);
                db.SaveChanges();

                return RedirectToAction("DandiMarket2Picture");
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    place = "DandiMarket2AddPitcture-add",
                    contents = e.ToString(),
                    for_projectid = projectId,
                    created = DateTime.Now,
                    isdeplecated = false
                };
                db.Errors.Add(error);
                db.SaveChanges();
            }

            return RedirectToAction("DandiMarket2AddPicture");
        }

        //  Oneday 
        public ActionResult DandiMarket2Oneday()
        {
            if (isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }
        public ActionResult DandiMarket2OnedayDetail(int id)
        {
            if (isAdmin())
            {
                var oneday = service.GetOnedayClassById(id);

                if (oneday.Id != 0)
                {
                    var viewModel = new DandiMarket2OnedayDetailViewModel
                    {
                        Oneday = oneday
                    };

                    return View(viewModel);
                }

                return RedirectToAction("DandiMarket2Oneday", "Management");
            }

            return RedirectToAction("Index", "Project");
        }
        public ActionResult SelectOneday(int id, bool isChecked)
        {
            var rtv = service.SelectOneday(id, isChecked);

            return RedirectToAction("DandiMarket2OnedayDetail", new { id = id });
        }
        #endregion





        // User
        public ActionResult User()
        {
            if (isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }

        // Project
        public ActionResult Project()
        {
            if (isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }
        public ActionResult AboutProject(int? id)
        {
            if (isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }
        public ActionResult AddProject()
        {
            if (isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }
        [HttpPost]
        public ActionResult AboutProject(string title, string content, DateTime start, DateTime end, int? id)
        {
            var project = new Project
            {
                title = title,
                content = content,
                startdate = start,
                enddate = end,
                created = DateTime.Now,
                updated = null,
                isdeprecated = false
            };

            try
            {
                db.Projects.Add(project);
                db.SaveChanges();

                return RedirectToAction("Project");
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    place = "프로젝트 추가",
                    contents = e.ToString(),
                    created = DateTime.Now,
                    isdeplecated = false,
                };
                db.Errors.Add(error);
                db.SaveChanges();

                return View();
            }
        }
        [HttpPost]
        public ActionResult AddProject(string title, string content)
        {
            var project = new Project
            {
                title = title,
                content = content,
                startdate = DateTime.Now,
                enddate = DateTime.Now,
                created = DateTime.Now,
                updated = null,
                isdeprecated = false
            };

            try
            {
                db.Projects.Add(project);
                db.SaveChanges();

                return RedirectToAction("Project");
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    place = "프로젝트 추가",
                    contents = e.ToString(),
                    created = DateTime.Now,
                    isdeplecated = false,
                };
                db.Errors.Add(error);
                db.SaveChanges();

                return View();
            }
            return View();
        }
        



        // Category
        public ActionResult ProductCategory()
        {
            if (isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }
        // category 추가
        public ActionResult AddProductCategory()
        {
            if (isAdmin())
            {
                return View();
            }

            return RedirectToAction("Index", "Project");
        }
        [HttpPost]
        public ActionResult AddProductCategory(string label)
        {
            if (isAdmin())
            {
                var item = new ProductCategory
                {
                    label = label,
                    isdeprecated = false
                };

                try
                {
                    db.ProductCategories.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                }

                return RedirectToAction("ProductCategory");
            }

            return RedirectToAction("Index", "Project");
        }
        // category 상세보기
        public ActionResult DetailProductCategory(int? id)
        {
            if(isAdmin())
            {
                var viewModel = new CategoryViewModel();
                if (id != null)
                {
                    var category = db.ProductCategories.FirstOrDefault(x => x.C_id == id.Value);
                    if(category != null)
                    {
                        viewModel.Id = category.C_id;
                        viewModel.Label = category.label;
                        viewModel.IsDeprecated = category.isdeprecated;
                    }
                }

                return View(viewModel);
            }

            return RedirectToAction("Index", "Project");
        }
        // category 수정
        public ActionResult EditProductCategory(int id, string label)
        {
            if (isAdmin())
            {
                var item = db.ProductCategories.FirstOrDefault(x => x.C_id == id);
                if (item != null)
                {
                    item.label = label;

                    db.SaveChanges();
                }

                return RedirectToAction("ProductCategory");
            }

            return RedirectToAction("Index", "Project");
        }
        // category 삭제, 사용
        public ActionResult DeleteAliveProductCategory(int id, bool isDeprecated)
        {
            if (isAdmin())
            {
                var item = db.ProductCategories.FirstOrDefault(x => x.C_id == id);
                if (item != null)
                {
                    item.isdeprecated = isDeprecated;

                    db.SaveChanges();
                }

                return RedirectToAction("ProductCategory");
            }

            return RedirectToAction("Index", "Project");
        }









        public JsonResult GetProjects(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var rtv = service.GetProjects(sorting, filter, page, count);

            return Json(rtv, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductCategories(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var rtv = service.GetProductCategories(sorting, filter, page, count);

            return Json(rtv, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOnedayClass(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var rtv = service.GetOnedayClass(sorting, filter, page, count);

            return Json(rtv, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var rtv = service.GetUser(sorting, filter, page, count);

            return Json(rtv, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMarket2Picture(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var rtv = service.GetMarket2Picture(sorting, filter, page, count);

            return Json(rtv, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMarket2RequestSeller(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var rtv = service.GetRequestSeller(sorting, filter, page, count);

            return Json(rtv, JsonRequestBehavior.AllowGet);
        }

        public bool isAdmin()
        {
            if (Request.IsAuthenticated)
            {
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var userData = ticket.UserData;
                var userId = int.Parse(userData);
                var user = db.Users.FirstOrDefault(x => x.C_userid == userId);

                if (user != null)
                {
                    if (user.email.Equals("admin@gmail.com"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }

            return false;
        }
    }
}
