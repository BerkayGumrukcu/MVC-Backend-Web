using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Core
{
    public class CoreEntity
    {
        public CoreEntity()
        {
            Id = Id++;
        }

        public int Id { get; set; }

    }
}
