using Microsoft.AspNetCore.Identity;

namespace LMS.Domain.Entities
{
    public class Role : IdentityRole<long>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
