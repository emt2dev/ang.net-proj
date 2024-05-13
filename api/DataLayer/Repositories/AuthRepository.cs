using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configs;
        private string _tokenProvider = "AuthReadyAPI";
        private string _refreshToken = "MadeByDavidDuron";
        private APIUserClass _user;
        private UserManager<APIUserClass> _userManager;

        public AuthRepository(IMapper mapper, UserManager<APIUserClass> userManager, IConfiguration configs)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configs = configs;
        }

        public async Task<APIUserClass> GetUserFromJwt(string BearerString)
        {
            string Jwt = BearerString.Replace("Bearer ", "");
            string UserId = await ReadUserId(Jwt);
            APIUserClass User = await _userManager.FindByIdAsync(UserId);
            
            if (User is not null) return User;
            return null;

        }


        public async Task<string> CreateJwt()
        {
            var SecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));
            var userCreds = new SigningCredentials(SecKey, SecurityAlgorithms.HmacSha256);
            var userRoles = await _userManager.GetRolesAsync(_user);
            var userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);
            var TokenDetails = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(userClaims)
            .Union(userRoleClaims);

            var newToken = new JwtSecurityToken(
                issuer: _configs["JwtSettings:Issuer"],
                audience: _configs["JwtSettings:Audience"],
                claims: TokenDetails,
                expires: DateTime.Now
                    .AddMinutes(Convert.ToDouble(
                    _configs["JwtSettings:DurationInMinutes"])),

                signingCredentials: userCreds
                );

            return new JwtSecurityTokenHandler().WriteToken(newToken);
        }
        public async Task<string> CreateRefreshToken()
        {
            
            await _userManager.RemoveAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken);
            
            var NewToken = await _userManager.GenerateUserTokenAsync(_user, _tokenProvider, _refreshToken);
            
            _ = await _userManager.SetAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken, NewToken);

            return NewToken;
        }
        public async Task<TokensDTO> RefreshTokens(TokensDTO DTO)
        {
            var SecHandler = new JwtSecurityTokenHandler();
            var Content = SecHandler.ReadJwtToken(DTO.Token);
            string UserId = Content.Claims.ToList().FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            _user = await _userManager.FindByIdAsync(UserId);

            if (_user is null) return null;

            var validatedRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _tokenProvider, _refreshToken, DTO.RefreshToken);
            if (!validatedRefreshToken) return null;

            var NewJwt = await CreateJwt();

            return new TokensDTO
            {
                Token = NewJwt,
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<string> ReadUserId(string Jwt)
        {
            var SecHandler = new JwtSecurityTokenHandler();
            var Content = SecHandler.ReadJwtToken(Jwt);
            string UserId = Content.Claims.ToList().FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            _user = await _userManager.FindByIdAsync(UserId);
            if (_user is null) return string.Empty;
            return UserId;
        }

        public async Task<InitialTokensDTO> Login(LoginDTO DTO)
        {
            _user = await _userManager.FindByEmailAsync(DTO.Email);
            if(_user is null) return null;

            bool ValidPassword = await _userManager.CheckPasswordAsync(_user, DTO.Password);
            if (!ValidPassword) return null;

            var This = await CreateJwt();
            return new InitialTokensDTO
            {
                Token = This,
                RefreshToken = await CreateRefreshToken(),
                User = _mapper.Map<APIUserDTO>(_user)
            };
        }
    }
}
