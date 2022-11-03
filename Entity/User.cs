using Microsoft.AspNetCore.Identity;

namespace Demo.Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<UserRoleMap> UserRoleMaps { get; set; }

        public User() { }
    }
}
