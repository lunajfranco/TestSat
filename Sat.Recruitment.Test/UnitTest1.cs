using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models.Entities;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.Services.Services;
using Sat.Recruitment.Test.Services.Mock;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IStreamReaderService streamReaderService = new StreamReaderService();
            IUserService userService = new UserServiceMock(streamReaderService);
            IValidateUserErrors validateUserService = new ValidateUserServiceMock();

            var userController = new UsersController(userService, validateUserService);

            var user = new User() { Name = "Mike", Email = "mike@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", Money = 124, UserType = "Normal" };
            var result = userController.CreateUser(user).Result;


            Xunit.Assert.Equal(true, result.IsSuccess);
            Xunit.Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            IStreamReaderService streamReaderService = new StreamReaderService();
            IUserService userService = new UserServiceMock(streamReaderService);
            IValidateUserErrors validateUserService = new ValidateUserServiceMock();
            var userController = new UsersController(userService, validateUserService);
            var user = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Money = 124,
                UserType = "Normal"
            };
            var result = userController.CreateUser(user).Result;


            Xunit.Assert.Equal(false, result.IsSuccess);
            Xunit.Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
