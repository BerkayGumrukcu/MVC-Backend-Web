using System;
using System.Linq;
using System.Net;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using UI.Areas.Admin.Utility;


namespace UI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ProjectImagesController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectImageService _projectImageService;
        private readonly IFileUploader _fileUploader;

        public ProjectImagesController(IProjectService projectService, IProjectImageService projectImageService, IFileUploader fileUploader)
        {
            _projectService = projectService;
            _projectImageService = projectImageService;
            _fileUploader = fileUploader;
        }

        public IActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                return View(id);
            }
            return RedirectToAction("Index", "Projects");
        }

        public IActionResult Upload(int id)
        {
            return View(id);
        }

        [HttpPost]
        public IActionResult Upload(int? id, IFormFile[] file)
        {
            if (id.HasValue)
            {
                Project project = _projectService.GetById(id.Value);

                if (project == null)
                {
                    return NotFound();
                }

                if (file.Length > 0)
                {
                    foreach (var item in file)
                    {
                        var result = _fileUploader.Upload(item);

                        if (result.FileResult == UI.Areas.Admin.Utility.FileResult.Succeeded)
                        {
                            ProjectImage image = new ProjectImage
                            {
                                ImageUrl = result.FileUrl,
                                ProjectId = id.Value
                            };

                            _projectImageService.Add(image);
                            _projectImageService.Save();
                        }
                    }
                }
            }
            return View();
        }

        public IActionResult _Delete(int? id)
        {
            if (id.HasValue)
            {
                var image = _projectImageService.GetById(id.Value);
                if (image == null)
                {
                    return Json(HttpStatusCode.NoContent);
                }
                _projectImageService.Remove(image);
                _projectImageService.Save();
                return Json(HttpStatusCode.OK);
            }
            return Json(HttpStatusCode.BadRequest);
        }

        public IActionResult _Active(int? id)
        {
            if (id.HasValue)
            {
                var image = _projectImageService.GetById(id.Value);
                if (image == null)
                {
                    return Json(new { Code = HttpStatusCode.NoContent, Result = false });
                }

                var images = _projectImageService.GetByDefault(x => x.ProjectId == image.ProjectId);
                _projectImageService.SetFalse(images);

                image.IsActive = true;

                _projectImageService.Update(image);
                _projectImageService.Save();
                return Json(new { Code = HttpStatusCode.OK, Result = image.IsActive });
            }
            return Json(new { Code = HttpStatusCode.BadRequest, Result = false });
        }


        public IActionResult _Index(int? id)
        {
            if (id.HasValue)
            {
                var resimler = _projectImageService.GetByDefault(x => x.ProjectId == id).Select(x => new { x.Id, x.ImageUrl, x.IsActive }).ToList();

                if (resimler.Count > 0)
                {
                    return Json(resimler);
                }
            }

            return Json(HttpStatusCode.BadRequest);
        }
    }
}
