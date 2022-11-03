using Microsoft.AspNetCore.Identity;

namespace Demo.Entity
{
    public class Role : IdentityRole
    {
        public Role(string RoleName) : base(RoleName) { }

        public Role() { }

        public DateTime DateCreated { get; set; }
        public virtual ICollection<UserRoleMap> UserRoleMaps { get; set; }
    }
}
