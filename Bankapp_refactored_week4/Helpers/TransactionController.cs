using System;
using System.Linq;
using Bankapp_refactored_week4.data;
using Bankapp_refactored_week4.Libraries;

namespace Bankapp_refactored_week4.Helpers
{
    public class TransactionController
    {

        public static void Deposit(Customer customer)
        {

            var accountList = customer.CustomerAccountList();

            if (accountList.Count < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to yellow
                Console.WriteLine($"\n\nYou have not opened any account... \n");
                Console.ResetColor(); // Reset the console text color to default
                Console.ReadKey();
                Navigator.Profile(customer.CustomerId, customer.FullName);

            }
            else
            {
                // a label for restore point
                depositLabel: Console.Clear();
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("\t       Please select Deposit Account:");
                Console.WriteLine("--------------------------------------------------------");

                //loops through the list of bank account ownered by this customer
                var count = 1;
                foreach (var item in accountList)
                {
                    //Populate the console with the option of accounts available
                    Console.WriteLine($"\n{count}:\t{item.AccountNumber}\t|\t{item.TypeOfAcc} \n");
                    count++;
                }
                Console.WriteLine("\n\nPress * to Return to Main menu  \n");

                Console.WriteLine("--------------------------------------------------------");


                Console.Write("\nEnter your option:");
                var selected = Console.ReadLine();

                //check that key entered is not * else call PerformTransaction 
                if (selected == "*") Navigator.PerformTransaction(customer.CustomerId, customer.FullName);

                //convert the string value to integer 
                var response = int.TryParse(selected, out int index); // .TryParse return boolean value

                if (response && index < count && index > 0)
                {
                    // selected the actual account to be used 
                    var transactionAccount = accountList[index - 1]; // "index - 1" list is Zero based index

                    depositChecklabel: Console.Write("\nEnter the Amount you want to deposit: "); // check another label
                    var depositAmount = Console.ReadLine();

                    Console.Write("\nEnter Note: ");
                    var depositNote = Console.ReadLine(); //collect deposite note

                    // call the amountchecker method to parse and return amount if valid and zero if invalid
                    var parseCheck = AmountChecker(depositAmount);
                    if (parseCheck > 0)
                    {
                        // call the makedeposit method in the BankAccount class
                        transactionAccount.MakeDeposit(parseCheck, DateTime.Now, depositNote);
                        Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
                        Console.WriteLine($"\n\nTransaction successful... \n");
                        Console.ResetColor(); // Reset the console text color to default

                        Console.ReadLine();
                        Navigator.PerformTransaction(customer.CustomerId, customer.FullName);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                        Console.WriteLine($"\nInvalid Amount Entry. \n");

                        Console.ResetColor(); // Reset the console text color to default
                        Console.WriteLine($"\nPlease try again... ");

                        Console.ReadLine();
                        Console.Clear();
                        goto depositChecklabel;
                    }


                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                    Console.WriteLine($"\nInvalid option.. \n");

                    Console.ResetColor(); // Reset the console text color to default
                    Console.WriteLine($"\nPlease try again.. ");

                    Console.ReadLine();
                    // return to the accountPick point
                    goto depositLabel;
                }



            }



        }

