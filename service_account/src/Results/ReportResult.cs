using System;
using Microsoft.Identity.Client;
using service_account.Entities;

namespace service_account.Results
{
	public class ReportResult
	{
		public List<TransactionResult> Statement { get; set; }
		public float Balance { get; set; }

        public static explicit operator ReportResult(List<AccountTransaction> obj)
        {
            ReportResult convertedObject = new ReportResult
            {
                Statement = obj.Select(x => (TransactionResult)x).ToList(),
                Balance = obj.Sum(x => x.TypeID == "D" ? x.Value * -1 : x.Value)

            };
  
            return convertedObject;
        }
    }
}

