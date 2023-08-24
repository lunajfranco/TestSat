using Sat.Recruitment.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Test.Services.Builders
{
    public class UserMockBuilder
    {
        public List<User> BuildUserMock()
        {
            return new List<User>() { new User() { 
                Name = "Mike", 
                Email = "mike@gmail.com", 
                Address = "Av. Juan G", 
                Phone = "+349 1122354215", 
                Money = 124, 
                UserType = "Normal" }, 
                new User() { 
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Money = 124,
                UserType = "Normal"
                }};
        }
    }
}
