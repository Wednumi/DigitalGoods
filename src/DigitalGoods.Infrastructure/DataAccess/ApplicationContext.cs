using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DigitalGoods.Core.Entities;
using DigitalGoods.Infrastructure.DataAccess.Configurations;

namespace DigitalGoods.Infrastructure.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ActivationCode> ActivationCodes { get; set; } = null!;

        public DbSet<BankAccount> BankAccounts { get; set; } = null!;

        public DbSet<BankAccountType> BankAccountTypes { get; set; } = null!;

        public DbSet<Media> Medias { get; set; } = null!;

        public DbSet<Offer> Offers { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Source> Sources { get; set; } = null!;

        public DbSet<Tag> Tags { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<OfferChange> OfferChanges { get; set; } = null!;

        public DbSet<ReceiveMethod> ReceiveMethods { get; set; } = null!;

        public DbSet<Comment> Comment { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=DESKTOP-98E3VKN\SQLEXPRESS;database=DigitalGoodsTest;trusted_connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
