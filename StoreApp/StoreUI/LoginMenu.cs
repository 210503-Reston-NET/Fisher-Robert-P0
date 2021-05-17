using StoreBL;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreUI
{
    public class LoginMenu : StoreMenu
    {
        private const int ManagerCode = 4321;
        MyValidate validate;
        StoreBLInterface bussinessLayer;
        DAO _repo;
        public LoginMenu(StoreBLInterface BL)
        {
            validate = new StringValidator();
            bussinessLayer = BL;
        }
        public override void Start()
        {
            bool repeat = true;

            do{
            // Current Menu selector using Console as an output
            string output = "Welcome to the Login page For Bearly Camping!" + "\n";
            output += "Please make a selection." + "\n";
            output += "[0] New User." + "\n";
            output += "[1] Current Customer." + "\n";
            output += "[2] Exit." + "\n";
            string input = validate.ValidateString(output);

            switch (input)
            {
                // Case New Customer
                case "0":
                    bool confirmed = false;
                    string username = "";
                    string password = "";
                    string FirstName = "";
                    string LastName = "";
                    int UserCode = 1;

                    // Collect Info Necessary to Create a new customer
                    do {
                    output = "Welcome to Bearly Camping!";
                    output += "Please Input your login User Name, or type 1 to escape.";
                    username = validate.ValidateString(output);

                    // Allows a user to escape if this was unintentional
                    if (username == "1"){
                        repeat = false;
                        break;
                    }

                    // Verify Password
                    output = "Now Insert your User Password.";
                    password = validate.ValidateString(output);

                    output = "Please confirm your password";
                    string confirm = validate.ValidateString(output);

                    // Recieve Full Name 
                    output = "Enter a First Name";
                    FirstName = validate.ValidateString(output);

                    output = "Enter a Last Name";
                    LastName = validate.ValidateString(output);

                    // Case: Passwords did not match
                    if (password != confirm)
                        System.Console.WriteLine("Please make sure your passwords match!");
                    else
                        confirmed = true;
                    
                    // Checks for the correct Manager Code.
                    output = "Enter your manager code. If you are not a manager, just enter 1.";
                    UserCode = validate.ValidateInteger(output);
                    }
                    while(!confirmed);

                    // Add Customer to DB
                    Customer customer = new Customer(username, password, FirstName, LastName, UserCode);
                    
                    bool SuccessfulyAdded = bussinessLayer.AddCustomer(customer);
                    System.Console.WriteLine(SuccessfulyAdded);
                    break;
                // Case: Current Customer
                case "1":
                    output = "Please give us your Username.";
                    string user = validate.ValidateString(output);

                    output = "Please Insert your Password.";
                    string pass = validate.ValidateString(output);

                    // Check Input to saved Users
                    List<Customer> archive = bussinessLayer.GetAllCustomers();

                    foreach (Customer cust in archive)
                    {
                        if (user==cust.UserName && pass == cust.Password)
                        {
                            MenuFactory.GetMenu("Home", bussinessLayer.GetUser(user, pass)).Start();
                            repeat = false;
                            break;
                        }
                        // Case: Invalid Username/Password combination
                        else 
                            System.Console.WriteLine("Sorry, This username and Password combination is Invalid!");
                    }
                    break;
                // Case: Exit
                case "2":
                    repeat=false;
                    break;
                // Case: Unreachable
                default:
                    throw new System.Exception("You have somehow reached the default case in the LoginMenu switch.\n" +
                    "This message should be unreachable.");
            }
            // Continue until User escapes
            } while(repeat);
        }
    }
}