        public static void Withdrawal(Customer customer)
        {
            var accountList = customer.CustomerAccountList();

            if (accountList.Count < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to yellow
                Console.WriteLine($"\n\nYou have not opened any account... \n");
                Console.ResetColor(); // Reset the console text color to default
                Console.ReadKey();
                Navigator.Profile(customer.CustomerId, customer.FullName);

            }
            else
            {
                // a label for restore point
                withdrawalLabel: Console.Clear();
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("\t       Please select Withdrawal Account:");
                Console.WriteLine("--------------------------------------------------------");

                //loops through the list of bank account ownered by this customer
                var count = 1;
                foreach (var item in accountList)
                {
                    //Populate the console with the option of accounts available
                    Console.WriteLine($"\n{count}:\t{item.AccountNumber}\t|\t{item.TypeOfAcc} \n");
                    count++;
                }

                Console.WriteLine("\n\nPress * to Return to Main menu  \n");

                Console.WriteLine("--------------------------------------------------------");


                Console.Write("\nEnter your option:");
                var selected = Console.ReadLine();

                if (selected == "*") Navigator.PerformTransaction(customer.CustomerId, customer.FullName);

                //convert the string value to integer 
                var response = int.TryParse(selected, out var index); // .TryParse return boolean value

                if (response && index < count && index > 0)
                {
                    // selected the actual account to be used 
                    var transactionAccount = accountList[index - 1]; // "index - 1" list is Zero based index

                    withdrawalChecklabel: Console.Write("\nEnter the Amount you want to withdraw: "); // check another label
                    var withdrawAmount = Console.ReadLine();

                    Console.Write("\nEnter Note: ");
                    var withdrawNote = Console.ReadLine(); //collect withdrawal note

                    // call the amountchecker method to parse and return withdrawAmount if valid and zero if invalid
                    var parseCheck = AmountChecker(withdrawAmount);

                    //parsecheck now contains the .tryParse return of withdrawAmount 
                    if (parseCheck > 0)
                    {
                        //check that the account type is savings
                        // check that the account balance will remain 100 after transaction
                        if (transactionAccount.TypeOfAcc == AccountType.Savings && transactionAccount.Balance - parseCheck < 100)
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                            Console.WriteLine($"\nInsufficient funds for this transaction. \n");

                            Console.ResetColor(); // Reset the console text color to default

                            Console.ReadLine();
                            Console.Clear();
                            goto withdrawalLabel;
                        }
                        else
                        {
                            transactionAccount.MakeWithdrawal(parseCheck, DateTime.Now, withdrawNote);
                            Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
                            Console.WriteLine($"\n\nTransaction successful... \n");
                            Console.ResetColor(); // Reset the console text color to default

                            Console.ReadLine();
                            Navigator.PerformTransaction(customer.CustomerId, customer.FullName);

                        }


                        if (transactionAccount.TypeOfAcc == AccountType.Current && transactionAccount.Balance - parseCheck < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                            Console.WriteLine($"\nInsufficient funds for this transaction. \n");

                            Console.ResetColor(); // Reset the console text color to default

                            Console.ReadLine();
                            Console.Clear();
                            goto withdrawalLabel;
                        }
                        else

                        {
                            transactionAccount.MakeWithdrawal(parseCheck, DateTime.Now, withdrawNote);
                            Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
                            Console.WriteLine($"\n\nTransaction successful... \n");
                            Console.ResetColor(); // Reset the console text color to default

                            Console.ReadLine();
                            Navigator.PerformTransaction(customer.CustomerId, customer.FullName);
                        }



                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                        Console.WriteLine($"\nInvalid Amount Entry. \n");

                        Console.ResetColor(); // Reset the console text color to default
                        Console.WriteLine($"\nPlease try again... ");

                        Console.ReadLine();
                        Console.Clear();
                        goto withdrawalChecklabel;
                    }


                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                    Console.WriteLine($"\nInvalid option.. \n");

                    Console.ResetColor(); // Reset the console text color to default
                    Console.WriteLine($"\nPlease try again.. ");

                    Console.ReadLine();
                    // return to the accountPick point
                    goto withdrawalLabel;
                }

            }
        }


