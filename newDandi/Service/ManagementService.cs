using newDandi.Models.db;
using newDandi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newDandi.Service
{
    public class ManagementService
    {
        dandiEntities db = new dandiEntities();


        #region Market2
        public RequestSellerDTO GetRequestSellerById(int id)
        {
            var seller = (from a in db.RequestSellers
                          where a.C_id == id
                          select new RequestSellerDTO
                          {
                              Id = a.C_id,
                              Name = a.name,
                              Phone = a.phone,
                              Email = a.email,
                              Picture = a.pic,
                              Info = a.info,
                              AboutCulture = a.aboutculture,
                              Product = a.product,
                              IsChecked = a.ischecked,
                              Checked = a.checkeddate,
                              Submited = a.submitted
                          }).FirstOrDefault();

            if (seller != null)
            {
                return seller;
            }

            return new RequestSellerDTO();
        }

        public ItemsDTO<RequestSellerDTO> GetRequestSeller(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var perPage = count;

            IQueryable<RequestSeller> items = db.RequestSellers;

                if (sorting != null)
                {
                    var st = sorting.First();
                    switch (st.Key)
                    {
                        case "Id":
                            items = st.Value.Equals("asc") ? items.OrderBy(x => x.C_id) : items.OrderByDescending(x => x.C_id);
                            break;
                        case "Name":
                            items = st.Value.Equals("asc") ? items.OrderBy(x => x.name) : items.OrderByDescending(x => x.name);
                            break;
                        case "Submited":
                            items = st.Value.Equals("asc") ? items.OrderBy(x => x.submitted) : items.OrderByDescending(x => x.submitted);
                            break;
                        case "Updated":
                            items = st.Value.Equals("asc") ? items.OrderBy(x => x.updated) : items.OrderByDescending(x => x.updated);
                            break;
                        case "Checked":
                            items = st.Value.Equals("asc") ? items.OrderBy(x => x.checkeddate) : items.OrderByDescending(x => x.checkeddate);
                            break;
                        case "IsChecked":
                            items = st.Value.Equals("asc") ? items.OrderBy(x => x.ischecked) : items.OrderByDescending(x => x.ischecked);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    items =
                        from p in items
                        orderby p.C_id descending
                        select p;
                }

                IQueryable<RequestSellerDTO> resultItems = (
                    from a in items
                    select new RequestSellerDTO
                    {
                        Id = a.C_id,
                        User = new UserDTO
                        {
                            Id = a.User.C_userid,
                            Username = a.User.username,
                            Email = a.User.email,
                        },
                        Name = a.name,
                        Phone = a.phone,
                        Info = a.info,
                        AboutCulture = a.aboutculture,
                        IsChecked = a.ischecked,
                        Submited = a.submitted,
                        Checked = a.checkeddate,
                        Updated = a.updated
                    });

                if (perPage == -1)
                {
                    var allItems = resultItems.ToList();
                    return new ItemsDTO<RequestSellerDTO>
                    {
                        Items = allItems,
                        PerPage = perPage,
                        RowCount = allItems.Count
                    };
                }
                else
                {
                    var allItems = resultItems.ToList();
                    return new ItemsDTO<RequestSellerDTO>
                    {
                        Items = resultItems.Skip((page - 1) * perPage).Take(perPage).ToList<RequestSellerDTO>(),
                        PerPage = perPage,
                        RowCount = resultItems.Count()
                    };
                }
            }

        public ItemsDTO<RequestSellerDTO> GetMarket3RequestSeller(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var perPage = count;

            IQueryable<RequestSeller> items = db.RequestSellers.Where(x => x.for_projectid == 1);

            if (sorting != null)
            {
                var st = sorting.First();
                switch (st.Key)
                {
                    case "Id":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.C_id) : items.OrderByDescending(x => x.C_id);
                        break;
                    case "Name":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.name) : items.OrderByDescending(x => x.name);
                        break;
                    case "Submited":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.submitted) : items.OrderByDescending(x => x.submitted);
                        break;
                    case "Updated":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.updated) : items.OrderByDescending(x => x.updated);
                        break;
                    case "Checked":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.checkeddate) : items.OrderByDescending(x => x.checkeddate);
                        break;
                    case "IsChecked":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.ischecked) : items.OrderByDescending(x => x.ischecked);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                items =
                    from p in items
                    orderby p.C_id descending
                    select p;
            }

            IQueryable<RequestSellerDTO> resultItems = (
                from a in items
                select new RequestSellerDTO
                {
                    Id = a.C_id,
                    User = new UserDTO
                    {
                        Id = a.User.C_userid,
                        Username = a.User.username,
                        Email = a.User.email,
                    },
                    Name = a.name,
                    Phone = a.phone,
                    Info = a.info,
                    AboutCulture = a.aboutculture,
                    IsChecked = a.ischecked,
                    Submited = a.submitted,
                    Checked = a.checkeddate,
                    Updated = a.updated
                });

            if (perPage == -1)
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<RequestSellerDTO>
                {
                    Items = allItems,
                    PerPage = perPage,
                    RowCount = allItems.Count
                };
            }
            else
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<RequestSellerDTO>
                {
                    Items = resultItems.Skip((page - 1) * perPage).Take(perPage).ToList<RequestSellerDTO>(),
                    PerPage = perPage,
                    RowCount = resultItems.Count()
                };
            }
        }

        public SellerImageDTO GetSellerImage(int id)
        {
            try
            {
                var request = db.RequestSellers.FirstOrDefault(x => x.C_id == id);

                if (request != null)
                {
                    return new SellerImageDTO
                    {
                        RequestId = request.C_id,
                        Content = request.pic
                    };
                }
                else
                {
                    return new SellerImageDTO { RequestId = 0 ,Content = new byte[1] };
                }
            }
            catch (Exception e)
            {
                return new SellerImageDTO { RequestId = 0, Content = new byte[1] };
            }
        }

        // 셀러 확정
        public bool SelectSeller(int id, bool isChecked)
        {
            try
            {
                var request = db.RequestSellers.FirstOrDefault(x => x.C_id == id);

                if (request != null)
                {
                    if (isChecked)
                    {
                        request.ischecked = isChecked;
                        request.checkeddate = DateTime.Now;
                    }
                    else
                    {
                        request.ischecked = isChecked;
                        request.checkeddate = null;
                    }
                    

                    db.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        // 사진 들고오기
        public ItemsDTO<DaypicDTO> GetMarket2Picture(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var perPage = count;

            IQueryable<Daypic> items = db.Daypics;

            if (sorting != null)
            {
                var st = sorting.First();
                switch (st.Key)
                {
                    case "Id":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.C_id) : items.OrderByDescending(x => x.C_id);
                        break;
                    case "Title":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.title) : items.OrderByDescending(x => x.title);
                        break;
                    case "Created":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.created) : items.OrderByDescending(x => x.created);
                        break;
                    case "Updated":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.updated) : items.OrderByDescending(x => x.updated);
                        break;
                    case "IsDeprecated":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.isdeprecated) : items.OrderByDescending(x => x.isdeprecated);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                items =
                    from p in items
                    orderby p.C_id descending
                    select p;
            }

            IQueryable<DaypicDTO> resultItems = (
                from a in items
                select new DaypicDTO
                {
                    Id = a.C_id,
                    Title = a.title,
                    Created = a.created,
                    Updated = a.updated,
                    IsDeprecated = a.isdeprecated
                });

            if (perPage == -1)
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<DaypicDTO>
                {
                    Items = allItems,
                    PerPage = perPage,
                    RowCount = allItems.Count
                };
            }
            else
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<DaypicDTO>
                {
                    Items = resultItems.Skip((page - 1) * perPage).Take(perPage).ToList<DaypicDTO>(),
                    PerPage = perPage,
                    RowCount = resultItems.Count()
                };
            }
        }

        // 원데이클레스
        public ItemsDTO<OnedayClassDTO> GetOnedayClass(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var perPage = count;

            IQueryable<OnedayClass> items = db.OnedayClasses;

            if (sorting != null)
            {
                var st = sorting.First();
                switch (st.Key)
                {
                    case "Id":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.C_id) : items.OrderByDescending(x => x.C_id);
                        break;
                    case "Name":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.name) : items.OrderByDescending(x => x.name);
                        break;
                    case "Phone":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.phone) : items.OrderByDescending(x => x.phone);
                        break;
                    case "Created":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.created) : items.OrderByDescending(x => x.created);
                        break;
                    case "IsChecked":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.ischecked) : items.OrderByDescending(x => x.ischecked);
                        break;
                    case "Checked":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.checkeddate) : items.OrderByDescending(x => x.checkeddate);
                        break;
                }
            }
            else
            {
                items =
                    from p in items
                    orderby p.C_id descending
                    select p;
            }

            IQueryable<OnedayClassDTO> resultItems = (
                from a in items
                select new OnedayClassDTO
                {
                    Id = a.C_id,
                    Name = a.name,
                    Phone = a.phone,
                    Checked = a.checkeddate,
                    IsChecked = a.ischecked,
                    Created = a.created,
                });

            if (perPage == -1)
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<OnedayClassDTO>
                {
                    Items = allItems,
                    PerPage = perPage,
                    RowCount = allItems.Count
                };
            }
            else
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<OnedayClassDTO>
                {
                    Items = resultItems.Skip((page - 1) * perPage).Take(perPage).ToList<OnedayClassDTO>(),
                    PerPage = perPage,
                    RowCount = resultItems.Count()
                };
            }
        }
        public OnedayClassDTO GetOnedayClassById(int id)
        {
            var seller = (from a in db.OnedayClasses
                          where a.C_id == id
                          select new OnedayClassDTO
                          {
                              Id = a.C_id,
                              Name = a.name,
                              Phone = a.phone,
                              Created = a.created,
                              IsChecked = a.ischecked,
                              Checked = a.checkeddate,
                          }).FirstOrDefault();

            if (seller != null)
            {
                return seller;
            }

            return new OnedayClassDTO();
        }
        public bool SelectOneday(int id, bool isChecked)
        {
            try
            {
                var oneday = db.OnedayClasses.FirstOrDefault(x => x.C_id == id);

                if (oneday != null)
                {
                    if (isChecked)
                    {
                        oneday.ischecked = isChecked;
                        oneday.checkeddate = DateTime.Now;
                    }
                    else
                    {
                        oneday.ischecked = isChecked;
                        oneday.checkeddate = null;
                    }


                    db.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion




        // 유저관리
        public ItemsDTO<UserDTO> GetUser(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var perPage = count;

            IQueryable<User> items = db.Users;

            if (sorting != null)
            {
                var st = sorting.First();
                switch (st.Key)
                {
                    case "Id":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.C_userid) : items.OrderByDescending(x => x.C_userid);
                        break;
                    case "Username":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.username) : items.OrderByDescending(x => x.username);
                        break;
                    case "Email":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.email) : items.OrderByDescending(x => x.email);
                        break;
                    case "Created":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.created) : items.OrderByDescending(x => x.created);
                        break;
                    case "Logined":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.logined) : items.OrderByDescending(x => x.logined);
                        break;
                    case "IsDeprecated":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.isdeprecated) : items.OrderByDescending(x => x.isdeprecated);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                items =
                    from p in items
                    orderby p.C_userid descending
                    select p;
            }

            IQueryable<UserDTO> resultItems = (
                from a in items
                select new UserDTO
                {
                    Id = a.C_userid,
                    Username = a.username,
                    Email = a.email,
                    Created = a.created,
                    Logined = a.logined,
                    IsDeprecated = a.isdeprecated
                });

            if (perPage == -1)
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<UserDTO>
                {
                    Items = allItems,
                    PerPage = perPage,
                    RowCount = allItems.Count
                };
            }
            else
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<UserDTO>
                {
                    //Items = resultItems.OrderBy(x => x.Id).Skip((page - 1) * perPage).Take(perPage).ToList<UserDTO>(),
                    Items = resultItems.Skip((page - 1) * perPage).Take(perPage).ToList<UserDTO>(),
                    PerPage = perPage,
                    RowCount = resultItems.Count()
                };
            }
        }


        // 프로젝트
        public ItemsDTO<ProjectDTO> GetProjects(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var perPage = count;

            IQueryable<Project> items = db.Projects;

            if (sorting != null)
            {
                var st = sorting.First();
                switch (st.Key)
                {
                    case "Id":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.C_id) : items.OrderByDescending(x => x.C_id);
                        break;
                    case "Title":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.title) : items.OrderByDescending(x => x.title);
                        break;
                    case "StartDate":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.startdate) : items.OrderByDescending(x => x.startdate);
                        break;
                    case "EndDate":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.enddate) : items.OrderByDescending(x => x.enddate);
                        break;
                    case "Created":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.created) : items.OrderByDescending(x => x.created);
                        break;
                    case "Updated":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.updated) : items.OrderByDescending(x => x.updated);
                        break;
                    case "IsDeprecated":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.isdeprecated) : items.OrderByDescending(x => x.isdeprecated);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                items =
                    from p in items
                    orderby p.C_id descending
                    select p;
            }

            IQueryable<ProjectDTO> resultItems = (
                from a in items
                select new ProjectDTO
                {
                    Id = a.C_id,
                    Title = a.title,
                    Content= a.content,
                    StartDate = a.startdate,
                    EndDate = a.enddate,
                    Created = a.created,
                    Updated = a.updated,
                    IsDeprecated = a.isdeprecated
                });

            if (perPage == -1)
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<ProjectDTO>
                {
                    Items = allItems,
                    PerPage = perPage,
                    RowCount = allItems.Count
                };
            }
            else
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<ProjectDTO>
                {
                    Items = resultItems.Skip((page - 1) * perPage).Take(perPage).ToList<ProjectDTO>(),
                    PerPage = perPage,
                    RowCount = resultItems.Count()
                };
            }
        }


        // 카테고리
        public ItemsDTO<ProductCategoryDTO> GetProductCategories(Dictionary<string, string> sorting, Dictionary<string, string> filter, int page = 1, int count = 10)
        {
            var perPage = count;

            IQueryable<ProductCategory> items = db.ProductCategories;

            if (sorting != null)
            {
                var st = sorting.First();
                switch (st.Key)
                {
                    case "Id":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.C_id) : items.OrderByDescending(x => x.C_id);
                        break;
                    case "Label":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.label) : items.OrderByDescending(x => x.label);
                        break;
                    case "IsDeprecated":
                        items = st.Value.Equals("asc") ? items.OrderBy(x => x.isdeprecated) : items.OrderByDescending(x => x.isdeprecated);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                items =
                    from p in items
                    orderby p.C_id descending
                    select p;
            }

            IQueryable<ProductCategoryDTO> resultItems = (
                from a in items
                select new ProductCategoryDTO
                {
                    Id = a.C_id,
                    Label = a.label,
                    IsDeprecated = a.isdeprecated
                });

            if (perPage == -1)
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<ProductCategoryDTO>
                {
                    Items = allItems,
                    PerPage = perPage,
                    RowCount = allItems.Count
                };
            }
            else
            {
                var allItems = resultItems.ToList();
                return new ItemsDTO<ProductCategoryDTO>
                {
                    Items = resultItems.Skip((page - 1) * perPage).Take(perPage).ToList<ProductCategoryDTO>(),
                    PerPage = perPage,
                    RowCount = resultItems.Count()
                };
            }
        }
    }
}