using System;
using service_account.Context;
using service_account.Entities;
using service_account.Repositories.Interfaces;

namespace service_account.Repositories
{
	public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {

        }



    }
}

