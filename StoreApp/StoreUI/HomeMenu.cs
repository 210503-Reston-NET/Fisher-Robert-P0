using System;

namespace StoreUI
{
    public class HomeMenu : StoreMenu
    {
        public void Start()
        {
            StoreMenu TargetMenu = null;
            StringValidator validate = new StringValidator();
            bool repeat = true;
            do
            {
                // Current Menu selector using Console as an output
                string output = "Welcome to the main store page!" + "\n";
                output += "Please make a selection." + "\n";
                output += "[0] Order Product." + "\n";
                output += "[1] Add Product." + "\n";
                output += "[2] Exit." + "\n";
                string input = validate.ValidateString(output);
                
                // Process user's input
                switch(input)
                {
                    // Case: Order Stuff
                    case "0":
                        TargetMenu = new OrderMenu();
                        break;
                    // Case: Add Product
                    case "1":
                        TargetMenu = new EditProductMenu();
                        break;
                    // Case: Exit
                    case "2":
                        repeat = false;
                        break;
                    default:
                        System.Console.WriteLine("Invalid Entry");
                        break;
                        
                }
                if (TargetMenu != null)
                    TargetMenu.Start();
            } while (repeat);
        }
    }
}