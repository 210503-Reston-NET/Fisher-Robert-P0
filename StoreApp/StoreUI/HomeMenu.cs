using System;
using System.Collections.Generic;

namespace StoreUI
{
    public class HomeMenu : StoreMenu
    {
        public bool IsManager { get; set; } = false;
        public override void Start()
        {
            StoreMenu TargetMenu = null;
            List<StoreMenu> Options = new List<StoreMenu>();
            StringValidator validate = new StringValidator();
            bool repeat = true;
            do
            {
                // Current Menu selector using Console as an output
                int index = 0;
                string output = "Welcome to the main store page!" + "\n";
                output += "Please make a selection." + "\n";
                output += "["+ index++ +"] Order Product." + "\n";
                Options.Add(new OrderMenu());

                if (this.CurrentUser.IsManager){
                    output += "["+ index++ +"] Add Product." + "\n";
                    Options.Add(new EditProductMenu());
                }
                
                output += "["+ index +"+] Exit." + "\n";

                int input = validate.ValidateInteger(output);

                if(input >= index)
                    break;
                
                TargetMenu = Options[input];
                
                if (TargetMenu != null)
                    TargetMenu.CurrentUser = this.CurrentUser;
                    TargetMenu.Start();
            } while (repeat);

            System.Console.WriteLine("Thanks for stopping by. Have a great day!");
        }
    }
}