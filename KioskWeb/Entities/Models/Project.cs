using Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Project : CoreEntity
    {
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public DateTime ProjectDate { get; set; }

        public ICollection<ProjectImage>? ProjectImages { get; set; }
    }
}
