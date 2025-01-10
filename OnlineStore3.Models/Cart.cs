namespace OnlineStore3.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string Username { get; set; }
        public int Quantity { get; set; }
        public int FinalPrice { get; set; }
    }
}
