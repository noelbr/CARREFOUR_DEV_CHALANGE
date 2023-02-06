using System;
using service_account.Entities;

namespace service_account.Repositories.Interfaces
{
	public interface IAccountTransactionRepository : IRepository<AccountTransaction>
    {
        AccountTransaction Withdrawal(string accountID, float value);
        AccountTransaction Deposit(string accountID, float value);
        float GetBalance(string accountID);

    }
}

