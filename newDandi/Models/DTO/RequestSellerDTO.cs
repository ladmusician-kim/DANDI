using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace newDandi.Models.DTO
{
    public class RequestSellerDTO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }
        public string AboutCulture { get; set; }
        public UserDTO User { get; set; }
        public bool IsChecked { get; set; }
        public byte[] Picture { get; set; }
        public String Product { get; set; }
        public DateTime? Submited { get; set; }
        public DateTime? Checked { get; set; }
        public DateTime? Updated { get; set; }

        public FileContentResult PictureFile { get; set; }
    }
}