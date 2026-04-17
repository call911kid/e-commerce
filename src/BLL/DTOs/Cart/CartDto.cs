namespace BLL.DTOs.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public decimal SubTotal => Items.Sum(i => i.UnitPrice * i.Quantity);
    }
}