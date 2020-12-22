using AzmoonSaz.Common.DTOs;
using AzmoonSaz.Common.DTOs.Classroom;
using AzmoonSaz.Domain.Entities.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Application.Services.Interfaces
{
    public interface IClassroomService
    {
        Task<bool> AddClassroom(Classroom classroom);
        Task<bool> RemoveClassroom(Classroom classroom);

        Task<ResultDto<IEnumerable<ClassroomForListDto>>> GetUsersClassroomsByUserId(int userId);

        Task<ResultDto> CreateClassroomByUser(RequestCreateClassroomByUserDto request);

        Task<ResultDto> DeleteClassroomByClassId(int classId);

        Task<Classroom> GetClassroomByClassId(int classId);

        Task<ResultDto> EditClassroomByUser(RequestEditClassroomByUserDto request);

        Task<ResultDto<RequestEditClassroomByUserDto>> GetClassroomDetailForEditByUser(int classId);

        Task<ResultDto<IEnumerable<StudentsListForClassDto>>> GetStudentsListForClassByClassId(int classId);

    }

}
