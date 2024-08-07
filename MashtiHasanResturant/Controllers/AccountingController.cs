using MashtiHasanRestaurant.Core.DTOs;
using MashtiHasanRestaurant.Core.Services.Interface;
using MashtiHasanRestaurant.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MashtiHasanResturant.Controllers
{
    public class AccountingController : Controller
    {
        private IUserService _userService;


        public AccountingController(IUserService userService)
        {
            _userService = userService;

        }
        public IActionResult Index()
        {
            return View();
        }


        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
          
            if (!ModelState.IsValid)
                return View(register);
            if (_userService.IsExistUserName(register.Mobile))
            {
                ModelState.AddModelError("Mobile", "شماره موبایل تکراری است");
                return View(register);
            }

            Customer customer = new Customer()
            {
                CustomerName = register.CustomerName,
                Mobile = register.Mobile,
                Tel = register.Tel,
                Address = register.Address,
                ContactTitle = register.ConcatTitle 
            };

        
            _userService.AddCustomer(customer);

            
            return RedirectToAction("SuccessRegister");
        }

        [Route("SuccessRegister")]
        public IActionResult SuccessRegister()
        {
            return View(); 
        }

        #endregion

    }
}
