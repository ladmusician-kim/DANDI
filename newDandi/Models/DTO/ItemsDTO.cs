using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newDandi.Models.DTO
{
    public class ItemsDTO<T>
    {
        public List<T> Items { get; set; }
        public int PerPage { get; set; }
        public int RowCount { get; set; }
    }
}