using ECinema.Domain.Domain_models;
using ECinema.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECinema.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ShoppApplicationUser> userManager;

        public AdminController(IOrderService orderService, UserManager<ShoppApplicationUser> userManager)
        {
            this.userManager = userManager;
            this._orderService = orderService;
    }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return this._orderService.GetAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this._orderService.GetOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDTO> model)
        {
            bool status = true;

            foreach (var user in model)
            {
                var userCheck = userManager.FindByEmailAsync(user.Email).Result;
                if (userCheck != null)
                {
                    var newUser = new ShoppApplicationUser
                    {
                        UserName = userCheck.Email,
                        NormalizedEmail = userCheck.Email,
                        Email = userCheck.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserShoppingCart = new ShoppingCart()
                    };

                    var result = userManager.CreateAsync(newUser, user.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }
    }
}
