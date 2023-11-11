using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace UI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ProjectContext _context;

        public ProjectsController(IProjectService projectService,  ProjectContext context)
        {
            _projectService = projectService;
            _context = context;
        }
        public IActionResult Index()
        {
            var projects = _projectService.GetAll();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Add(project);
                bool result = _projectService.Save() > 0;
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(project);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id.HasValue)
            {

                var project = _projectService.GetById(id.Value);
                if (project == null)
                {
                    return NotFound();
                }

                return View(project);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project project)
        {

            if (ModelState.IsValid)
            {
                _projectService.Update(project);
                bool result = _projectService.Save() > 0;
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(project);

        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var project = _projectService.GetById(id.Value);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        public IActionResult Delete(int? id)
        {

            if (id.HasValue)
            {
                var project = _projectService.GetById(id.Value);
                if (project == null)
                {
                    return NotFound();
                }

                _projectService.Remove(project);
                _projectService.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
