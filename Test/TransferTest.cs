using System;
using Bankapp_refactored_week4.Libraries;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TransferTest
    {

        //Customer account created for the test
        private readonly Customer _customer = new Customer("Jane", "Micheal", "janemicheal@yahoo.com", "jane1", "12345");
        private readonly Customer _anotherCustomer = new Customer("Chidi", "Okobia", "chidimicheal@gmail.com", "chidi1", "45467");

        [Test]
        public void ValidTransferFund()
        {
            //create bank account object
            var cus1Acc = new BankAccount(_customer, AccountType.Savings, 150);
            var cus2Acc = new BankAccount(_anotherCustomer, AccountType.Current, 1000);

            //set balance to initialBal
            var initialBal = cus2Acc.Balance;

            //Call the make transfer method
            cus2Acc.MakeTransfer(cus1Acc, 100, DateTime.Now, "Valid transfer test");
            
            // set balance after transaction
            var depositBalance = cus2Acc.Balance;
           
            //check that the balance is deduced
            Assert.That(depositBalance, Is.EqualTo(initialBal - 100));
        }

       
        [Test]
        public void SavingsAccountInvalidTransfer()
        {
            //create bank account
            var cus1Acc = new BankAccount(_customer, AccountType.Current, 5000);
            var cus2Acc = new BankAccount(_anotherCustomer, AccountType.Savings, 1000);

            //check for available balance is less than the  transfer amount
            Assert.That( () => cus2Acc.MakeTransfer(cus1Acc, 990, DateTime.Now, "Savings Test for transfers more than available balance"),
                                    Throws.TypeOf<InvalidOperationException>() );

        }


        [Test]
        public void CurrentAccountInvalidTransfer()
        {
            // create bank account of savings and current
            var cus1Acc = new BankAccount(_anotherCustomer, AccountType.Current, 1400);
            var cus2Acc = new BankAccount(_anotherCustomer, AccountType.Savings, 500);

            //check for available balance is less than the  transfer amount
            Assert.That(() => cus1Acc.MakeTransfer(cus2Acc, 1500, DateTime.Now, "Current account Test for transfer more than available balance"),
                                    Throws.TypeOf<InvalidOperationException>()
            );

        }


        [Test]
        public void SameAccountTransfer()
        {
            // create bank account 
            var cus1Acc = new BankAccount(_anotherCustomer, AccountType.Current, 1400);

            // check that the both account is not the same
            Assert.That( () => cus1Acc.MakeTransfer(cus1Acc, 1000, DateTime.Now, "You cannot transfer to yourself"),
                                Throws.TypeOf<ArgumentException>()
            );

        }

    }
}
