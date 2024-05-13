using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class APIUserClass : IdentityUser
    {
        public string Name { get; set; }
    }
}
