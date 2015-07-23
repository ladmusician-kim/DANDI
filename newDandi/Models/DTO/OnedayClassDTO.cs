using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newDandi.Models.DTO
{
    public class OnedayClassDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsChecked { get; set; }
        public DateTime? Checked { get; set; }
        public DateTime Created { get; set; }

        public UserDTO User { get; set; }
    }
}