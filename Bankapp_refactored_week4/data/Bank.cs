using System.Collections.Generic;
using Bankapp_refactored_week4.Libraries;

namespace Bankapp_refactored_week4.data
{
    public class Bank
    {
        /// <summary>
        /// list to store all the customers in the Bank 
        /// </summary>
        public static List<Customer> AllCustomers = new List<Customer>();

        /// <summary>
        /// to store all the account in the bank
        /// </summary>
        public static List<BankAccount> AllCustomersBankAccount = new List<BankAccount>();
    }
}
