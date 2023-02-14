using System;
using service_account.Entities;

namespace service_account.Results
{
	public class TransactionResult
	{
        public string TransactionID { get; set; }

        public string Type { get; set; }

        public float Value { get; set; }

        public DateTime Created { get; set; }

        public static explicit operator TransactionResult(AccountTransaction obj)
        {
            TransactionResult convertedObject = new TransactionResult
            {
                TransactionID = obj.AccountTransactionID,
                Type = obj.TypeID,
                Value = obj.Value,
                Created = obj.Created
            };

            return convertedObject;
        }
    }
}

