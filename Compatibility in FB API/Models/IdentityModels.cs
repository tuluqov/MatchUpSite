using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using MatchUp.Models.DBModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MatchUp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public int Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string PhotoUrl { get; set; }

        public IEquatable<Person> Persons { get; set; }

        public int? MatrixId { get; set; }
        public virtual PythagorianMatrix Matrix { get; set; }

        //public int? SecondaryAbilitiesId { get; set; }
        //public virtual SecondaryAbilities SecondaryAbilities { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Star> Stars { get; set; }

        public DbSet<Description> Descriptions { get; set; }

        public DbSet<PythagorianMatrix> PythagorianMatrices { get; set; }

       // public DbSet<SecondaryAbilities> SecondaryAbilitieses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}