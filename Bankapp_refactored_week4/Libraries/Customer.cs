using System;
using System.Collections.Generic;
using Bankapp_refactored_week4.data;

namespace Bankapp_refactored_week4.Libraries
{
    public class Customer
    {

        public int CustomerId { get; private set; }

        private string FirstName { get; set; }

        private string LastName { get; set; }

        public string FullName => LastName + " " + FirstName;

        public string Email { get; }
        public string Username { get; }
        public string Password { get; }

        public bool LoggedIn { get; private set; }

        private static int _seedId = 1;

        private readonly List<BankAccount> _customerAccount = new List<BankAccount>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"> customers firstname </param>
        /// <param name="lastName"> customer lastname</param>
        /// <param name="email"> customer email</param>
        /// <param name="username"> customer username</param>
        /// <param name="password"> customer password</param>
        public Customer(string firstName, string lastName, string email, string username, string password)
        {
            CustomerId = _seedId++;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;

            Bank.AllCustomers.Add(this);
        }

        public void AddAcount(BankAccount account)
        {
            _customerAccount.Add(account);
        }

        // checks the username and password and log the user in
        public void LoggedInCheck(string username, string password)
        {
            
            if (username == Username && password == Password) LoggedIn = true;
            else Console.WriteLine("Enter the correct username or password");
        }

        // check the logout boolean value to false
        public void LoggedOut()
        {
            LoggedIn = false;
        }

        //creates a list of type bank account and return each individual customers accounts 
        public List<BankAccount> CustomerAccountList()
        {
            return _customerAccount;
        }
    }

}
