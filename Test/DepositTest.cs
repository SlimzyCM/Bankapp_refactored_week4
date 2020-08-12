using System;
using Bankapp_refactored_week4.Libraries;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DepositTest
    {
        //Customer account created for the test
        private readonly Customer _customer = new Customer("jane", "micheal", "chidimicheal@yahoo.com", "jane1", "12345");
        

        [Test]
        public void ValidAmountDeposit()
        {
            // Create an account for the customer
            var cusAccount = new BankAccount(_customer, AccountType.Savings, 1500);
            
            //set balance to initialBal
            var initialBal = cusAccount.Balance;

            //Call the make deposit method
            cusAccount.MakeDeposit(100, DateTime.Now, "Attempt to make a valid deposit ");
            // get balance after deposit
            var depositBalance = cusAccount.Balance;
            
            //Test that the amount deposit is added
            Assert.That(depositBalance, Is.EqualTo(initialBal + 100));

        }


        [Test]
        public void InvalidAmountDeposit()
        {
            // Create an account for the customer
            var cusAccount = new BankAccount(_customer, AccountType.Savings, 1000);
            
            //test for invalid amount entered
            Assert.That( () => cusAccount.MakeDeposit(-100, DateTime.Now, "Attempt to deposit Invalid amount"),
                                Throws.TypeOf<ArgumentOutOfRangeException>()

            );

        }
    }
}
