using PaymentConsumer.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace PaymentConsumer.Services
{
    public class PaymentsService
    {
        private readonly IMongoCollection<Payment> _Payments;

        public PaymentsService(IPaymentsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Payments = database.GetCollection<Payment>(settings.PaymentsCollectionName);
        }

        public List<Payment> Get() => _Payments.Find(Payment => true).ToList();

        public Payment Get(string id) => _Payments.Find(Payment => Payment.Id == id).FirstOrDefault();

        public Payment Create(Payment Payment)
        {
            _Payments.InsertOne(Payment);
            return Payment;
        }

        public void Update(string id, Payment updatedPayment) => _Payments.ReplaceOne(Payment => Payment.Id == id, updatedPayment);

        public void Delete(Payment PaymentForDeletion) => _Payments.DeleteOne(Payment => Payment.Id == PaymentForDeletion.Id);

        public void Delete(string id) => _Payments.DeleteOne(Payment => Payment.Id == id);
    }
}