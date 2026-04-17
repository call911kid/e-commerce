namespace BLL.DTOs.Order
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? ShippingMethod { get; set; }
    }
}