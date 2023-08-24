using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models.Entities;
using Sat.Recruitment.Api.Services.Interfaces;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IStreamReaderService _streamReaderService;

        public UserService(IStreamReaderService streamReaderService)
        {
            _streamReaderService = streamReaderService;
        }
        #region Public Methods
        public async Task<EndPointResults> CreateUser(User user)
        {
            var newUser = await AsingUser(user);

            newUser = await AdjustingMoneyByUserType(user, newUser);
            
            var _users = await _streamReaderService.ReadUserFile();

            newUser.Email = await NormalizedEmial(newUser.Email);

            try
            {
                var isDuplicated = VerifyDuplicated(_users, user);

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return new EndPointResults()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return new EndPointResults()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return new EndPointResults()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }
        }
        #endregion
        #region Private Methods

        private async Task<string> NormalizedEmial(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        private Task<User> AsingUser(User user)
        {
            return Task.FromResult(new User
            {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                UserType = user.UserType,
                Money = user.Money
            });
        }

        private bool VerifyDuplicated(List<User> _readerUsers, User newUser)
        {
            var isDuplicated = false;
            foreach (var readerUser in _readerUsers)
            {
                if (readerUser.Email == newUser.Email
                    ||
                    readerUser.Phone == newUser.Phone)
                {
                    isDuplicated = true;
                }
                else if (readerUser.Name == newUser.Name)
                {
                    if (readerUser.Address == newUser.Address)
                    {
                        isDuplicated = true;
                        throw new Exception("User is duplicated");
                    }

                }
            }
            return isDuplicated;
        }

        private async Task<User> AdjustingMoneyByUserType(User user, User newUser)
        {
            switch (newUser.UserType)
            {
                case "Normal":
                    if (user.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = user.Money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                    if (user.Money < 100)
                    {
                        if (user.Money > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = user.Money * percentage;
                            newUser.Money = newUser.Money + gif;
                        }
                    }
                    break;
                case "SuperUser":
                    if (user.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = user.Money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                    break;
                case "Premiun":
                    if (user.Money > 100)
                    {
                        var gif = user.Money * 2;
                        newUser.Money = newUser.Money + gif;
                    }
                    break;
            }
            return newUser;
            return newUser;
        }
        #endregion
    }
}
