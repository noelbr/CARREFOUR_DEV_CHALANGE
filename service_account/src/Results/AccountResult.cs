using System;
using service_account.Entities;

namespace service_account.Results
{
	public class AccountResult
	{
        public string AccountID { get; set; }

        public string Name { get; set; }

        public float Balance { get; set; }

        public static explicit operator AccountResult(Account obj)
        {
            AccountResult convertedObject = new AccountResult
            {
                AccountID = obj.AccountID,
                Name = obj.Name,
                Balance = obj.Balance
            };

            return convertedObject;
        }
    }
}