        public static void Transfer(Customer customer)
        {
            var accountList = customer.CustomerAccountList();

            if (accountList.Count < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to yellow
                Console.WriteLine($"\n\nYou have not opened any account... \n");
                Console.ResetColor(); // Reset the console text color to default
                Console.ReadKey();
                Navigator.Profile(customer.CustomerId, customer.FullName);

            }
            else
            {
                transferLabel: Console.Clear();
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("\t       Please select funds transfer type:");
                Console.WriteLine("--------------------------------------------------------");

                Console.WriteLine("1: To My Account  \n"); // customer transfer between owned accounts
                Console.WriteLine("2: To Other Customer  \n\n"); // transfer between customers
                Console.WriteLine("\nPress * to Return to Main menu  \n");

                Console.WriteLine("--------------------------------------------------------");
                Console.ResetColor();

                Console.WriteLine("Note: Use left Numbering for Navigation...\n\n"); //instruction for the program use

                Console.Write("\nEnter your option:");

                var command = Console.ReadLine();

                if (command == "*") Navigator.PerformTransaction(customer.CustomerId, customer.FullName);


                switch (command)
                {
                    case "1":
                        if (accountList.Count > 1)
                        {
                            transferCheckLabel: Console.Clear();
                            Console.WriteLine("--------------------------------------------------------");
                            Console.WriteLine("\t       Please select a payment source:");
                            Console.WriteLine("--------------------------------------------------------");

                            //loops through the list of bank account owner-ed by this customer
                            int count = 1;
                            foreach (var item in accountList)
                            {
                                //Populate the console with the option of accounts available
                                Console.WriteLine($"\n{count}:\t{item.AccountNumber}\t|\t{item.TypeOfAcc} \n");
                                count++;
                            }

                            Console.WriteLine("\n\nPress * to Return to Main menu  \n");

                            Console.WriteLine("--------------------------------------------------------");


                            Console.Write("\nEnter your option:");
                            string selected = Console.ReadLine();

                            //option entered check
                            if (selected == "*") goto transferLabel;

                            //convert the string value to integer 
                            bool response = int.TryParse(selected, out int index); // .TryParse return boolean value

                            if (response && index < count && index > 0)//check that the option selected is in the list of account
                            {
                                // pick the payment source 
                                var transferFromAccount = accountList[index - 1]; // "index - 1" list is Zero based index

                                transferDestinationCheckLabel: Console.Clear();
                                Console.WriteLine("--------------------------------------------------------");
                                Console.WriteLine("\t       Please select the destination account:");
                                Console.WriteLine("--------------------------------------------------------");

                                int secondCount = 1; // secondcount for list numbering

                                //loops through the list of bank account ownered by this customer
                                foreach (var item in accountList.Where(item => item != transferFromAccount))
                                {
                                    //Populate the console with the option of accounts available
                                    Console.WriteLine($"\n{secondCount}:\t{item.AccountNumber}\t|\t{item.TypeOfAcc} \n");
                                    secondCount++;
                                }

                                Console.WriteLine("\n\nPress * to Return to Main menu  \n");

                                Console.WriteLine("--------------------------------------------------------");


                                Console.Write("\nEnter your option:");
                                string secondSelected = Console.ReadLine();
                                //option entered check
                                if (secondSelected == "*") goto transferCheckLabel;

                                //convert the string value to integer 
                                bool secondResponse = int.TryParse(secondSelected, out int secondIndex); // .TryParse return boolean value

                                if (secondResponse && secondIndex < secondCount && secondIndex > 0)//check that the option selected is in the list of account
                                {
                                    BankAccount transferDestinationAccount; //initialize a new variable to store the destination account

                                    //account transferring has been removed from the list, the index will reduce by 1
                                    if (secondIndex < index) transferDestinationAccount = accountList[secondIndex - 1]; // "index - 1" list is Zero based index

                                    // the selected receiver has the same index with account transferring use the index directly
                                    else if (secondIndex == index) transferDestinationAccount = accountList[secondIndex];

                                    // the selected receiver has the greater index with account transferring use the index directly
                                    else transferDestinationAccount = accountList[secondIndex];

                                    transferDestinationLabel: Console.WriteLine($"\n{transferDestinationAccount.AccountNumber}");

                                    Console.Write("\nEnter the Amount you want to transfer: "); // check another label
                                    string transferAmount = Console.ReadLine();

                                    Console.Write("\nEnter Note: ");
                                    string transferNote = Console.ReadLine(); //collect withdrawal note

                                    // call the amountchecker method to parse and return withdrawAmount if valid and zero if invalid
                                    var parseCheck = AmountChecker(transferAmount);

                                    if (parseCheck > 0)
                                    {
                                        //check that the account type is savings
                                        // check that the account balance will remain 100 after transaction
                                        if (transferFromAccount.TypeOfAcc == AccountType.Savings && transferFromAccount.Balance - parseCheck < 100)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                            Console.WriteLine($"\nInsufficient funds for this transaction. \n");

                                            Console.ResetColor(); // Reset the console text color to default

                                            Console.ReadLine();
                                            Console.Clear();
                                            goto transferCheckLabel;
                                        }
                                        else
                                        {
                                            transferFromAccount.MakeTransfer(transferDestinationAccount, parseCheck, DateTime.Now, transferNote);
                                            Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
                                            Console.WriteLine($"\n\nTransaction successful... \n");
                                            Console.ResetColor(); // Reset the console text color to default

                                            Console.ReadLine();
                                            Navigator.PerformTransaction(customer.CustomerId, customer.FullName);

                                        }


                                        if (transferFromAccount.TypeOfAcc == AccountType.Current && transferFromAccount.Balance - parseCheck < 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                            Console.WriteLine($"\nInsufficient funds for this transaction. \n");

                                            Console.ResetColor(); // Reset the console text color to default

                                            Console.ReadLine();
                                            Console.Clear();
                                            goto transferCheckLabel;
                                        }
                                        else

                                        {
                                            transferFromAccount.MakeTransfer(transferDestinationAccount, parseCheck, DateTime.Now, transferNote);
                                            Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
                                            Console.WriteLine($"\n\nTransaction successful... \n");
                                            Console.ResetColor(); // Reset the console text color to default

                                            Console.ReadLine();
                                            Navigator.PerformTransaction(customer.CustomerId, customer.FullName);
                                        }




                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                        Console.WriteLine($"\nInvalid Amount Entry. \n");

                                        Console.ResetColor(); // Reset the console text color to default
                                        Console.WriteLine($"\nPlease try again... ");

                                        Console.ReadLine();
                                        Console.Clear();
                                        goto transferDestinationLabel;// continue call to return to the label
                                    }


                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                    Console.WriteLine($"\nInvalid option.. \n");

                                    Console.ResetColor(); // Reset the console text color to default
                                    Console.WriteLine($"\nPlease try again.. ");

                                    Console.ReadLine();
                                    // return to the accountPick point
                                    goto transferDestinationCheckLabel;

                                }

                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                Console.WriteLine($"\nInvalid option.. \n");

                                Console.ResetColor(); // Reset the console text color to default
                                Console.WriteLine($"\nPlease try again.. ");

                                Console.ReadLine();
                                // return to the accountPick point
                                goto transferCheckLabel;
                            }

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                            Console.WriteLine($"\nInvalid option, You only have one account... \n");

                            Console.ResetColor(); // Reset the console text color to default

                            Console.ReadLine();
                            goto transferLabel;

                        }
                        break;
                    case "2":

                        case2TransferLabel: Console.Clear();
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("\t       Please select a payment source:");
                        Console.WriteLine("--------------------------------------------------------");

                        //loops through the list of bank account ownered by this customer
                        int custCount = 1;
                        foreach (var item in accountList)
                        {
                            //Populate the console with the option of accounts available
                            Console.WriteLine($"\n{custCount}:\t{item.AccountNumber}\t|\t{item.TypeOfAcc} \n");
                            custCount++;
                        }

                        Console.WriteLine("\n\nPress * to Return to Main menu  \n");

                        Console.WriteLine("--------------------------------------------------------");


                        Console.Write("\nEnter your option:");
                        string input = Console.ReadLine();

                        //option entered check
                        if (input == "*") goto transferLabel;

                        //convert the string value to integer 
                        bool inputResponse = int.TryParse(input, out int custIndex); // .TryParse return boolean value
                        if (inputResponse && custIndex < custCount && custIndex > 0)//check that the option selected is in the list of account
                        {
                            var secondTransferFromAccount = accountList[custIndex - 1];
                            case2SecondTransferLabel: Console.Write("\nEnter the Account Number of the Receiver: "); // check another label
                            string transferDestinationCustomer = Console.ReadLine();

                            bool resp = int.TryParse(transferDestinationCustomer, out int transferDestinationCustomerParse);


                            BankAccount myCustomerAccount = null;
                            foreach (BankAccount item in Bank.AllCustomersBankAccount)
                            {
                                if (item.AccountNumber == transferDestinationCustomerParse.ToString())
                                {
                                    myCustomerAccount = item;
                                    break;
                                }
                            }
                            //check if found

                            if (myCustomerAccount == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                Console.WriteLine($"\nAccount number does not exist with us.. \n");
                                Console.ResetColor(); // Reset the console text color to default
                                Console.ReadKey();
                                Console.Clear();
                                goto case2TransferLabel;

                            }
                            else
                            {
                                Console.Write("\nEnter Amount to transfer: ");
                                string transferDestinationCustomerAmount = Console.ReadLine();
                                Console.Write("\nEnter Amount to transfer: ");
                                var parseCheck = AmountChecker(transferDestinationCustomerAmount);

                                if (parseCheck > 0)
                                {
                                    Console.Write("\nEnter Note: "); // check another label
                                    string transferCustomerNote = Console.ReadLine();

                                    //check that the account type is savings
                                    // check that the account balance will remain 100 after transaction
                                    if (secondTransferFromAccount.TypeOfAcc == AccountType.Savings && secondTransferFromAccount.Balance - parseCheck < 100)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                        Console.WriteLine($"\nInsufficient funds for this transaction. \n");

                                        Console.ResetColor(); // Reset the console text color to default

                                        Console.ReadLine();
                                        Console.Clear();
                                        goto case2TransferLabel;
                                    }
                                    else
                                    {
                                        //All has been met call the bank account withdrawal method
                                        secondTransferFromAccount.MakeTransfer(myCustomerAccount, parseCheck, DateTime.Now, transferCustomerNote);
                                        Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
                                        Console.WriteLine($"\n\nTransaction successful... \n");
                                        Console.ResetColor(); // Reset the console text color to default

                                        Console.ReadKey();
                                        Navigator.PerformTransaction(customer.CustomerId, customer.FullName);

                                    }

                                    // check that the account type is current
                                    // check that the account balance will be enough to carry out the transaction
                                    if (secondTransferFromAccount.TypeOfAcc == AccountType.Current && secondTransferFromAccount.Balance - parseCheck < 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                        Console.WriteLine($"\nInsufficient funds for this transaction. \n");

                                        Console.ResetColor(); // Reset the console text color to default

                                        Console.ReadLine();
                                        Console.Clear();
                                        //goto transferCheckLabel;
                                    }
                                    else

                                    {
                                        secondTransferFromAccount.MakeTransfer(myCustomerAccount, parseCheck, DateTime.Now, transferCustomerNote);
                                        Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
                                        Console.WriteLine($"\n\nTransaction successful... \n");
                                        Console.ResetColor(); // Reset the console text color to default

                                        Console.ReadLine();
                                        Navigator.PerformTransaction(customer.CustomerId, customer.FullName);
                                    }


                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                                    Console.WriteLine($"\nInvalid Amount Entry. \n");

                                    Console.ResetColor(); // Reset the console text color to default
                                    Console.WriteLine($"\nPlease try again... ");

                                    Console.ReadLine();
                                    Console.Clear();
                                    goto case2TransferLabel;
                                }






                            }



                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                            Console.WriteLine($"\nInvalid option.. \n");

                            Console.ResetColor(); // Reset the console text color to default
                            Console.WriteLine($"\nPlease try again.. ");

                            Console.ReadLine();
                            Console.Clear();

                            goto case2TransferLabel;
                        }


                        break;
                    case "*":
                        Navigator.PerformTransaction(customer.CustomerId, customer.FullName);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                        Console.WriteLine($"\nInvalid option.. \n");

                        Console.ResetColor(); // Reset the console text color to default
                        Console.WriteLine($"\nPlease try again.. ");

                        Console.ReadLine();
                        Console.Clear();
                        Transfer(customer);
                        break;

                }

            }
        }

        public static void CheckBalance(Customer customer)
        {
            var accountList = customer.CustomerAccountList();

            if (accountList.Count < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to yellow
                Console.WriteLine($"\n\nYou have not opened any account... \n");
                Console.ResetColor(); // Reset the console text color to default
                Console.ReadKey();
                Navigator.Profile(customer.CustomerId, customer.FullName);

            }
            else
            {
            balanceCheckLabel: Console.Clear();
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("\t       Please select the transaction source:");
                Console.WriteLine("--------------------------------------------------------");

                //loops through the list of bank account ownered by this customer
                int count = 1;
                foreach (var item in accountList)
                {
                    //Populate the console with the option of accounts available
                    Console.WriteLine($"\n{count}:\t{item.AccountNumber}\t|\t{item.TypeOfAcc} \n");
                    count++;
                }


                Console.WriteLine("\n\nPress * to Return to Main menu  \n");

                Console.WriteLine("--------------------------------------------------------");


                Console.Write("\nEnter your option:");
                string selected = Console.ReadLine();
                //check that key entered is not * else call PerformTransaction 
                if (selected == "*") Navigator.PerformTransaction(customer.CustomerId, customer.FullName);

                //convert the string value to integer 
                bool response = int.TryParse(selected, out int index); // .TryParse return boolean value

                if (response && index < count && index > 0)
                {
                    // selected the actual account to be used 
                    var BalanceCheckAccount = accountList[index - 1]; // "index - 1" list is Zero based index

                    Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
                    Console.WriteLine($"\n\nGreat! Your account balance is N{BalanceCheckAccount.Balance} \n");
                    Console.ResetColor(); // Reset the console text color to default

                    Console.ReadLine();
                    Navigator.PerformTransaction(customer.CustomerId, customer.FullName);

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                    Console.WriteLine($"\nInvalid option.. \n");

                    Console.ResetColor(); // Reset the console text color to default
                    Console.WriteLine($"\nPlease try again.. ");

                    Console.ReadLine();
                    Console.Clear();
                    goto balanceCheckLabel;
                }


            }
        }


        public static void StatementOfAccount(Customer customer)
        {
            var accountList = customer.CustomerAccountList();

            if (accountList.Count < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to yellow
                Console.WriteLine($"\n\nYou have not opened any account... \n");
                Console.ResetColor(); // Reset the console text color to default
                Console.ReadKey();
                Navigator.Profile(customer.CustomerId, customer.FullName);

            }
            else
            {
                statementCheckLabel: Console.Clear();
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("\t       Please select the transaction source:");
                Console.WriteLine("--------------------------------------------------------");

                //loops through the list of bank account ownered by this customer
                int count = 1;
                foreach (var item in accountList)
                {
                    //Populate the console with the option of accounts available
                    Console.WriteLine($"\n{count}:\t{item.AccountNumber}\t|\t{item.TypeOfAcc} \n");
                    count++;
                }


                Console.WriteLine("\n\n\nPress * to Return to Main menu  \n");

                Console.WriteLine("--------------------------------------------------------");


                Console.Write("\nEnter your option:");
                string selected = Console.ReadLine();
                //chek that key entered is not * else call PerformTransaction 
                if (selected == "*") Navigator.PerformTransaction(customer.CustomerId, customer.FullName);

                //convert the string value to integer 
                bool response = int.TryParse(selected, out int index); // .TryParse return boolean value

                if (response && index < count && index > 0)
                {
                    // selected the actual account to be used 
                    var statementCheckAccount = accountList[index - 1]; // "index - 1" list is Zero based index

                    Console.Clear();
                    Console.WriteLine($"\n\t\tSTATEMENT OF ACCOUNT");
                    Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to red

                    Console.WriteLine($"{statementCheckAccount.GetAccountHistory()}");
                    Console.ResetColor(); // Reset the console text color to default

                    Console.ReadKey();
                    Navigator.PerformTransaction(customer.CustomerId, customer.FullName);


                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                    Console.WriteLine($"\nInvalid option.. \n");

                    Console.ResetColor(); // Reset the console text color to default
                    Console.WriteLine($"\nPlease try again.. ");

                    Console.ReadLine();
                    Console.Clear();
                    goto statementCheckLabel;
                }


            }
        }

        //method to parse amount before transaction
        public static decimal AmountChecker(string amount)
        {
            var response = decimal.TryParse(amount, out decimal amountChecked); // .TryParse return boolean value

            return response ? amountChecked : 0;
        }



    }
}
