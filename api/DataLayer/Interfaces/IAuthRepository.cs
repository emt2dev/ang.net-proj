using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Models.PII;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IAuthRepository
    {
        public Task<APIUserClass> GetUserFromJwt(string BearerString); 
        public Task<string> CreateJwt();
        public Task<string> CreateRefreshToken();
        public Task<TokensDTO> RefreshTokens(TokensDTO DTO);
        public Task<string> ReadUserId(string Jwt);

        public Task<InitialTokensDTO> Login(LoginDTO DTO);
    }
}
