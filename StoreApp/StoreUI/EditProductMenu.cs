using StoreBL;
using StoreDL;
using StoreModels;
using System;
using System.Collections.Generic;


namespace StoreUI
{
    public class EditProductMenu : StoreMenu
    {
        StoreBussinessLayer bussinessLayer = new StoreBussinessLayer(new FileRepo());
        MyValidate validate = new StringValidator();
        public override void Start()
        {
            string output = "--------Edit Product--------" + "\n";
                output += "Please make a selection." + "\n";
                output += "[0] Add Product." + "\n";
                output += "[1] Remove Product." + "\n";
                output += "[2] Edit Product." + "\n";
                output += "[3] Exit." + "\n";
                string input = validate.ValidateString(output);

                switch(input)
                {
                    // Case: Add Product
                    case "0":
                        Product item = new Product();
                
                        output = "Enter product name: " + "\n";
                        item.Name = validate.ValidateString(output);

                        output = "Enter product ISBN: " + "\n";
                        item.ISBN = validate.ValidateString(output);

                        output = "Enter product Price: " + "\n";
                        item.Price = validate.ValidateDouble(output);

                        bussinessLayer.AddProduct(item);
                        break;
                    
                    // Case: Remove Product
                    case "1":
                        output = "Enter product ISBN: " + "\n";
                        string isbn_13 = validate.ValidateString(output);

                        
                        break;
                    // Case Invalid Entry
                    default:
                        System.Console.WriteLine("Invalid entry! Please try again.");
                        break;
                }
        }
    }
}