using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newDandi.Models.DTO
{
    public class DaypicDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsDeprecated { get; set; }

        public int For_ProductId { get; set; }
        public ProjectDTO Project { get; set; }
    }
}