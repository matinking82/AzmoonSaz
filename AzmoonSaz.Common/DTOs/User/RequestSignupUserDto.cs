using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Common.DTOs.User
{
    public record RequestSignupUserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
