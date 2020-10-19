using System;
using System.Collections.Generic;

namespace OrderListApi.Models
{
    public partial class Login
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
