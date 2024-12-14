namespace PaymentConsumer.Models;

public class PaymentsDatabaseSettings : IPaymentsDatabaseSettings
{
    public string PaymentsCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}

public interface IPaymentsDatabaseSettings
{
    string PaymentsCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}