using System;
using System.Collections.Generic;
using Common.Enums;

namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
    }
}
