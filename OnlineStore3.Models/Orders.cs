using System;

namespace OnlineStore3.Models
{
    public class Orders
    {
        public string Username { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDetails { get; set; }
    }
}
