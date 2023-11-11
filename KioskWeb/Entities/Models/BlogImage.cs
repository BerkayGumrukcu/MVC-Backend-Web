using Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BlogImage : CoreEntity
    {
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
