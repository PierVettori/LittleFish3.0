using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LittleFish3._0.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<People> People { get; set; }

        public DbSet<Tour> Tour { get; set; }

        public DbSet<TourType> TourType { get; set; }

        public DbSet<Marketing> Marketing { get; set; }

        public DbSet<MarketingTypes> MarketingTypes { get; set; }

        public DbSet<MarketingOrder> MarketingOrders { get; set; }

        public DbSet<Guide> Guides { get; set; }

        public DbSet<GuideSales> GuideSales{ get; set; }

        public DbSet<GuideNumbers> GuideNumbers { get; set; }





        public ApplicationDbContext()
           : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DbSeed());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}