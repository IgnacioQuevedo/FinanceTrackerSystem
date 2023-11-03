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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MonetaryAccount> MonetaryAccounts { get; set; }
        public DbSet<CreditCardAccount> CreditCardAccounts { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<ExchangeHistory> ExchangeHistories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonetaryAccount>().ToTable("MonetaryAccounts");
            modelBuilder.Entity<CreditCardAccount>().ToTable("CreditCardAccounts");
        }
    }
}