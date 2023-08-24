using EasyMapper;
using KUSYS.Business.Helpers;
using KUSYS.Core.Models.Authorization;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KUSYS.Business.Handlers.Auth.Commands
{
    public class LoginCommand : IRequest<TokenResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserTokenRepository _userTokenRepository;
            private readonly TokenSettings _tokenSettings;

            public LoginCommandHandler(IUserRepository userRepository, IUserTokenRepository userTokenRepository, IOptions<TokenSettings> tokenSettings)
            {
                _userRepository = userRepository;
                _userTokenRepository = userTokenRepository;
                _tokenSettings = tokenSettings.Value;
            }

            public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.Include(i => i.Role).FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
                if (user == null || !HashingHelper.CheckPassword(user.ToMap<UserAuth>(), request.Password))
                {
                    return default;
                }
                var expireDate = DateTime.UtcNow.AddMinutes(_tokenSettings.AccessTokenExpiration);
                var token = TokenHelper.CreateToken(user.ToMap<UserAuth>(), _tokenSettings, expireDate);
                _userTokenRepository.Add(new UserToken
                {
                    Token = token,
                    TokenExpireDate = expireDate,
                    UserId = user.Id,
                    User = user
                });
                await _userTokenRepository.SaveChangesAsync();
                return new TokenResponse
                {
                    User = user.ToMap<UserResponse>(),
                    Token = token,
                    ExpireDate = expireDate
                };
            }
        }
    }
}
