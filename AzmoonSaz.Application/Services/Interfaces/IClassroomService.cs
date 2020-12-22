using AzmoonSaz.Common.DTOs;
using AzmoonSaz.Common.DTOs.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Application.Services.Interfaces
{
    public interface IClassroomService
    {
        Task<ResultDto<IEnumerable<ClassroomForListDto>>> GetUsersClassroomsByUserId(int userId);
    }
}
