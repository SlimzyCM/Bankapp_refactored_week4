using System;
using Bankapp_refactored_week4.Libraries;
using NUnit.Framework;

namespace Tests
{

    [TestFixture]
    public class WithdrawalTest
    {

        // Customer account created for the test
        private readonly Customer _customer = new Customer("jane", "micheal", "chidimicheal@yahoo.com", "jane1", "12345");


        [Test]
        public void ValidWithdrawal()
        {

            //create bank account object
            var cus1Acc = new BankAccount(_customer, AccountType.Savings, 2000);
            //set balance to initialBal
            var initialBal = cus1Acc.Balance;
            // call the withdrawal method
            cus1Acc.MakeWithdrawal(200, DateTime.Now, "Valid withdrawal test");  
            
            //get the balance after transaction
            var depositBalance = cus1Acc.Balance;
            
            //Test that the balance is debited
            Assert.That(depositBalance, Is.EqualTo(initialBal - 200));
        }


        [Test]
        public void SavingsAccountInvalidWithdrawal()
        {
            
            var cus1Acc = new BankAccount(_customer, AccountType.Savings, 150);

            //test that the withdrawal throws error for amount is more than available balance
            Assert.That( () => cus1Acc.MakeWithdrawal(2000, DateTime.Now, "Saving account test attempt to withdraw more than available balance"),
                Throws.TypeOf< InvalidOperationException > ()
            );

        }


        [Test]
        public void CurrentAccountInvalidWithdrawal()
        {
            
            var cus2Acc = new BankAccount(_customer, AccountType.Current, 1000);

            //test that the withdrawal throws error for amount is more than available balance
            Assert.That(() => cus2Acc.MakeWithdrawal(11000, DateTime.Now, "Current account test attempt to withdraw more than available balance"),
                Throws.TypeOf<InvalidOperationException>()
            );

            
        }

        [Test]
        public void InvalidAmountWithdrawal()
        {

            
            var cus2Acc = new BankAccount(_customer, AccountType.Current, 1000);
            
            //Test for invalid amount entered
            Assert.That(() => cus2Acc.MakeWithdrawal(0, DateTime.Now, "Amount of withdrawal must be positive"),
                                        Throws.TypeOf<ArgumentNullException>()
            );

        }
    }


       
    
}
