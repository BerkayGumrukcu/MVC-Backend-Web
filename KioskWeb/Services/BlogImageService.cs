using Entities.Context;
using Entities.Models;
using Services.Base;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BlogImageService : BaseService<BlogImage>, IBlogImageService
    {
        private ProjectContext _context;
        public BlogImageService(ProjectContext context) : base(context)
        {
            _context = context;
        }

        public void SetFalse(List<BlogImage> blogImages)
        {
            foreach (BlogImage blogImage in blogImages)
            {
                blogImage.IsActive = false;
                Update(blogImage);
            }
            Save();
        }
    }
}
