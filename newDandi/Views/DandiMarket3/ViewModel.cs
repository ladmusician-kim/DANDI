using newDandi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newDandi.Views.DandiMarket3
{
    public class SellerDetailViewModel
    {
        public RequestSellerDTO Seller { get; set; }
    }

    public class RequestViewModel
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }
        public string AboutCulture { get; set; }
        public string Product { get; set; }

        public bool isEdit { get; set; }

        public string Error { get; set; }
    }
}