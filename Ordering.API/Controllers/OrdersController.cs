using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    //[Authorize]
    [ApiController]
    public class OrdersController : Controller
    {

        public OrdersController()
        {

        }

        [Route("{orderId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            //try
            //{
            //    var order = await _orderQueries
            //        .GetOrderAsync(orderId);

            //    return Ok(order);
            //}
            //catch (KeyNotFoundException)
            //{
                
            //}

            return NotFound();
        }

    }
}
