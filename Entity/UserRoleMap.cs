using Microsoft.AspNetCore.Identity;

namespace Demo.Entity
{
    public class UserRoleMap : IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
