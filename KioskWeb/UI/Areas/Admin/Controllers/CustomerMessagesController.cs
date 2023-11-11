using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace UI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class CustomerMessagesController : Controller
    {
        private readonly ICustomerMessageService _customerMessageService;

        public CustomerMessagesController(ICustomerMessageService customerMessageService)
        {
            _customerMessageService = customerMessageService;
        }

        public IActionResult Index()
        {
            var customerMessage = _customerMessageService.GetAll();
            return View(customerMessage);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerMessage = _customerMessageService.GetById(id.Value);
            if (customerMessage == null)
            {
                return NotFound();
            }
            return View(customerMessage);
        }

        public IActionResult Delete(int? id)
        {

            if (id.HasValue)
            {
                var customerMessage = _customerMessageService.GetById(id.Value);
                if (customerMessage == null)
                {
                    return NotFound();
                }

                _customerMessageService.Remove(customerMessage);
                _customerMessageService.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
