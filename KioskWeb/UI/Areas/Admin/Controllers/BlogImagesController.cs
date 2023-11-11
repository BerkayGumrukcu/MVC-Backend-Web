
    using System;
       using System.Linq;
    using System.Net;
    using Entities.Models;
    using global::UI.Areas.Admin.Utility;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
   


    namespace UI.Areas.Admin.Controllers
    {
        [Area("admin")]
        [Authorize]
        public class BlogImagesController : Controller
        {
            private readonly IBlogService _blogService;
            private readonly IBlogImageService _blogImageService;
            private readonly IFileUploader _fileUploader;

            public BlogImagesController(IBlogService blogService, IBlogImageService blogImageService, IFileUploader fileUploader)
            {
                _blogService = blogService;
                _blogImageService = blogImageService;
                _fileUploader = fileUploader;
            }

            public IActionResult Index(int? id)
            {
                if (id.HasValue)
                {
                    return View(id);
                }
                return RedirectToAction("Index", "Blogs");
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
                    Blog blog = _blogService.GetById(id.Value);

                    if (blog == null)
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
                                BlogImage image = new BlogImage
                                {
                                    ImageUrl = result.FileUrl,
                                    BlogId = id.Value
                                };

                                _blogImageService.Add(image);
                                _blogImageService.Save();
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
                    var image = _blogImageService.GetById(id.Value);
                    if (image == null)
                    {
                        return Json(HttpStatusCode.NoContent);
                    }
                    _blogImageService.Remove(image);
                    _blogImageService.Save();
                    return Json(HttpStatusCode.OK);
                }
                return Json(HttpStatusCode.BadRequest);
            }

            public IActionResult _Active(int? id)
            {
                if (id.HasValue)
                {
                    var image = _blogImageService.GetById(id.Value);
                    if (image == null)
                    {
                        return Json(new { Code = HttpStatusCode.NoContent, Result = false });
                    }

                    var images = _blogImageService.GetByDefault(x => x.BlogId == image.BlogId);
                    _blogImageService.SetFalse(images);

                    image.IsActive = true;

                    _blogImageService.Update(image);
                    _blogImageService.Save();
                    return Json(new { Code = HttpStatusCode.OK, Result = image.IsActive });
                }
                return Json(new { Code = HttpStatusCode.BadRequest, Result = false });
            }


            public IActionResult _Index(int? id)
            {
                if (id.HasValue)
                {
                    var resimler = _blogImageService.GetByDefault(x => x.BlogId == id).Select(x => new { x.Id, x.ImageUrl, x.IsActive }).ToList();

                    if (resimler.Count > 0)
                    {
                        return Json(resimler);
                    }
                }

                return Json(HttpStatusCode.BadRequest);
            }
        }
    }
