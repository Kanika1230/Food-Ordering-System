using System;
using System.Collections.Generic;

namespace ItemListApi.Models
{
    public partial class Login
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
