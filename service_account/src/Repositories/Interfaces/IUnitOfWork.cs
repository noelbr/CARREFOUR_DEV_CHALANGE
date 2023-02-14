using System;
namespace service_account.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IAccountRepository Account { get; }
        IAccountTransactionRepository Transaction { get; } 

        Task<int> Complete();
    }
}

