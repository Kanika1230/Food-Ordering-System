using System;
using System.Collections.Generic;

namespace OrderListApi.Models
{
    public partial class Customer
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
