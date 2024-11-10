using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Commons;

using ErrorOr;

using MediatR;

namespace Connexa.Application.Auth.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ErrorOr<AccessTokenResponse>>
    {

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _usersRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserQueryHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository usersRepository, IJwtService jwtService, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _usersRepository = usersRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        public async Task<ErrorOr<AccessTokenResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {

            var user = await _usersRepository.GetByEmailAsync(request.Email);

            if (user is null)
            {
                return Error.Validation("Invalid credentials");
            }

            if (!_passwordHasher.IsCorrectPassword(request.Password, user.Password))
            {
                return Error.Validation("Invalid credentials");
            }

            var token = _jwtService.GetAccessToken(user);
            var refreshToken = _jwtService.GetRefreshToken(user.Id);

            await _refreshTokenRepository.AddOrUpdateRefreshTokenAsync(refreshToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var accessTokenResponse = new AccessTokenResponse(token, refreshToken.Token);

            return accessTokenResponse;
        }
    }
}