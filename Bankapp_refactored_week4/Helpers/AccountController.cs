using System;
using System.Linq;
using Bankapp_refactored_week4.data;
using Bankapp_refactored_week4.Libraries;

namespace Bankapp_refactored_week4.Helpers
{
    public class AccountController
    {
        public static void CreateAccount(AccountType type, decimal amount, int customerId)
        {


            Customer myCustomer = Bank.AllCustomers.FirstOrDefault(item => item.CustomerId == customerId);


            BankAccount createAccount = new BankAccount(myCustomer, type, amount);

            Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to red
            Console.WriteLine($"\n\nA {createAccount.TypeOfAcc} account {createAccount.AccountNumber} was created with N{createAccount.Balance} initial balance...\n");

            Console.ResetColor(); // Reset the console text color to default

            Console.ReadLine();

            if (myCustomer != null) Navigator.Profile(myCustomer.CustomerId, myCustomer.FullName);
        }

    }
}
