using Application.Dto;
using Application.Interfaces;
using Application.Main;
using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuayaBytes.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {

        private readonly IPaymentsApplication _paymentsApplication;

        public PaymentsController(IPaymentsApplication paymentsApplication)
        {
            _paymentsApplication = paymentsApplication;
        }
       
        [HttpGet("client_token")]
        public async Task<IActionResult> GetClientToken()
        {

            var clientToken = await _paymentsApplication.GetClientToken();
            if (clientToken.IsSuccess)
                return Ok(clientToken);
            
            return BadRequest(clientToken);
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] PaymentRequestDto paymentRequest)
        {
            
            var result = await _paymentsApplication.Checkout(paymentRequest);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Message);
        }

    }
}