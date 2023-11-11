using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace UI.Areas.Admin.Controllers
{
        [Area("admin")]
        [Authorize]
        public class BlogsController : Controller
        {
            private readonly IBlogService _blogService;
            private readonly ProjectContext _context;

            public BlogsController(IBlogService blogService, ProjectContext context)
            {
                _blogService = blogService;
                _context = context;
            }
            public IActionResult Index()
            {
                var blog = _blogService.GetAll();
                return View(blog);
            }

            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Blog blog)
            {
                if (ModelState.IsValid)
                {
                    _blogService.Add(blog);
                    bool result = _blogService.Save() > 0;
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(blog);
            }

            [HttpGet]
            public IActionResult Edit(int? id)
            {

                if (id.HasValue)
                {

                    var blog = _blogService.GetById(id.Value);
                    if (blog == null)
                    {
                        return NotFound();
                    }

                    return View(blog);
                }
                return RedirectToAction("Index");
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Blog blog)
            {

                if (ModelState.IsValid)
                {
                    _blogService.Update(blog);
                    bool result = _blogService.Save() > 0;
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(blog);

            }

            public IActionResult Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }


                var blog = _blogService.GetById(id.Value);
                if (blog == null)
                {
                    return NotFound();
                }

                return View(blog);
            }

            public IActionResult Delete(int? id)
            {

                if (id.HasValue)
                {
                    var blog= _blogService.GetById(id.Value);
                    if (blog == null)
                    {
                        return NotFound();
                    }

                    _blogService.Remove(blog);
                    _blogService.Save();
                }

                return RedirectToAction("Index");
            }
        }
    }
