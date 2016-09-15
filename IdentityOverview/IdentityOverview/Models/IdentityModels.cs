using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public string Address { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    // Use a sensible display name for views:
    //    [Display(Name = "Postal Code")]
    //    public string PostalCode { get; set; }

    //    // Concatenate the address info for display in tables and such:
    //    public string DisplayAddress
    //    {
    //        get
    //        {
    //            string dspAddress =
    //                string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
    //            string dspCity =
    //                string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
    //            string dspState =
    //                string.IsNullOrWhiteSpace(this.State) ? "" : this.State;
    //            string dspPostalCode =
    //                string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;

    //            return string
    //                .Format("{0} {1} {2} {3}", dspAddress, dspCity, dspState, dspPostalCode);
    //        }
    //    }
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager
    //            .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    //public class ApplicationRole : IdentityRole
    //{
    //    public ApplicationRole() : base() { }

    //    public ApplicationRole(string name) : base(name) { }


    //    public string Description { get; set; }

    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    static ApplicationDbContext()
    //    {
    //        // Set the database intializer which is run once during application start
    //        // This seeds the database with admin user credentials and admin role
    //        Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}


    // Using integer key
    public class ApplicationUserLogin : IdentityUserLogin<int> { }

    public class ApplicationUserClaim : IdentityUserClaim<int> { }

    public class ApplicationUserRole : IdentityUserRole<int> { }

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>, IRole<int>
    {
        public string Description { get; set; }
        public ApplicationRole()
        {

        }

        public ApplicationRole(string name)
            : this()
        {
            Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            Description = description;
        }
    }

    //public class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser, IUser<string>

    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<int>
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        // Use a sensible display name for views:
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        // Concatenate the address info for display in tables and such:
        public string DisplayAddress
        {
            get
            {
                string dspAddress =
                    string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string dspCity =
                    string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspState =
                    string.IsNullOrWhiteSpace(this.State) ? "" : this.State;
                string dspPostalCode =
                    string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;

                return string
                    .Format("{0} {1} {2} {3}", dspAddress, dspCity, dspState, dspPostalCode);
            }
        }

        public async Task<ClaimsIdentity>
        GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    //public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


    // public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> 
    //: IUserLoginStore<TUser, TKey>, IUserClaimStore<TUser, TKey>, IUserRoleStore<TUser, TKey>,
    // IUserPasswordStore<TUser, TKey>, IUserSecurityStampStore<TUser, TKey>, 
    //IQueryableUserStore<TUser, TKey>, IUserEmailStore<TUser, TKey>, IUserPhoneNumberStore<TUser, TKey>, 
    //IUserTwoFactorStore<TUser, TKey>, IUserLockoutStore<TUser, TKey>, IUserStore<TUser, TKey>, IDisposable
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>,
        IUserStore<ApplicationUser, int>, IDisposable
    {
        public ApplicationUserStore()
            : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }
        public ApplicationUserStore(DbContext context)
            : base(context)
        {

        }
    }


    public class ApplicationRoleStore : RoleStore<ApplicationRole, int,ApplicationUserRole>,
        IQueryableRoleStore<ApplicationRole,int>,
        IRoleStore<ApplicationRole,int>, IDisposable
    {
        public ApplicationRoleStore()
            :base(new IdentityDbContext())
        {

        }

        public ApplicationRoleStore(DbContext context)
            :base(context)
        {

        }
    }

}