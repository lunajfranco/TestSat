using Sat.Recruitment.Api.Models.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IValidateUserErrors
    {
        Task<EndPointResults> ValidateErrors(string name, string email, string address, string phone);
    }
}
