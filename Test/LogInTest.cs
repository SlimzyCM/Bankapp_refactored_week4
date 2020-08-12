using Bankapp_refactored_week4.Libraries;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LogInTest
    {

        [Test]
        public void ExistingCustomerLogInSuccessFul() 
        {
            //Customer account created for the test
            var newCustomer = new Customer("Emperor", "Idowu", "adeolaidowu@yahoo.com", "free", "coolguy");
            
            //get username
            const string username = "free";
            //get password
            const string password = "coolguy";
            
            //call the Login method
            newCustomer.LoggedInCheck(username, password);
            
            //if the username and password is correct, loggedIn will be set to true
            Assert.That(newCustomer.LoggedIn);
            newCustomer.LoggedOut();

        }

        [Test]
        public void CustomerInvalidLoginAttempt()
        {
            //Customer account created for the test
            var newCustomer = new Customer("Emperor", "Idowu", "adeolaidowu@yahoo.com", "ade", "coolguy");
            
            const string username = "ade";
            const string password = "wrongPassword";

            //call the Login method with wrong username and password
            newCustomer.LoggedInCheck(username, password);
           
            //Test that  the loggedIn is false
            Assert.That(newCustomer.LoggedIn, Is.False);

        }
    }
}
