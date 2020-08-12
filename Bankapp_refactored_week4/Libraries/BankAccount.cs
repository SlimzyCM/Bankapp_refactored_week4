using System;
using System.Collections.Generic;
using System.Linq;
using Bankapp_refactored_week4.data;

namespace Bankapp_refactored_week4.Libraries
{
    public class BankAccount
    {

        /// <summary>
        /// class containing the account details
        /// 
        /// </summary>
        public Customer AccountOwner { get; }
        //property that indicates the customer that owns the account

        public AccountType TypeOfAcc { get; }
        //customer account type

        //the account number
        public string AccountNumber { get; }

        // combine numbers to create an account format and then auto increment
        private static int _accountNumberSeed = 1234567890;

        // account creation time
        public DateTime DateCreated { get; }


        //balance
        public decimal Balance
        {
            get
            {
                return AllTransactions.Sum(x => x.Amount);
            }
        }

        //list of type transaction to store all transactions
        public List<Transaction> AllTransactions = new List<Transaction>();

        // the constructor to create the account
        public BankAccount(Customer accountOwner, AccountType type, decimal initialBalance)
        {

            AccountOwner = accountOwner;
            TypeOfAcc = type;
            DateCreated = DateTime.Now;
            if (TypeOfAcc == AccountType.Current && initialBalance < 1000) throw new InvalidOperationException("Initial deposit for a Current account must be 1000 and above.. ");
            if (TypeOfAcc == AccountType.Savings && initialBalance < 100) throw new InvalidOperationException("Initial deposit for a Savings account must be 100 and above.. ");

            MakeDeposit(initialBalance, DateTime.Now, "Initial Deposit Amount");

            //Convert to string
            this.AccountNumber = _accountNumberSeed.ToString();
            _accountNumberSeed++;

            // add the created account by calling the customer owned account constructor
            AccountOwner.AddAcount(this);

            // add all account to all customer account list
            Bank.AllCustomersBankAccount.Add(this);


        }

        // the method for making deposit
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {

            if (amount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(AccountOwner, AccountNumber, TypeOfAcc, Balance, Balance + amount, amount, date, note);
            AllTransactions.Add(deposit);
        }

        // the withdrawal method

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {

            if (TypeOfAcc == AccountType.Savings && Balance - amount < 100) throw new InvalidOperationException("Insufficient funds for this transaction. ");
            if (TypeOfAcc == AccountType.Current && Balance - amount < 0) throw new InvalidOperationException("Insufficient funds for this transaction. ");

            if (amount <= 0)
            {
                throw new ArgumentNullException(nameof(amount), "Amount of withdrawal must be positive");
            }

            var withdrawal = new Transaction(AccountOwner, AccountNumber, TypeOfAcc, Balance, Balance - amount, -amount, date, note);
            AllTransactions.Add(withdrawal);
        }

        // transfer method
        public void MakeTransfer(BankAccount receiver, decimal amount, DateTime date, string note)
        {
            if (TypeOfAcc == AccountType.Savings && Balance - amount < 100) throw new InvalidOperationException("Insufficient Balance for this transfer. ");
            if (TypeOfAcc == AccountType.Current && Balance - amount < 0) throw new InvalidOperationException("Insufficient Balance for this transfer. ");
            if(this.AccountNumber == receiver.AccountNumber) throw new ArgumentException("You cannot transfer to yourself");
            // call the deposit method for the receiver
            receiver.MakeDeposit(amount, date, note);

            // same call the withdrawal method for the sender
            this.MakeWithdrawal(amount, date, note);

        }

        public int GetAccountTransactionCount()
        {
            return AllTransactions.Count;

        }

        // used to get the statement of account
            public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;



            report.AppendLine("--------------------------------------------------------");

            // Header for the transaction history
            report.AppendLine("Date\t\tAmount\tBalance\t\tNote");
            report.AppendLine("--------------------------------------------------------");

            foreach (var item in AllTransactions)
            {
                balance += item.Amount;

                //Each rows of transaction in the transaction list
                report.AppendLine($"\n{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }
            report.AppendLine("--------------------------------------------------------");

            return report.ToString();
        }

    }

}
