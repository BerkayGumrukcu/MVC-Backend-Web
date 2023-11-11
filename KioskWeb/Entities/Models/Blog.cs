using Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Blog : CoreEntity
    {
        public string Title { get; set; }

        public string BlogDescription { get; set; }

        public DateTime Date { get; set; }

        public string? Content { get; set; }

        public ICollection<BlogImage>? BlogImages { get; set; }
    }
}
