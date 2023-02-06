using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using service_account.Entities;

namespace service_account.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<AccountTransaction> Transaction { get; set; }

    }
}

