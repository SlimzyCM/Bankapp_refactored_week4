using System;
using System.Linq;
using Bankapp_refactored_week4.data;
using Bankapp_refactored_week4.Libraries;

namespace Bankapp_refactored_week4.Helpers
{
    public class FormController
    {
        public static void CustomerSignUp()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan; // set the text color to yellow
            Console.WriteLine("   --- Create new Customer ---  ");
            Console.ResetColor(); // Reset the console text color to default

            Console.WriteLine("----------------------------------");

            Console.ForegroundColor = ConsoleColor.White; // set the text color to white

            Console.Write("\nEnter Username: ");// sign up entry details
            string userName = Console.ReadLine();

            Console.Write("\nEnter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("\nEnter last name: ");
            string lastName = Console.ReadLine();

            Console.Write("\nEnter email Address: ");
            string eMail = Console.ReadLine();

            Console.Write("\nEnter password: ");
            string password = Console.ReadLine();
            Console.ResetColor(); // Reset the console text color to default


            //Create a Customer object
            var customer = new Customer(firstName, lastName, eMail, userName, password);

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
            Console.WriteLine($"\n\n{customer.FullName}, You have successfully registered... \n");
            Console.ResetColor(); // Reset the console text color to default


            Console.Write("\nPress Any key to LogIn");
            Console.ReadLine();

            //call the customerLogin method
            CustomerLogIn();

        }

        //Login method controller
        public static void CustomerLogIn()
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Yellow; // set the text color to yellow
            Console.WriteLine("  \t\t --- Log In  ---  ");
            Console.ResetColor(); // Reset the console text color to default

            Console.WriteLine("------------------------------------------------------");

            //collect username
            Console.Write("\nEnter username: ");
            var userName = Console.ReadLine();
            //collect password
            Console.Write("\nEnter password: ");
            var password = Console.ReadLine();

            //create customer variable assigned to null
            var myCustomer = Bank.AllCustomers.FirstOrDefault(item => item.Username == userName && item.Password == password);
            //check if found
            if (myCustomer != null)
            {
                myCustomer.LoggedInCheck(userName, password);
                Navigator.Profile(myCustomer.CustomerId, myCustomer.FullName);

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // set the text color to yellow
                Console.WriteLine($"\n\nIncorrect Username or Password.. \n");
                Console.ResetColor(); // Reset the console text color to default

                Console.WriteLine("\nPress * to Sign Up  || Press Any other key to try again"); //navigate back using *
                string command = Console.ReadLine();

                if (command == "*") CustomerSignUp();
                else CustomerLogIn();
            }
        }


    }
}
