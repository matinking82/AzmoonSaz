using AzmoonSaz.Domain.Entities.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Application.Services.Interfaces
{
    public interface ITestService
    {
        Task<bool> AddTest(Test test);
        Task<bool> DeleteTest(Test test);
        Task<bool> DeleteTestById(int testId);
        Task<bool> UpdateTest(Test test);
        Task<Test> GetTestById(int testId);
    }
}
