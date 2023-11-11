using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Areas.Admin.Models;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ProjectContext _context;

        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IPasswordValidator<User> _passwordValidator;

        public AdminController(
            ProjectContext context,
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher,
            IPasswordValidator<User> passwordValidator)
        {
            _context = context;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = vm.UserName;
                user.Email = vm.Email;

                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vm);
        }




        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id.HasValue)
            {
                var user = await _userManager.FindByIdAsync(id.Value.ToString());
                if (user != null)
                {
                    UserVM vm = new UserVM();
                    user.UserName = vm.UserName;
                    vm.Password = user.PasswordHash;
                    vm.Email = user.Email;
                    return View(vm);
                }

                return RedirectToAction("Index");
            }
            return View("Index", _userManager.Users);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(vm.Id.ToString());

                if (user != null)
                {
                    user.Email = vm.Email;
                    IdentityResult validPassword = null;

                    if (!string.IsNullOrWhiteSpace(vm.Password))
                    {
                        validPassword = await _passwordValidator.ValidateAsync(_userManager, user, vm.Password);
                        if (validPassword.Succeeded)
                        {
                            user.PasswordHash = _passwordHasher.HashPassword(user, vm.Password);
                        }
                        else
                        {
                            foreach (var item in validPassword.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }

                    if (validPassword.Succeeded)
                    {
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "user not found");
                }
            }

            return View("Index", _userManager.Users);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id.HasValue)
            {
                var user = await _userManager.FindByIdAsync(id.Value.ToString());
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "user not found");
                }
            }

            return View("Index", _userManager.Users);
        }
    }
}
