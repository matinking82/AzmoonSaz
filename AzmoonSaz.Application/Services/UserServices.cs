using AzmoonSaz.Application.Interfaces;
using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs;
using AzmoonSaz.Common.DTOs.User;
using AzmoonSaz.Common.Enums;
using AzmoonSaz.Common.Utilities;
using AzmoonSaz.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ResultDto> AddStudentByTeacher(RequestAddStudentByTeacherDto request)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var classroom = await _context.Classrooms.FindAsync(request.ClassId);

                    if (classroom == null)
                    {
                        return new ResultDto()
                        {
                            Status = ServiceStatus.NotFound,
                        };
                    }

                    User newUser = new User()
                    {
                        Password = await request.UserName.ToHashedAsync(),
                        UserName = request.UserName,
                    };

                    UserInClassroom newUserInClassroom = new UserInClassroom()
                    {
                        Classroom = classroom,
                        ClassroomId = request.ClassId,
                        User = newUser,
                        UserId = newUser.Id,
                    };

                    await _context.Users.AddAsync(newUser);

                    await _context.UserInClassrooms.AddAsync(newUserInClassroom);

                    await _context.SaveChangesAsync();



                    return new ResultDto()
                    {
                        Status = ServiceStatus.Success,
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

        public async Task<bool> AddUserAsync(User user)
        {
            return await Task.Run(async () =>
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

        public async Task<ResultDto> DeleteStudentFromClass(int userId)
        {
            return await Task.Run(async () =>
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return new ResultDto()
                    {
                        Status = ServiceStatus.NotFound,
                    };
                }

                await DeleteUser(user);

                return new ResultDto()
                {
                    Status = ServiceStatus.Success
                };
            });
        }

        public async Task<bool> DeleteUser(User user)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    #region Delete UserInClassrooms

                    var UserInClassrooms = await _context.UserInClassrooms
                    .Where(c => c.UserId == user.Id)
                    .ToListAsync();


                    foreach (var item in UserInClassrooms)
                    {
                        _context.UserInClassrooms.Remove(item);
                    }

                    #endregion

                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteUserById(int userId)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var User = await _context.Users.FindAsync(userId);
                    var res = await DeleteUser(User);
                    return res;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }



        public async Task<User> GetUserByUserName(string username)
        {
            return await Task.Run(async () =>
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            });
        }

        public async Task<bool> IsExistUserNameAsync(string username)
        {
            return await Task.Run(() =>
            {
                return _context.Users.Any(u => u.UserName == username);
            });
        }



        public async Task<ResultDto<int>> LoginUserAsync(RequestLoginUserDto request)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    User user = await GetUserByUserName(request.UserName);

                    if (user == null)
                    {
                        return new ResultDto<int>()
                        {
                            Status = ServiceStatus.NotFound
                        };
                    }

                    if (user.Password == await request.Password.ToHashedAsync())
                    {
                        return new ResultDto<int>()
                        {
                            Status = ServiceStatus.Success,
                            Data = user.Id
                        };
                    }

                    return new ResultDto<int>()
                    {
                        Status = ServiceStatus.NotFound
                    };
                }
                catch (Exception)
                {
                    return new ResultDto<int>()
                    {
                        Status = ServiceStatus.SystemError,
                    };
                }
            });
        }

        public async Task<ResultDto> SignupUserAsync(RequestSignupUserDto request)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if (await IsExistUserNameAsync(request.UserName))
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
