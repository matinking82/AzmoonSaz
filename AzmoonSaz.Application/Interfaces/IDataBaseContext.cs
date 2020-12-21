using AzmoonSaz.Domain.Entities.Classroom;
using AzmoonSaz.Domain.Entities.Test;
using AzmoonSaz.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Application.Interfaces
{
    public interface IDataBaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserInClassroom> UserInClassrooms { get; set; }
        DbSet<Classroom> Classrooms { get; set; }
        DbSet<Test> Tests { get; set; }
        DbSet<Question> Questions { get; set; }
    }
}
