using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PaymentConsumer.Context;
using Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using PaymentConsumer.Context;
using Shared.Model;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.ComponentModel;
using PaymentConsumer.Controllers;

namespace PaymentPublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPublishController : ControllerBase
    {
        private readonly IBus _bus;
        public ILogger<PaymentsController> _logger;
        public List<Pagamentos> _payments;
        //public DataContext db;

        public PaymentPublishController( IBus bus)
        {
           
            _bus = bus;
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateTicket(Pagamentos mgs)
        {

          
            //deveria enviar lista do banco
            //var mgs = db.Paymentdb.ToList();
            //exemplo
            if (mgs != null)
            {
                //foreach (var m in mgs)
                //{
                    mgs.Booked = DateTime.Now;
                    Uri uri = new Uri("rabbitmq://localhost/Payment");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    await endPoint.Send(mgs);
                    return Ok();
                //}
            }

            return BadRequest();
        }
    }
}
