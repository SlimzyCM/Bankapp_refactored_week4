using System;

namespace Bankapp_refactored_week4.Helpers
{
    public class Util
    {

        public bool IsEmailValid(string email)
        {
            return email != null && (email.Contains("@"));
        }

        // validate email input
        public string ValidateEmailFormat(string email)
        {
            var isEmValid = false;
            isEmValid = IsEmailValid(email);
            var input = "";

            while (isEmValid == false)
            {
                Console.WriteLine("\nInvalid email format");
                Console.WriteLine("Enter email again");
                input = Console.ReadLine();
                isEmValid = IsEmailValid(input);
            }

            return input;
        }


        

    }
}
