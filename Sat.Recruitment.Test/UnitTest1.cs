using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models.Entities;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.Services.Services;
using Sat.Recruitment.Test.Services.Builders;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private readonly UsersController _userController;
        private readonly IUserService _userService;
        private readonly IValidateUserErrors _validateUserErrors;
        private readonly IStreamReaderService _streamReaderService;
        private readonly UserMockBuilder _userMockBuilder;
        public UnitTest1()
        {
            _userMockBuilder = new UserMockBuilder();
            _streamReaderService = new StreamReaderService();
            _userService = new UserService(_streamReaderService);
            _validateUserErrors = new ValidateUserErrors();
            _userController = new UsersController(_userService, _validateUserErrors);
        }
        [Fact]
        public void Test1()
        {
            var user = _userMockBuilder.BuildUserMock()[0];

            var result = _userController.CreateUser(user).Result;

            Xunit.Assert.Equal(true, result.IsSuccess);
            Xunit.Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var user = _userMockBuilder.BuildUserMock()[1];

            var result = _userController.CreateUser(user).Result;

            Xunit.Assert.Equal(false, result.IsSuccess);
            Xunit.Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
