using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data.StreamReader
{
    public class UserFile
    {
        public static System.IO.StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            System.IO.StreamReader reader = new System.IO.StreamReader(fileStream);
            return reader;
        }
    }
}
