using ProductApp.Core.Models;
using ProductApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Service.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenService tokenService;

        public AuthService(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new();

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Username == "ahmet" && request.Password == "12345")
            {
                var generatedTokenInformation = await tokenService.GenerateToken(new GenerateTokenRequest { Username = request.Username });

                response.AuthenticateResult = true;
                response.AuthToken = generatedTokenInformation.Token;
                response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;
            }

            return response;
        }
    }
}
