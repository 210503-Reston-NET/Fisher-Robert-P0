using StoreModels;
using StoreBL;
using StoreDL;
using System.Collections.Generic;
using System;


namespace StoreUI
{
    public class OrderMenu : StoreMenu
    {
        StoreBLInterface bussinessLayer;
        MyValidate validate = new StringValidator();
        User CurrentUser;
        public OrderMenu(StoreBLInterface BL, User PassedUser)
        {
            bussinessLayer = BL;
            CurrentUser = PassedUser;
        }
        public override void Start()
        {
            // Create a List of products available to the User
            bool repeat = true;
            decimal total = 0.00M;
            string city;
            string state;
            List<Inventory> StoreStock;
            Store FoundStore = null;
            Store TargetStore = null;
            List<Transaction> OrderTransactions = new List<Transaction>();
            List<Product> Products = new List<Product>();



            string output = "--------Order Menu--------";
                output += "\n Welcome to the Order Menu!\n";
                System.Console.WriteLine(output);

            while (FoundStore == null){  
                output = "Please insert the City from where your purchasing.";
                city = validate.ValidateString(output);

                output = "Please insert the State from where your purchasing.";
                state = validate.ValidateString(output);

                TargetStore = new Store(city, state);
                FoundStore = bussinessLayer.GetStore(TargetStore);

                if (FoundStore == null)
                    System.Console.WriteLine("Im sorry, that is an invalid store, Please try again!");
            }
            
            do
            {
                int index = 0;
            // Populate the options with available options
            //Products.Add(new Product(3.49, "1111111111111", "Toot Paste"));


            // Populate options for User to purchase. 
            // Allows User to keep selecting options until they are ready to check out or
            // Inventory runs out.
            System.Console.WriteLine("------------OrderMenu Page------------");
            System.Console.WriteLine("Add whatever items you like!");

            StoreStock = bussinessLayer.GetInventory(FoundStore.storeID);

            foreach (Inventory inventory in StoreStock)
            {
                Product item = bussinessLayer.GetProduct(inventory.ISBN);
                Products.Add(item);
                System.Console.WriteLine("[" + index++ + "] : " + item.ToString() + "\tQTY: " + inventory.Quantity);
            }

            System.Console.WriteLine("[" + index + "] Exit");
            try
            {
                output = "Select from one of the items above by inserting the related number.";
                int selector = validate.ValidateInteger(output);

                if (selector != index){
                    bool added = false;
                    Product item = new Product();
                    Product selected = Products[selector];

                    item.Name = selected.Name;
                    item.ISBN = selected.ISBN;
                    item.Price = selected.Price;

                    foreach (Transaction transact in OrderTransactions)
                    {
                        if (transact.ISBN == item.ISBN){
                            transact.Quantity++;
                            added = true;
                        }
                    }
                    if (!added)
                        OrderTransactions.Add(new Transaction(item.ISBN, 1));

                    total += item.Price;
                }

                if (selector == index){
                    repeat = false;
                    System.Console.WriteLine("--------Your Order--------");

                    Order order= new Order(FoundStore.storeID, this.CurrentUser.UserName, total);
                    order.StoreID = FoundStore.storeID;
                    

                    try {
                    bussinessLayer.AddOrder(order);
                    Order neworder = bussinessLayer.GetOrder(order);
                    neworder.Transactions = OrderTransactions;

                    foreach (Transaction transact in neworder.Transactions){
                        transact.OrderNumber = neworder.OrderNumber;
                        bussinessLayer.AddTransaction(transact);
                    }

                    } catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                        System.Console.WriteLine("FAILED TO ADD ORDER!");
                    }

                    foreach (Transaction transact in OrderTransactions)
                        System.Console.WriteLine(bussinessLayer.GetProduct(transact.ISBN) + "\tQTY: " + transact);

                    System.Console.WriteLine("\nTotal: " + total);
                    System.Console.WriteLine();
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