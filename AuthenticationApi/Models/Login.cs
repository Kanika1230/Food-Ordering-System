using System;
using System.Collections.Generic;

namespace AuthenticationApi.Models
{
    public partial class Login
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
