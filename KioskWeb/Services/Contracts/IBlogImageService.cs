using Entities.Models;
using Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
   public interface IBlogImageService : IService<BlogImage>
    {
        void SetFalse(List<BlogImage> blogImages);
    }
}
