using System;
using Bankapp_refactored_week4.data;
using Bankapp_refactored_week4.Libraries;
using NUnit.Framework;

namespace Tests
{

    [TestFixture]
    public class RegisterTest
    {

        [Test]
        public void CheckThatCustomerProfileIsCreatedSuccessfully()
        {
            //create customer account profile
            var newCustomer = new Customer("Adeola", "Idowu", "adeolaidowu@yahoo.com", "adeola", "coolguy");
            
            //check that the AllCustomers list is populated with each customer sign up
            Assert.That(Bank.AllCustomers, Does.Contain(newCustomer));
        }


        [Test]
        public void CheckThatAccountIsCreatedSuccessfully()
        {
            //create customer account profile
            var newCustomer = new Customer("Adeola", "Idowu", "adeolaidowu@yahoo.com", "adeola", "coolguy");
           
            //create bank account for the above customer
            var bankAccount = new BankAccount(newCustomer, AccountType.Savings, 10000000);

            //check that the AllCustomersBankAccount list is populated with each account created
            Assert.That(Bank.AllCustomersBankAccount, Does.Contain(bankAccount));
        }


        [Test]
        public void LessThanTheMinimumAccountOpeningDepositForSavingsAccount()
        {
            //create customer account profile
            var newCustomer = new Customer("Adeola", "Idowu", "adeolaidowu@yahoo.com", "adeola", "coolguy");
           
            //Check that a savings account is only created with 100 minimum deposit
            Assert.That( ()=> new BankAccount(newCustomer, AccountType.Savings, 10),
                                        Throws.TypeOf<InvalidOperationException>() );

        }


        [Test]
        public void LessThanTheMinimumAccountOpeningDepositForCurrentAccount()
        {
            //create customer account profile
            var newCustomer = new Customer("Emperor", "Current", "adeolaidowu@yahoo.com", "ade", "coolguy");
            
            //Check that a current account is only created with 100 minimum deposit
            Assert.That(() => new BankAccount(newCustomer, AccountType.Current, 800),
                                        Throws.TypeOf<InvalidOperationException>() );

        }

    }
}
