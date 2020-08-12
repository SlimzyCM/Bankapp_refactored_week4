using System;
using Bankapp_refactored_week4.Libraries;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TransactionTest
    {
        //Create two customer for the test
        private readonly Customer _customer = new Customer("Jane", "Micheal", "janemicheal@yahoo.com", "jane1", "12345");
        private readonly Customer _anotherCustomer = new Customer("Chidi", "Okobia", "chidimicheal@gmail.com", "chidi1", "45467");
       
        [Test]
        public void ValidTransactionIsStoredInTransactionList()
        {
            //bank account created for the test
            var cus1Acc = new BankAccount(_customer, AccountType.Current, 5000);
            var cus2Acc = new BankAccount(_anotherCustomer, AccountType.Savings, 1000);

            //call the GetAccountTransactionCount that return the total count of transaction for an account
            var countBeforeTransaction = cus1Acc.GetAccountTransactionCount();
            
            //call the make deposit method for customer 1
            cus1Acc.MakeDeposit(100, DateTime.Now, "Attempt to make a valid deposit ");
            //call the make transfer method for customer 2
            cus1Acc.MakeTransfer(cus2Acc, 100, DateTime.Now, "Valid transfer test");

            //get the count after the transaction
            var countAfterTransaction = cus1Acc.GetAccountTransactionCount();
            
            //test that the transaction list count is increased by the number of transaction performed 
            Assert.That(countAfterTransaction, Is.EqualTo(countBeforeTransaction + 2));

        }


        [Test]
        public void ValidTransactionOf_cus2Acc_IsNotStoredIn_cus1Acc_TransactionList()
        {

            var cus1Acc = new BankAccount(_customer, AccountType.Current, 5000);
            var cus2Acc = new BankAccount(_anotherCustomer, AccountType.Savings, 1000);
            
            //call the GetAccountTransactionCount that return the total count of transaction for an account
            var countBeforeTransaction = cus1Acc.GetAccountTransactionCount();

            //call the make deposit method for customer 2
            cus2Acc.MakeDeposit(1000, DateTime.Now, "Attempt to make a valid deposit ");

            //get the count after the transaction
            var countAfterTransaction = cus1Acc.GetAccountTransactionCount();

            //test that the transaction list count is the same for invalid transaction
            Assert.That(countAfterTransaction, Is.EqualTo(countBeforeTransaction));

        }

    }
}
