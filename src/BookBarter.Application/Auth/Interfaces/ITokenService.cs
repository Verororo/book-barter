using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Application.Auth.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(List<Claim> claims);
}
