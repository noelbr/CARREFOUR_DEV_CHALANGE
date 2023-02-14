using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace service_account.Entities;

public class AccountTransaction
{
    [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
    public string AccountTransactionID { get; set; }

    public string TypeID { get; set; }

    public DateTime Created { get; set; }

    public float Value { get; set; }

    [Column("AccountID")]
    public string AccountID { get; set; }

    public Account Account
    {
        get;


    }
}

