using System.Data.Common;
using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.Goal_Components;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.User_Components;
using BusinessLogic.Transaction_Components;


namespace DataManagers
{
    public class SqlContext : DbContext
    {
        public DbSet<User> Users{ get; set; }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonetaryAccount>().ToTable("MonetaryAccounts");
            modelBuilder.Entity<CreditCardAccount>().ToTable("CreditCardAccounts");
        }
    }
}