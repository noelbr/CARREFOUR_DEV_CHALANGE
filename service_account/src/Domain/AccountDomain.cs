using System;
using Azure.Core;
using Microsoft.Identity.Client;
using service_account.CustomResults;
using service_account.Entities;
using service_account.Repositories;
using service_account.Repositories.Interfaces;

namespace service_account.Domain
{
	public interface IAccountDomain { };

    public class AccountDomain : IAccountDomain
    {
        private readonly UnitOfWork _unitOfWork;

        public AccountDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        public async Task<Account> Create(string name) {

            var account = new Account { Name = name, Balance = 0 };
            _unitOfWork.Account.Add(account);

            await _unitOfWork.Complete();

            return account;
        }

        public async Task<AccountTransaction> Withdrawal(string accountID, float value)
        {
            var account = _unitOfWork.Account.SingleOrDefault(x => x.AccountID == accountID);
            if (account == null)
            {
                throw new Exception("Account not found");

            }

            if (account.Balance < value)
            {
                throw new Exception("out of credit");

            }

            var transaction = _unitOfWork.Transaction.Withdrawal(accountID, value);

            account.Balance -= value;

            _unitOfWork.Account.Update(account);

            await _unitOfWork.Complete();  

            return transaction;
        } 

        public async Task<AccountTransaction> Deposit(string accountID, float value)
        {
            var account = _unitOfWork.Account.SingleOrDefault(x => x.AccountID == accountID);
            if (account == null)
            {
                throw new Exception("Account not found");

            }

            var transaction = _unitOfWork.Transaction.Deposit(accountID, value);

            account.Balance += value;

            _unitOfWork.Account.Update(account);

            await _unitOfWork.Complete();

            return transaction;
        }
    }
}

