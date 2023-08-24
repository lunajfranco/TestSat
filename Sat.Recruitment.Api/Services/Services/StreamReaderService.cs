using Sat.Recruitment.Api.Data.StreamReader;
using Sat.Recruitment.Api.Models.Entities;
using Sat.Recruitment.Api.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Services
{
    public class StreamReaderService : IStreamReaderService
    {
        public async Task<List<User>> ReadUserFile()
        {
            var _users = new List<User>();
            var reader = UserFile.ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var readerUser = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(readerUser);
            }
            reader.Close();
            return _users;
        }
    }
}
