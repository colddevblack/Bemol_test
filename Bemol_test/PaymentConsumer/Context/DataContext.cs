
using Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace PaymentConsumer.Context
{
    public class DataContext : DbContext
    {
        private const string Options = "PaymentDB";


#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        {
            Database.EnsureCreated();
        }

        public DbSet<Pagamentos> Paymentdb { get; set; }

    }
}
