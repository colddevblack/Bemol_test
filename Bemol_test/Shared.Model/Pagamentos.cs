using System;

namespace Shared.Model
{
    public class Pagamentos
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
        public DateTime Booked { get; set; }
    }
}
