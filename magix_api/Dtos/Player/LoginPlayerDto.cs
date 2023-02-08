using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magix_api.Dtos.Player
{
    public class LoginPlayerDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}