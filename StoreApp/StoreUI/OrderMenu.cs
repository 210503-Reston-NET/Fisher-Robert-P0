using StoreModels;
using StoreBL;
using StoreDL;
using System.Collections.Generic;
using System;


namespace StoreUI
{
    public class OrderMenu : StoreMenu
    {
        public override void Start()
        {
            // Create a List of products available to the User
            bool repeat = true;
            /// <summary>
            /// List of items available to the User
            /// </summary>
            /// <typeparam name="Product"></typeparam>
            /// <returns></returns>
            List<Product> Products = new List<Product>(); 
            StoreBussinessLayer bussinessLayer = new StoreBussinessLayer(new FileRepo());
            
            Products = bussinessLayer.GetInventory();
            // Populate the options with available options
            //Products.Add(new Product(3.49, "1111111111111", "Toot Paste"));
            

            /// <summary>
            /// A List of options chosen by the User
            /// </summary>
            /// <typeparam name="Product"></typeparam>
            /// <returns></returns>
            List<Product> Order = new List<Product>();

            // Populate options for User to purchase. 
            // Allows User to keep selecting options until they are ready to check out or
            // Inventory runs out.
            do
            {
                int index = 0;
                System.Console.WriteLine("------------OrderMenu Page------------");
                System.Console.WriteLine("Add whatever items you like!");

                foreach (Product product in Products)
                {
                    System.Console.WriteLine("[" + index++ + "] : " + product.ToString());
                }

                System.Console.WriteLine("[" + index + "] Exit");
                string UserInput = Console.ReadLine();
                
                try
                {
                    int selector = int.Parse(UserInput);

                    if (selector != index){
                        Product item = new Product();
                        Product selected = Products[selector];

                        item.Name = selected.Name;
                        item.ISBN = selected.ISBN;
                        item.Price = selected.Price;


                        Order.Add(item);
                    }

                    if (selector == index){
                        repeat = false;
                        System.Console.WriteLine("--------Your Order--------");
                        foreach (Product product in Order)
                            System.Console.WriteLine(product.ToString());
                    }

                }
                catch(Exception)
                {
                    System.Console.WriteLine("Please insert a number between 0 and " + index);
                }
                
            } while (repeat);
        }
    }
}