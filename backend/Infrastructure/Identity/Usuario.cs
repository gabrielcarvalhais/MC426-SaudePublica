using Microsoft.AspNetCore.Identity;

namespace MC426_Backend.Infrastructure.Identity
{
    public class Usuario : IdentityUser
    {
        public string? Name { get; set; }
    }
}
