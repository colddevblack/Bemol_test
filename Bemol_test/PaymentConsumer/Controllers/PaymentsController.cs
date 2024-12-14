using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using PaymentConsumer.Context;
using Shared.Model;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.ComponentModel;

namespace PaymentConsumer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        public ILogger<PaymentsController> _logger;
        public  DataContext db;
        public List<Pagamentos> _payments;


        public PaymentsController(DataContext conect, ILogger<PaymentsController> logger)
        {
            db = conect;
            _logger = logger;
        }




        //POST api/PagamentosController/CriaPagamentos
        [HttpPost]
        public HttpStatusCode CriarPagamento()
        {
           
            try
            {
                var model = new Pagamentos();
                
                    db.Paymentdb.Add(model);
                    db.SaveChanges();
                    _logger.LogInformation("Endpoint  registro" + model);
                    return HttpStatusCode.Created;
                    
             

            }
            catch (Exception ex)
            {
                _logger.LogWarning("erro" + ex);
                return HttpStatusCode.Found;

            }
           
        }
        // PUT api/PagamentosController/EditarPagamentos
        [HttpPut]
        public HttpStatusCode EditarPagamentos(int id)
        {
           


            try
            {
                    db.Entry(id).State = EntityState.Modified;
                    db.SaveChanges();
                    return HttpStatusCode.Accepted;
               
            }
            catch (Exception ex)
            {

                _logger.LogWarning("erro" + ex);
                return HttpStatusCode.Found;
            }

        }

        // GET api/PagamentosController/ListaPagamentos

        [HttpGet]
        public List<Pagamentos> ListaPagamentos()
        {
            try
            {
                   _payments = db.Paymentdb.ToList();
                return db.Paymentdb.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }




        // DELETE: api/PagamentosController/DeletePagamento

        [HttpDelete]
        public HttpStatusCode DeletePagamentos(int id)
        {
            try
            {
                // consulta linq
                
                
                var cli = db.Paymentdb.Find(id);
                db.Paymentdb.Remove(cli);
                db.SaveChanges();
                return HttpStatusCode.Accepted;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }

}


