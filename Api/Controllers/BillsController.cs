using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hotel.Core.Managers;
using Hotel.Domain.Enums;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly ILogger<BillsController> _log;
        private readonly ICheckoutManager _checkout;

        public BillsController(ILogger<BillsController> log, ICheckoutManager checkout)
        {
            _log = log;
            _checkout = checkout;
        }

        [HttpGet]
        public async Task<ActionResult> Get(long customerId)
        {
            var result = await _checkout.GetOverstayFee(customerId);
            return Ok(result);
        }

    }
}