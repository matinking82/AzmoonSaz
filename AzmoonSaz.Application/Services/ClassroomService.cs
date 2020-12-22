using AzmoonSaz.Application.Interfaces;
using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs;
using AzmoonSaz.Common.DTOs.Classroom;
using AzmoonSaz.Common.Enums;
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
        public ClassroomService(IDataBaseContext context)
        {
            _context = context;
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
    }
}
