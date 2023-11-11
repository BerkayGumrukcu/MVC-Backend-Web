using Entities.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;
using System.Linq;

namespace UI.CustomHelpers
{
    [HtmlTargetElement("article", Attributes = "projectimage")]
    public class ProjectImageTagHelper : TagHelper
    {
        private readonly IProjectImageService _projectImageService;
        public ProjectImageTagHelper(IProjectImageService projectImageService)
        {
            _projectImageService = projectImageService;
        }

        [HtmlAttributeName("projectimage")]
        public ProjectImage Project{ get; set; }
        private string ImageUrl { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var images = _projectImageService.GetByDefault(x => x.ProjectId == Project.ProjectId && x.IsActive != true);

            string tamplate = "";

            if (images.Count > 0)
            {
                ImageUrl = images.FirstOrDefault(x => x.ProjectId == Project.ProjectId && x.IsActive != true).ImageUrl;
            }
            else
            {
                ImageUrl = "/images/.jpg";
            }

            tamplate = @$"
                            <img src='{ImageUrl}' alt='' class='main-img'>
                          ";

            output.Content.SetHtmlContent(tamplate);
        }
    }
}
