using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models.Entities;
using Sat.Recruitment.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        private readonly IUserService _userService;
        private readonly IValidateUserErrors _validateUserErrors;

        public UsersController(IUserService userService, IValidateUserErrors validateUserErrors)
        {
            _userService = userService;
            _validateUserErrors = validateUserErrors;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<EndPointResults> CreateUser(User user)
        {
            var errors = await _validateUserErrors.ValidateErrors(user.Name, user.Email, user.Address, user.Phone);

            if (errors.Errors != null && errors.Errors != "") 
            {
                errors.IsSuccess = false;
                return errors;
            }

            var results = await _userService.CreateUser(user);
            return results;
        }
    }
}
