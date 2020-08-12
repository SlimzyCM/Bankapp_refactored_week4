using System;
using Bankapp_refactored_week4.data;
using Bankapp_refactored_week4.Libraries;

namespace Bankapp_refactored_week4.Helpers
{
    public class Navigator
    {

        /// <summary>
        /// HopePage is the Default method
        /// </summary>
        public static void HomePage()
        {
            Console.Clear(); // clear the Console
            Console.WriteLine("=====================================");

            Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
            Console.WriteLine("\n\tWelcome to SlimBank\n  ");
            Console.ResetColor(); // Reset the console text color to default

            Console.WriteLine("=====================================");


            //homepage menu 
            Console.ForegroundColor = ConsoleColor.White; // set the text color to white
            Console.WriteLine("\n1: New Customer Sign Up\n");
            Console.WriteLine("2: Existing Customers Log In\n");
            Console.WriteLine("-------------------------------------");
            Console.ResetColor();

            Console.WriteLine("Note: Use left Numbering for Navigation..\n\n"); //instruction for the program use

            Console.ForegroundColor = ConsoleColor.White; // set the text color to white
            Console.Write("Enter your option:");
            Console.ResetColor();

            // Read User input
            string command = Console.ReadLine();

            //check the input and perform based on it
            if (command == "1")
            {
                FormController.CustomerSignUp();
            }
            else if (command == "2")
            {
                FormController.CustomerLogIn();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                Console.WriteLine($"\n\n\tInvalid option.. \n");
                Console.ResetColor(); // Reset the console text color to default

                Console.ReadLine();

                HomePage();

            }

        }


        public static void Profile(int customerId, string name)
        {
        profileLabel: Console.Clear(); // clear the Console
            Console.WriteLine("---------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan; // set the text color to cyan
            Console.WriteLine($"\t      Welcome {name}  ");
            Console.ResetColor(); // Reset the console text color to default

            Console.WriteLine("---------------------------------------");

            Console.ForegroundColor = ConsoleColor.White; // set the text color to white
            Console.WriteLine("\n1: Create New Account\n"); // menu for selection
            Console.WriteLine("2: Perform Transaction on Existing Account\n");
            Console.WriteLine("3: Log Out \n");

            Console.WriteLine("---------------------------------------");
            Console.ResetColor();

            Console.WriteLine("Note: Use left Numbering for Navigation..\n\n"); //instruction for the program use


            string command = Console.ReadLine();

            // 1 => create account functionality
            if (command == "1") selectAccountOperation(customerId, name);

            // 2 => performTransaction functionality
            else if (command == "2")
            {
                PerformTransaction(customerId, name);
            }
            else if (command == "3")
            {
                // loop through the all customer list and find the matching Id
                Bank.AllCustomers.ForEach(item => { if (item.CustomerId == customerId) item.LoggedOut(); });
                HomePage();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                Console.WriteLine($"\nInvalid option.. \n");

                Console.ResetColor(); // Reset the console text color to default
                Console.WriteLine($"\nPress any key to try again.. ");

                Console.ReadLine();
                goto profileLabel;

            }


        }


        public static void selectAccountOperation(int customerId, string name)
        {
        //selectAccount - label to repeat process if invalid key selected
        selectAccount: Console.Clear();
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("\t\tSelect Account Type");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("1: Current | Requires initial deposit of 1000 and above.\n");
            Console.WriteLine("2: Savings | Requires initial deposit of 100 and above.\n");

            Console.Write("\nEnter your option:");

            string selected = Console.ReadLine();

            if (selected == "1")
            {

                Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow

            // Enter initial deposit value
            enterAmount: Console.WriteLine("\n\nEnter Initial deposit Amount");
                Console.ResetColor(); // Reset the console text color to default

                string depositRead = Console.ReadLine(); // store the input value in depositRead

                //convert the string value to integer 
                bool response = decimal.TryParse(depositRead, out decimal amount); // .TryParse return boolean value

                if (response)
                {
                    if (amount < 1000)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                        Console.WriteLine($"\n\nInitial deposit for a Current account must be 1000 and above.. \n"); // error message for less than required amount

                        Console.ResetColor(); // Reset the console text color to default

                        Console.ReadLine();
                        Console.Clear();
                        goto enterAmount;

                    }
                    else AccountController.CreateAccount(AccountType.Current, amount, customerId);

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                    Console.WriteLine($"\n\nInvalid amount, Enter only Digits.. \n"); // error message for non digit

                    Console.ResetColor(); // Reset the console text color to default
                    Console.WriteLine("\nPress * to select Account type || Press Any other key to try again");

                    // store the user input
                    string repeatEnterAmount = Console.ReadLine();

                    //clear the console existing text
                    Console.Clear();

                    //condition to redirect based on the key selected
                    if (repeatEnterAmount == "*") goto selectAccount;
                    else goto enterAmount;


                }


            }
            else if (selected == "2")
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow

            // Enter initial deposit value
            enterAmount: Console.WriteLine("\n\nEnter Initial deposit Amount");
                Console.ResetColor(); // Reset the console text color to default

                string depositRead = Console.ReadLine(); // store the input value in depositRead

                //convert the string value to integer 
                bool response = decimal.TryParse(depositRead, out decimal amount); // .TryParse return boolean value

                if (response)
                {
                    if (amount < 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                        Console.WriteLine($"\n\nInitial deposit for a Current account must be 100 and above.. \n"); // error message for less than required amount

                        Console.ResetColor(); // Reset the console text color to default

                        Console.ReadLine();
                        Console.Clear();
                        goto enterAmount;

                    }
                    else AccountController.CreateAccount(AccountType.Savings, amount, customerId);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                    Console.WriteLine($"\n\nInvalid amount, Enter only Digits.. \n"); // error message for non digit

                    Console.ResetColor(); // Reset the console text color to default
                    Console.WriteLine("\nPress * to select Account type || Press Any other key to try again");

                    // store the user input
                    string repeatEnterAmount = Console.ReadLine();

                    //clear the console existing text
                    Console.Clear();

                    //condition to redirect based on the key selected
                    if (repeatEnterAmount == "*") goto selectAccount;
                    else goto enterAmount;


                }
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                Console.WriteLine($"\nInvalid option.. \n");

                Console.ResetColor(); // Reset the console text color to default
                Console.WriteLine($"\nPress any key to try again.. ");

                Console.ReadLine();

                // return back to labelled point
                goto selectAccount;

            }

        }



        

