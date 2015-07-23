using newDandi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newDandi.Views.Management
{
    public class DandiMarket2SellerDetailViewModel
    {
        public RequestSellerDTO Seller { get; set; }
    }

    public class DandiMarket2OnedayDetailViewModel
    {
        public OnedayClassDTO Oneday { get; set; }
    }

    public class AboutProjectViewModel
    {
        public ProjectDTO Project { get; set; }
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool IsDeprecated { get; set; }
    }
}