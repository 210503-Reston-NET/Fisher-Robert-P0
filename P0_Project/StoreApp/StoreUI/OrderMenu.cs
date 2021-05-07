using StoreModels;
using System.Collections.Generic;
using System;

namespace StoreUI
{
    public class OrderMenu : StoreMenu
    {
        public void Start()
        {
            bool repeat = true;
            List<Product> Products = new List<Product>(); 
            Products.Add(new Product(3.49, "1111111111111", "Toot Paste"));
            Products.Add(new Product(3.49, "1111111111111", "Toot Paste"));
            Products.Add(new Product(3.49, "1111111111111", "Toot Paste"));

            int index = 0;
            do
            {
                System.Console.WriteLine("------------OrderMenu Page------------");
                System.Console.WriteLine("Add whatever items you like!");

                foreach (Product product in Products)
                {
                    System.Console.WriteLine("[" + index + "] : " + product.ToString());
                    index++;
                }

                System.Console.WriteLine("[" + index + "] Exit");
                string UserInput = Console.ReadLine();
                
                try
                {
                    int selector = int.Parse(UserInput);
                }
                catch(Exception)
                {
                    System.Console.WriteLine("Please insert a number between 0 and " + index);
                }

                
                
            } while (repeat);
        }
    }
}