using System;

namespace StoreUI
{
    public class HomeMenu : StoreMenu
    {
        public void Start()
        {
            StoreMenu TargetMenu;
            bool repeat = true;
            do
            {
                // Current Menu selector using Console as an output
                System.Console.WriteLine("Welcome to the main store page!");
                System.Console.WriteLine("Please make a selection.");
                System.Console.WriteLine("[0] Login.");
                System.Console.WriteLine("[1] Order Product.");
                System.Console.WriteLine("[2] Exit.");
                string input = Console.ReadLine();
                
                // Process user's input
                switch(input)
                {
                    case "0":
                        TargetMenu = null;
                        break;
                    case "1":
                        TargetMenu = new OrderMenu();
                        TargetMenu.Start();
                        break;
                    case "2":
                        repeat = false;
                        break;
                    default:
                        System.Console.WriteLine("Invalid Entry");
                        break;
                }
            } while (repeat);
        }
    }
}