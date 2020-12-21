using AzmoonSaz.Application.Interfaces;
using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs;
using AzmoonSaz.Common.DTOs.User;
using AzmoonSaz.Common.Enums;
using AzmoonSaz.Common.Utilities;
using AzmoonSaz.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IDataBaseContext _context;
        public UserServices(IDataBaseContext context)
        {
            _context = context;
        }

        public Task<bool> AddUserAsync(User user)
        {
            return Task.Run(async () =>
            {
                try
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<ResultDto> SignupUserAsync(RequestSignupUserDto request)
        {
            return await Task.Run(async () =>
            {
                try
                {

                    if (_context.Users.Any(u => u.UserName == request.UserName))
                    {
                        return new ResultDto()
                        {
                            Status = ServiceStatus.Error,
                            Message = "نام کاربری قبلا استفاده شده است"
                        };
                    }

                    User newUser = new User()
                    {
                        UserName = request.UserName,
                        Password = await request.Password.ToHashedAsync(),
                    };

                    await AddUserAsync(newUser);

                    return new()
                    {
                        Status = ServiceStatus.Success
                    };
                }
                catch (Exception)
                {
                    return new ResultDto()
                    {
                        Status = ServiceStatus.SystemError,
                    };
                }
            });
        }
    }
}
