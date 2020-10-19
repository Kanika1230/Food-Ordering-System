using System;
using System.Collections.Generic;

namespace FoodOrderClient.Models
{
    public partial class Item
    {
        public Item()
        {
            Order = new HashSet<Order>();
        }

        public int Iid { get; set; }
        public string Iname { get; set; }
        public string Iprice { get; set; }
        public string Iavailability { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
