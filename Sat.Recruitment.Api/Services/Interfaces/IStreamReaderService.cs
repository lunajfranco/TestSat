using Sat.Recruitment.Api.Models.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IStreamReaderService
    {
        Task<List<User>> ReadUserFile();
    }
}
