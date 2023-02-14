using System;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using service_account.Context;
using service_account.Entities;
using service_account.Repositories.Interfaces;

namespace service_account.Repositories
{
	public class AccountTransactionRepository : Repository<AccountTransaction>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public AccountTransaction Withdrawal(string accountID, float value)
        {

            var balance = GetBalance(accountID);
            if (balance < value) {
                throw new Exception("out of credit");
            }


            var new_transaction = new AccountTransaction
            {
                TypeID = "D",
                Value = value,
                Created = DateTime.Now,
                AccountID = accountID
            };

            _entities.Add(new_transaction);

            return new_transaction;
        }

        public AccountTransaction Deposit(string accountID, float value)
        { 
            var new_transaction = new AccountTransaction
            {
                TypeID = "C",
                Value = value,
                Created = DateTime.Now,
                AccountID = accountID
            };

            _entities.Add(new_transaction);

            return new_transaction;
        }
        public float GetBalance(string accountID)
        {

            var balance = _entities.Where(x => x.AccountID == accountID).Sum(x => x.TypeID == "D" ? x.Value * -1 : x.Value);

            return balance;
        }
    }
}

