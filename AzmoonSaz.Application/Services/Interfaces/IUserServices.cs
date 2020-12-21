using AzmoonSaz.Common.DTOs;
using AzmoonSaz.Common.DTOs.User;
using AzmoonSaz.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Application.Services.Interfaces
{
    public interface IUserServices
    {
        Task<bool> AddUserAsync(User user);

        Task<ResultDto> SignupUserAsync(RequestSignupUserDto request);
    }
}
