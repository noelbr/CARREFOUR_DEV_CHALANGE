using System;
using Microsoft.EntityFrameworkCore;
using service_account.Context;

namespace service_account_tests.UnitTests.Helpers
{
	public class MockDb : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
                .Options;

            return new ApplicationDbContext(options);
        }
    }

}

