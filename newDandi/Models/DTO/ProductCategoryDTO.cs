using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newDandi.Models.DTO
{
    public class ProductCategoryDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool IsDeprecated { get; set; }
    }
}