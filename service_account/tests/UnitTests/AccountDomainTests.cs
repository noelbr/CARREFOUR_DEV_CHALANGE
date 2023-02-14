using System;
using service_account_tests.UnitTests.Helpers;
using service_account.Domain;
using service_account.Repositories;

namespace service_account_tests.UnitTests
{
	public class AccountDomainTests
	{
        public readonly AccountDomain accountDomain;
        public readonly UnitOfWork unitOfWork;

        public AccountDomainTests()
        {

            var context = new MockDb().CreateDbContext();
            unitOfWork = new UnitOfWork(context);
            accountDomain = new AccountDomain(unitOfWork);
        }


        [Fact]
        public async void CreateAccount()
        { 
            var account = await accountDomain.Create("ACCOUNT_TESTE");

            Assert.Equal("ACCOUNT_TESTE", account.Name);
            Assert.Equal(0, account.Balance);

        }

        [Fact]
        public async void AccountWithdrawal()
        {
            var account = await accountDomain.Create("ACCOUNT_WITHDRAWAL");

            var desposit = await accountDomain.Deposit(account.AccountID,100);

            Assert.Equal("C", desposit.TypeID);
            Assert.Equal(100, desposit.Value);


            var withdrawal = await accountDomain.Withdrawal(account.AccountID, 50);

            Assert.Equal("D", withdrawal.TypeID);
            Assert.Equal(50, withdrawal.Value);

            var account_validation = unitOfWork.Account.SingleOrDefault(x => x.AccountID == account.AccountID);
            Assert.Equal(50, account_validation.Balance);

        }

        [Fact]
        public async void AccountDeposit()
        {
            var account = await accountDomain.Create("ACCOUNT_DEPOSIT");

            var desposit = await accountDomain.Deposit(account.AccountID, 100);

            Assert.Equal("C", desposit.TypeID);
            Assert.Equal(100, desposit.Value);

            var account_validation = unitOfWork.Account.SingleOrDefault(x => x.AccountID == account.AccountID);
            Assert.Equal(100, account_validation.Balance);
        }
    }
}

