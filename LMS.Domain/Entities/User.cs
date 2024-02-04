using LMS.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace LMS.Domain.Entities;

public class User : IdentityUser<long>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }    

    public virtual ICollection<UserRole> UserRoles { get; set; }
}