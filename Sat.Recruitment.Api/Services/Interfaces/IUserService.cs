using Sat.Recruitment.Api.Models.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<EndPointResults> CreateUser(User user);
    }
}
