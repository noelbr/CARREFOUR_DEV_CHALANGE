using System;
using service_account.Context;
using service_account.Repositories.Interfaces;

namespace service_account.Repositories
{
	public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Account = new AccountRepository(_context);
            Transaction = new AccountTransactionRepository(_context); 

        }

        public IAccountRepository Account { get; private set; }

        public IAccountTransactionRepository Transaction { get; private set; }
 
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

