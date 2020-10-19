using System;
using System.Collections.Generic;

namespace AuthenticationApi.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public virtual Item Item { get; set; }
    }
}
