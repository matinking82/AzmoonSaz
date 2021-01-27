using AzmoonSaz.Application.Interfaces;
using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Domain.Entities.Test;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Application.Services
{
    public class TestService : ITestService
    {
        #region Dependency
        private readonly IDataBaseContext _context;

        public TestService(IDataBaseContext context)
        {
            _context = context;
        }
        #endregion






        public async Task<bool> AddTest(Test test)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await _context.Tests.AddAsync(test);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public Task<bool> DeleteTest(Test test)
        {
            return Task.Run(async () =>
            {
                try
                {
                    _context.Tests.Remove(test);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteTestById(int testId)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var test = await GetTestById(testId);

                    return await DeleteTest(test);
                    
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public Task<Test> GetTestById(int testId)
        {
            return Task.Run(async () =>
            {
                try
                {
                    return await _context.Tests.FindAsync(testId);
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        public Task<bool> UpdateTest(Test test)
        {
            return Task.Run(async () =>
            {
                try
                {
                    _context.Tests.Update(test);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}
