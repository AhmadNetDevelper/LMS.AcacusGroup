using LMS.Domain.Common;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS.Persistence.Context;

public class DataContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>,
                                     IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public int UserId => int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var hasher = new PasswordHasher<User>();

        builder.Entity<UserRole>(
                userRole =>
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                    userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(fur => fur.RoleId)
                    .IsRequired();

                    userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(fur => fur.UserId)
                    .IsRequired();
                }
            );

        //Create New Roles
        builder.Entity<Role>().ToTable("Roles").HasData(new List<Role>
            {
                new Role {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Role {
                    Id = 2,
                    Name = "Librarian",
                    NormalizedName = "LIBRARIAN"
                },
                new Role {
                    Id = 3,
                    Name = "Member",
                    NormalizedName = "MEMBER"
                }
            });

        //Create New Users
        builder.Entity<User>().ToTable("Users").HasData(
        new User
        {
            Id = 1, // primary key
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            PhoneNumber = "123456789",
            Email = "Admin@a.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            PasswordHash = hasher.HashPassword(null, "LMS@#12345"),
            IsActive = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        },
        new User
        {
            Id = 2, // primary key
            UserName = "Librarian",
            NormalizedUserName = "LIBRARIAN",
            PhoneNumber = "12345678",
            Email = "Librarian@Librarian.com",
            NormalizedEmail = "LIBRARIAN@LIBRARIAN.COM",
            PasswordHash = hasher.HashPassword(null, "LMS@#12345"),
            IsActive = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        },
        new User
        {
            Id = 3, // primary key
            UserName = "Member",
            NormalizedUserName = "MEMBER",
            PhoneNumber = "1234567",
            Email = "Member@member.com",
            NormalizedEmail = "MEMBER@MEMBER.COM",
            PasswordHash = hasher.HashPassword(null, "LMS@#12345"),
            IsActive = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        });

        //Assign role to User
        builder.Entity<UserRole>().ToTable("UserRoles").HasData(
        new UserRole
        {
            RoleId = 1,
            UserId = 1
        },
        new UserRole
        {
            RoleId = 2,
            UserId = 2
        },
        new UserRole
        {
            RoleId = 3,
            UserId = 3
        });

        builder.Entity<NotificationType>().ToTable("NotificationType").HasData(new List<NotificationType>
        {
            new NotificationType {
                    CreatedBy = 1,
                    Id = 10,
                    Name = "BookOrderNeedApproval",
                },
            new NotificationType {
                    CreatedBy = 1,
                    Id = 20,
                    Name = "ApprovedBookOrder",
                },
        });

        //Rename All Tables Identity
        builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
        builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
        builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");
    }

    public override int SaveChanges()
    {
        return this.SaveChangesAsync(true).Result;
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = UserId;
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = UserId;
                    entry.Entity.UpdatedDate = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<NotificationType> NotificationType { get; set; }
    public DbSet<Notifications> Notifications { get; set; }
    public DbSet<NotificationsDetails> NotificationsDetails { get; set; }
}


public class ApplicationUser : IdentityUser<long>
{
}

public class ApplicationRole : IdentityRole<long>
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}