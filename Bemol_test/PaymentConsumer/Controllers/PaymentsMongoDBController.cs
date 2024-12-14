//using PaymentConsumer.Models;
//using PaymentConsumer.Services;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;

//namespace PaymentConsumer.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PaymentsController : ControllerBase
//    {
//        private readonly PaymentsService _PaymentService;

//        public PaymentsController(PaymentsService PaymentsService)
//        {
//            _PaymentService = PaymentsService;
//        }

//        [HttpGet]
//        public ActionResult<List<Payment>> Get() =>
//            _PaymentService.Get();

//        [HttpGet("{id:length(24)}", Name = "GetPayment")]
//        public ActionResult<Payment> Get(string id)
//        {
//            var Payment = _PaymentService.Get(id);

//            if (Payment == null)
//            {
//                return NotFound();
//            }

//            return Payment;
//        }

//        [HttpPost]
//        public ActionResult<Payment> Create(Payment Payment)
//        {
//            _PaymentService.Create(Payment);

//            return CreatedAtRoute("GetPayment", new { id = Payment.Id.ToString() }, Payment);
//        }

//        [HttpPut("{id:length(24)}")]
//        public IActionResult Update(string id, Payment PaymentIn)
//        {
//            var Payment = _PaymentService.Get(id);

//            if (Payment == null)
//            {
//                return NotFound();
//            }

//            _PaymentService.Update(id, PaymentIn);

//            return NoContent();
//        }

//        [HttpDelete("{id:length(24)}")]
//        public IActionResult Delete(string id)
//        {
//            var Payment = _PaymentService.Get(id);

//            if (Payment == null)
//            {
//                return NotFound();
//            }

//            _PaymentService.Delete(Payment.Id);

//            return NoContent();
//        }
//    }
//}