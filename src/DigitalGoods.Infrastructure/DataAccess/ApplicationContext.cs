using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DigitalGoods.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using DigitalGoods.Core.DbMethods;
=======
using DigitalGoods.Core.Interfaces;
>>>>>>> 18a5c9aec697eec17ab9ada5a9c7448c6010cd2c

namespace DigitalGoods.Infrastructure.DataAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Core.Entities.ActivationCode> ActivationCodes { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Media> Medias { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Core.Entities.Order> Orders { get; set; }

        public DbSet<Core.Entities.Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<OfferChange> OfferChanges { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<PaymentRecord> PaymentRecords { get; set; }

        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DigitalGoods;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
<<<<<<< HEAD
            modelBuilder.HasDbFunction(() => IDbFunctions.FinalPrice(default, default))
                .HasName("f_calculate_final_price");
            modelBuilder.HasDbFunction(() => IDbFunctions.WeeklySalesPerDay(default))
               .HasName("f_weekly_sales_per_day");
            modelBuilder.HasDbFunction(() => IDbFunctions.SoldPeriodChange(default, default))
               .HasName("f_period_change");
=======
            modelBuilder.HasDbFunction(() => IDataBaseFunctions.FinalPrice(default, default))
                .HasName("f_calculate_final_price");
>>>>>>> 18a5c9aec697eec17ab9ada5a9c7448c6010cd2c
        }
    }
}
