using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Common.Interfaces.Auth;

public interface IAuthenticationService
{
    Task<bool> PasswordSignInAsync(string userName, string password);
}
