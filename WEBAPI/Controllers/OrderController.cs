﻿using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost(nameof(PlaceOrder))]
        public async Task<IActionResult> PlaceOrder([FromBody] CheckoutDetails checkoutDetails)
        {
            var res = await _orderService.PlaceOrder(checkoutDetails);
            return Ok(res);
        }

        [HttpPost(nameof(GetOrders))]
        [Authorize(Roles =$"{MasterRole.ADMIN},{MasterRole.VENDOR}")]
        public async Task<IActionResult> GetOrders()
        {
            var res = await _orderService.GetAllOrders(User.GetLoggedInUserId<int>());
            return Ok(res);
        }
    }
}
