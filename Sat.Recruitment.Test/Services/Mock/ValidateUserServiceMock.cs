using Sat.Recruitment.Api.Models.Entities;
using Sat.Recruitment.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test.Services.Mock
{
    public class ValidateUserServiceMock : IValidateUserErrors
    {
        public async Task<EndPointResults> ValidateErrors(string name, string email, string address, string phone)
        {
            var results = new EndPointResults();

            if (name == null)
                //Validate if Name is null
                results.Errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                results.Errors = results.Errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                results.Errors = results.Errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                results.Errors = results.Errors + " The phone is required";

            return results;
        }
    }
}
