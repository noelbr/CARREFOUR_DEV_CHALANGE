using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace service_account.Entities
{
	public class Account
	{
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string AccountID { get; set; }

        public string Name { get; set; }

        public float Balance { get; set; }


    }
}

