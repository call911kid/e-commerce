namespace BLL.DTOs.Cart
{
    public class AddToCartDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}