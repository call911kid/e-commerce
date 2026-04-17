using System;

namespace DAL.Models
{
    public class CartItem
    {
        public CartItem(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice => Product.Price * Quantity;
    }
}
