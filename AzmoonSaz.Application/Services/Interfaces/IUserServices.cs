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

        Task<bool> IsExistUserNameAsync(string username);
        Task<User> GetUserByUserName(string username);

        Task<ResultDto> SignupUserAsync(RequestSignupUserDto request);
        Task<ResultDto<int>> LoginUserAsync(RequestLoginUserDto request);

        Task<ResultDto> AddStudentByTeacher(RequestAddStudentByTeacherDto request);

        Task<bool> DeleteUserById(int userId);
        Task<bool> DeleteUser(User user);
        Task<ResultDto> DeleteStudentFromClass(int userId);
    }
}
