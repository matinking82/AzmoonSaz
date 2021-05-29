using AzmoonSaz.Application.Interfaces;
using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs;
using AzmoonSaz.Common.DTOs.Test;
using AzmoonSaz.Common.Enums;
using AzmoonSaz.Domain.Entities.Classroom;
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

        public async Task<bool> DeleteTest(Test test)
        {
            return await Task.Run(async () =>
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

        public async Task<Test> GetTestById(int testId)
        {
            return await Task.Run(async () =>
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

        public async Task<ResultDto<List<TestsClassForUserInSiteDto>>> GetTestsForUserByUserId(int userId)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    List<TestsClassForUserInSiteDto> Data = new List<TestsClassForUserInSiteDto>();
                    #region MyClasses
                    var MyClassrooms = await _context.Classrooms
                    .Where(c => c.CreatorId == userId)
                    .ToListAsync();


                    foreach (var Classroom in MyClassrooms)
                    {
                        TestsClassForUserInSiteDto newData = await GetClassDataByClassAsync(Classroom, true);
                        Data.Add(newData);
                    }

                    #endregion

                    #region Classes

                    var userInClassrooms = await _context.UserInClassrooms
                    .Where(u => u.UserId == userId)
                    .ToListAsync();


                    foreach (var item in userInClassrooms)
                    {

                        var Classroom = await _context.Classrooms.FindAsync(item.ClassroomId);
                        TestsClassForUserInSiteDto newData = await GetClassDataByClassAsync(Classroom, false);

                        Data.Add(newData);
                    }
                    #endregion

                    #region GetClassData
                    async Task<TestsClassForUserInSiteDto> GetClassDataByClassAsync(Classroom Classroom, bool isMyClass)
                    {
                        TestsClassForUserInSiteDto newData = new TestsClassForUserInSiteDto()
                        {
                            ClassId = Classroom.Id,
                            ClassName = Classroom.ClassName,
                            IsMyClass = isMyClass,
                            Tests = new List<TestsListDto>()
                        };

                        var tests = await _context.Tests
                        .Where(t => t.ClassId == Classroom.Id)
                        .ToListAsync();

                        foreach (var test in tests)
                        {
                            TestsListDto newTest = new TestsListDto()
                            {
                                Description = test.Description,
                                Id = test.Id,
                                Title = test.Title
                            };
                            var questionsCount = await _context.Questions
                            .Where(q => q.TestId == test.Id)
                            .CountAsync();

                            newTest.QuestionsCount = questionsCount;
                            newData.Tests.Add(newTest);
                        }

                        return newData;
                    }
                    #endregion


                    return new ResultDto<List<TestsClassForUserInSiteDto>>()
                    {
                        Data = Data,
                        Status = ServiceStatus.Success
                    };
                }
                catch (Exception)
                {
                    return new ResultDto<List<TestsClassForUserInSiteDto>>()
                    {
                        Status = ServiceStatus.SystemError
                    };
                }
            });
        }

        public async Task<bool> UpdateTest(Test test)
        {
            return await Task.Run(async () =>
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
