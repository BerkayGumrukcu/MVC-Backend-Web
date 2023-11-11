using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class IletisimController : Controller
    {

        private readonly ICustomerMessageService _customerMessageService;

        public IletisimController(ICustomerMessageService customerMessageService)
        {
            _customerMessageService = customerMessageService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CustomerMessage customerMessage)
        {
            if (ModelState.IsValid)
            {
                _customerMessageService.Add(customerMessage);
                bool result = _customerMessageService.Save() > 0;
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customerMessage);
        }
    }
}