        public static void PerformTransaction(int customerId, string name)
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("\t\tPerform Transaction");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("1: Cash Deposit  \n");
            Console.WriteLine("2: Withdrawal  \n");
            Console.WriteLine("3: Transfer Fund  \n");
            Console.WriteLine("4: Balance Inquiry  \n");
            Console.WriteLine("5: Statement Of Account  \n\n");
            Console.WriteLine("Press * to Return to Main menu  \n");

            Console.WriteLine("--------------------------------------------------------");
            Console.ResetColor();

            Console.WriteLine("Note: Use left Numbering for Navigation...\n\n"); //instruction for the program use

            Console.Write("\nEnter your option:");

            string command = Console.ReadLine();
            Customer myCustomer = null;
            foreach (Customer item in Bank.AllCustomers)
            {
                if (item.CustomerId == customerId && item.FullName == name)
                {
                    myCustomer = item;
                    break;
                }
            }

            // perform particular transaction selected using switch
            switch (command)
            {
                case "1":
                    // call the deposit method with the customer class
                    TransactionController.Deposit(myCustomer);
                    break;
                case "2":
                    // call the withdraw method with the customer class
                    TransactionController.Withdrawal(myCustomer);
                    break;
                case "3":
                    //call the transfer method with the customer class
                    TransactionController.Transfer(myCustomer);
                    break;
                case "4":
                    //call the checkbalance method with the customer class
                    TransactionController.CheckBalance(myCustomer);
                    break;
                case "5":
                    //call the statement of account method with the customer class
                    TransactionController.StatementOfAccount(myCustomer);
                    break;
                case "*":
                    // * to call the profile and go back
                    Profile(customerId, name);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red; // set the text color to red
                    Console.WriteLine($"\nInvalid option.. \n");

                    Console.ResetColor(); // Reset the console text color to default
                    Console.WriteLine($"\nPress any key to try again.. ");

                    Console.ReadLine();
                    //make a call to PerformTransaction with Id and name
                    PerformTransaction(customerId, name);
                    break;
            }





        }






    }
}
