using AzmoonSaz.Application.Interfaces;
using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs;
using AzmoonSaz.Common.DTOs.Classroom;
using AzmoonSaz.Common.Enums;
using AzmoonSaz.Domain.Entities.Classroom;
using AzmoonSaz.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Application.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IDataBaseContext _context;
        private readonly IUserServices _userServices;
        public ClassroomService(IDataBaseContext context,IUserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        public async Task<bool> AddClassroom(Classroom classroom)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await _context.Classrooms.AddAsync(classroom);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<ResultDto> CreateClassroomByUser(RequestCreateClassroomByUserDto request)
        {
            return await Task.Run(async () =>
            {
                var user = await _context.Users.FindAsync(request.CreatorId);

                if (user == null)
                {
                    return new ResultDto()
                    {
                        Status = ServiceStatus.NotFound,
                        Message = "کاربری یافت نشد"
                    };
                }

                Classroom newclassroom = new Classroom()
                {
                    ClassName = request.ClassName,
                    CreateDate = DateTime.Now,
                    CreatorId = request.CreatorId,
                    Creator = user,
                };

                await AddClassroom(newclassroom);

                return new ResultDto()
                {
                    Status = ServiceStatus.Success
                };
            });
        }

        public async Task<ResultDto> DeleteClassroomByClassId(int classId)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    #region Delete ClassStudents

                    var UserInClass = await _context.UserInClassrooms
                    .Where(u => u.ClassroomId == classId)
                    .ToListAsync();

                    foreach (var item in UserInClass)
                    {
                        //_context.UserInClassrooms.Remove(item);
                        var Res = await _userServices.DeleteUserById(item.UserId);

                        if (!Res)
                        {
                            return new ResultDto()
                            {
                                Status = ServiceStatus.SystemError
                            };
                        }
                    }

                    #endregion

                    #region Delete Class
                    var classroom = await GetClassroomByClassId(classId);

                    if (classroom == null)
                    {
                        return new ResultDto()
                        {
                            Status = ServiceStatus.NotFound,
                        };
                    }
                    await RemoveClassroom(classroom);
                    #endregion

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

        public async Task<ResultDto> EditClassroomByUser(RequestEditClassroomByUserDto request)
        {
            return await Task.Run(async () =>
            {
                try
                {

                    var classroom = await GetClassroomByClassId(request.ClassId);

                    if (classroom == null)
                    {
                        return new ResultDto()
                        {
                            Status = ServiceStatus.NotFound
                        };
                    }

                    if (classroom.ClassName != request.NewName)
                    {
                        classroom.ClassName = request.NewName;

                        await _context.SaveChangesAsync();
                    }

                    return new ResultDto()
                    {
                        Status = ServiceStatus.Success
                    };
                }
                catch (Exception)
                {
                    return new ResultDto()
                    {
                        Status = ServiceStatus.SystemError
                    };
                }

            });
        }

        public async Task<Classroom> GetClassroomByClassId(int classId)
        {
            return await Task.Run(async () =>
            {
                return await _context.Classrooms.FindAsync(classId);
            });
        }

        public async Task<ResultDto<RequestEditClassroomByUserDto>> GetClassroomDetailForEditByUser(int classId)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var classroom = await GetClassroomByClassId(classId);

                    if (classroom == null)
                    {
                        return new ResultDto<RequestEditClassroomByUserDto>()
                        {
                            Status = ServiceStatus.NotFound
                        };
                    }

                    RequestEditClassroomByUserDto Data = new RequestEditClassroomByUserDto
                    {
                        ClassId = classroom.Id,
                        NewName = classroom.ClassName,
                    };

                    return new ResultDto<RequestEditClassroomByUserDto>()
                    {
                        Status = ServiceStatus.Success,
                        Data = Data
                    };

                }
                catch (Exception)
                {
                    return new ResultDto<RequestEditClassroomByUserDto>()
                    {
                        Status = ServiceStatus.SystemError
                    };
                }
            });
        }

        public async Task<ResultDto<IEnumerable<StudentsListForClassDto>>> GetStudentsListForClassByClassId(int classId)
        {
            return await Task.Run(async () =>
            {
                var userinClasses = await _context.UserInClassrooms
                .Where(c => c.ClassroomId == classId)
                .ToListAsync();


                if (userinClasses.Count == 0)
                {
                    return new ResultDto<IEnumerable<StudentsListForClassDto>>()
                    {
                        Data = new List<StudentsListForClassDto>(),
                        Status = ServiceStatus.Success
                    };
                }

                List<StudentsListForClassDto> Data = new List<StudentsListForClassDto>();

                foreach (var item in userinClasses)
                {

                    User user = await _context.Users
                    .FindAsync(item.UserId);

                    StudentsListForClassDto student = new StudentsListForClassDto()
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                    };

                    Data.Add(student);
                }


                return new ResultDto<IEnumerable<StudentsListForClassDto>>()
                {
                    Data = Data,
                    Status = ServiceStatus.Success
                };

            });
        }

        public async Task<ResultDto<IEnumerable<ClassroomForListDto>>> GetUsersClassroomsByUserId(int userId)
        {
            return await Task.Run(async () =>
            {
                try
                {


                    List<ClassroomForListDto> MyClasses = await _context.Classrooms
                    .Where(c => c.CreatorId == userId)
                    .Select(c => new ClassroomForListDto()
                    {
                        ClassId = c.Id,
                        ClassName = c.ClassName,
                        IsMyClass = true
                    }).ToListAsync();


                    var MyJoinedClasses = await _context.UserInClassrooms
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                    foreach (UserInClassroom userinClass in MyJoinedClasses)
                    {

                        ClassroomForListDto classroom = await _context.Classrooms
                        .Where(c => c.Id == userinClass.ClassroomId)
                        .Select(c => new ClassroomForListDto()
                        {
                            ClassId = c.Id,
                            ClassName = c.ClassName,
                            IsMyClass = false
                        })
                        .FirstOrDefaultAsync();

                        MyClasses.Add(classroom);
                    }


                    return new ResultDto<IEnumerable<ClassroomForListDto>>()
                    {
                        Data = MyClasses,
                        Status = ServiceStatus.Success,
                    };

                }
                catch (Exception)
                {
                    return new ResultDto<IEnumerable<ClassroomForListDto>>()
                    {
                        Status = ServiceStatus.SystemError,
                    };
                }
            });
        }

        public async Task<bool> RemoveClassroom(Classroom classroom)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    _context.Classrooms.Remove(classroom);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            });
        }
    }
}
