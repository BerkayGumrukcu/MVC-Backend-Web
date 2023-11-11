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
    public class ProjectImageService : BaseService<ProjectImage>, IProjectImageService
    {
        private ProjectContext _context;
        public ProjectImageService(ProjectContext context) : base(context)
        {
            _context = context;
        }

        public void SetFalse(List<ProjectImage> projectImages)
        {
            foreach (ProjectImage projectImage in projectImages)
            {
                projectImage.IsActive = false;
                Update(projectImage);
            }
            Save();
        }
    }
}
