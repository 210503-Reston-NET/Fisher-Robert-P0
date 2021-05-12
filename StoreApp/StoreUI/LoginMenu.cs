using StoreBL;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreUI
{
    public class LoginMenu : StoreMenu
    {
        private const int ManagerCode = 4321;
        public override void Start()
        {
            MyValidate validate = new StringValidator();
            StoreMenu TargetMenu;
            StoreBussinessLayer bussinessLayer = new StoreBussinessLayer(new FileRepo());
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

                    do {
                    output = "Welcome to Bearly Camping!";
                    output += "Please Input your login User Name, or type 1 to escape.";
                    username = validate.ValidateString(output);

                    if (username == "1"){
                        repeat = false;
                        break;
                    }

                    output = "Now Insert your User Password.";
                    password = validate.ValidateString(output);

                    output = "Please confirm your password";
                    string confirm = validate.ValidateString(output);

                    do {
                        output = "Enter your manager code. If you are not a manager, just enter 1.";
                        int UserCode = validate.ValidateInteger(output);

                        if (UserCode == 4321){
                            this.CurrentUser.IsManager = true;
                            break;
                        }
                        if (UserCode == 1){
                            this.CurrentUser.IsManager = false;
                            break;
                        }
                        else
                            System.Console.WriteLine("Your input doesnt match any manager code. Please enter a valid code.");
                    } while(true);


                    if (password != confirm)
                        System.Console.WriteLine("Please make sure your passwords match!");
                    else
                        confirmed = true;
                    }
                    while(!confirmed);

                    Customer customer = new Customer(username, password);
                    System.Console.WriteLine(customer);
                    bool SuccessfulyAdded = bussinessLayer.AddCustomer(customer);
                    System.Console.WriteLine(SuccessfulyAdded);
                    break;
                // Case: Current Customer
                case "1":
                    output = "Please give us your Username.";
                    string user = validate.ValidateString(output);

                    output = "Please Insert your Password.";
                    string pass = validate.ValidateString(output);

                    List<Customer> archive = bussinessLayer.GetCustomers();

                    foreach (Customer cust in archive)
                    {
                        if (user==cust.UserName && pass == cust.Password)
                        {
                            TargetMenu = new HomeMenu();
                            TargetMenu.CurrentUser = cust;
                            TargetMenu.Start();
                            repeat = false;
                            break;
                        }
                        else 
                            System.Console.WriteLine("Sorry, This username and Password combination is Invalid!");
                    }
                    break;
                // Case: Exit
                case "2":
                    repeat=false;
                    break;
                default:
                    throw new System.Exception("You have somehow reached the default case in the LoginMenu switch.\n" +
                    "This message should be unreachable.");
            }
            } while(repeat);
        }
    }
}