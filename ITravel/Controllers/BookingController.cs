using ITravel.Domain.Commons;
using ITravel.Domain.Entities;
using ITravel.Domain.Entities.Payme;
using ITravel.Service.Extensions;
using ITravel.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace ITravel.Controllers
{
    public class BookingController : Controller
    {
        string payme_merchant = "";
        public string payme_id = "1"; 
        string PaymeQrcode = "";
        private readonly IPaymeService paymeService;
        private readonly ISettingService settingService;
        public BookingController(IPaymeService paymeService, ISettingService settingService)
        {
            this.paymeService = paymeService;
            this.settingService = settingService;
        }


        [Route("Booking")]
        public IActionResult Booking()
        {
            return View();
        }

        public async Task<IActionResult> PaymePay(string Name, decimal Amount)
        {
            var soft_sett = await settingService.GetAsync();
            payme_merchant = soft_sett.PaymeKassId;

            var payme_order = paymeService.GetLastOrder();

            if (payme_order != null)
            {
                payme_id = (payme_order.id + 1).ToString();
            }

            var new_order = new order()
            {
                amount = Amount,
                source = "payme",
                status = "new",
            };

            await paymeService.AddOrder(new_order);

            string payme_amount = (Amount * 100).ToString();

            PaymeQrcode = "m=" + payme_merchant + ";ac.order_id=" + payme_id + ";a=" + payme_amount;
            PaymeQrcode = Base64Encode(PaymeQrcode);
            PaymeQrcode = "https://checkout.paycom.uz/" + PaymeQrcode;

            return Redirect(PaymeQrcode);
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Integration endpoint (DANGER!!!)")]
        public async ValueTask<ActionResult<PaymeResponse<object>>> DirectMethod([FromBody] PaymeRequest request)
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                //var headerCredentials = HeaderExtensions.GetBasicCredentials(authHeader);
                var headerCredentials = authHeader.Split(' ');
                var username = (await settingService.GetAsync()).username;
                var password = (await settingService.GetAsync()).password;
                await Console.Out.WriteLineAsync(headerCredentials[0] + " " + headerCredentials[1]);

                if (username.Equals(headerCredentials[0]) && password.Equals(headerCredentials[1]))
                {
                    return Ok(await paymeService.DirectMethod(request));
                }
                else
                {
                    return Ok(new PaymeResponse<object>
                    {
                        error = new PaymeError
                        {
                            Code = -32504,
                            Message = "Login or password incorrect",
                            Data = headerCredentials[0] + ":" + headerCredentials[1]
                        }
                    });
                }
            }
            return Ok(new PaymeResponse<object>
            {
                error = new PaymeError
                {
                    Code = -32504,
                    Message = "Auth is invalid",
                    Data = "payment"
                }
            });
        }
    }
}
