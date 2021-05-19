using System.Collections.Generic;
using Models = StoreModels;
using Entity = StoreDL.Entities;
using System.Linq;
using System;
using System.Data;
using StoreModels;
using Serilog;

namespace StoreDL
{
    public class RepoDB : DAO
    {
        public Entity.BearlyCampingDataContext _context;
        public RepoDB(Entity.BearlyCampingDataContext context)
        {
            _context = context;

            // Initialize Serilogger
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("../logs/StoreApp.txt", rollingInterval : RollingInterval.Day)
            .CreateLogger();
        }

        public Models.Order AddOrder(Models.Order order)
        {
            try
            {
                Log.Debug("Attempting to Persist order: {} to Database.");
                Entity.Order toBeAdded = new Entity.Order(){
                    DateCreated = order.Create,
                        UserName = order.UserName,
                        StoreId = order.StoreID,
                        Total = order.Total
                };
                _context.Orders.Add(toBeAdded);
                _context.SaveChanges();
                
                order.OrderNumber = toBeAdded.OrderNumber;
                return order;
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to add order: " + order +" to database.");
                return null;
            }
        }

        public bool AddTransaction(Models.Transaction transact)
        {
            try {
            Log.Debug("Attempting to persist transaction: " + transact + " to database.");
            _context.Transactions.Add(
                new Entity.Transaction
                {
                    OrderNumber = transact.OrderNumber,
                    Isbn = transact.ISBN,
                    Quantity = transact.Quantity
                }
            );
            _context.SaveChanges();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to persist transaction: " + transact + " to database");
                return false;
            }
            return true;
        }

        public bool AddProduct(Models.Product product)
        {
            try
            {
                Log.Debug("Attempting to persist transaction: " + product + " to database.");
                _context.Products.Add(
                    new Entity.Product
                    {
                        Isbn = product.ISBN,
                        ProductName = product.Name,
                        Price = product.Price
                    }
                );
                _context.SaveChanges();
                return true;
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to persist transaction: " + product + " to database");
                return false;
            }
        }

        public bool AddStore(Models.Store store)
        {
            try
            {
                Log.Debug("Attempting to persist Store: " + store + " to database.");
                _context.Stores.Add(
                    new Entity.Store
                    {
                        StoreCity = store.StoreCity,
                        StoreState = store.StoreState
                    }
                );
                foreach(Models.Inventory inventory in store.Inventory)
                {
                    Log.Debug("Attempting to persist Inventory: " + inventory + " to database.");
                    _context.Inventories.Add(
                        new Entity.Inventory
                        {
                        StoreId = store.storeID,
                        Isbn = inventory.ISBN,
                        Quantity = inventory.Quantity
                        }
                    );
                }
                _context.SaveChanges();

                GetStore(store);
                return true;
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to persist Store: " + store + " as well as its inventory to database.");
                return false;
            }
        }

        public bool AddUser(Models.User user)
        {
            try {
                Log.Debug("Attempting to persist User: " + user + " to database.");
            _context.Accounts.Add(
                new Entity.Account
                {
                    UserName = user.UserName,
                    UserPassword = user.Password,
                    Created = user.created,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeId = user.Code,
                    
                }
            );
            _context.SaveChanges();
            return true;
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to persist User: " + user + " to database.");
                return false;
            }
        }

        public List<Models.Store> GetAllStores()
        {
            List<Models.Store> models;
            try{
            Log.Debug("Attempting to retrieve Stores from the database.");
            models = _context.Stores
            .Select(
                store => new Models.Store(store.StoreCity, store.StoreState, store.StoreId)
            ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Stores from database.");
                return null;
            }
            return models;
        }

        public List<Models.User> GetAllUsers()
        {
            List<Models.User> users;
            try{
            Log.Debug("Attempting to retrieve Users from the database.");
            users = _context.Accounts
            .Select(
                account => new Models.User(account.UserName, account.UserPassword, account.FirstName, account.LastName, account.EmployeeId, account.Created)
            ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Users from database.");
                return null;
            }
            return users;
        }

        public List<Models.Inventory> getInventory(int StoreID)
        {
            List<Models.Inventory> inventory;
            try{
            Log.Debug("Attempting to retrieve Inventory from the database.");
            inventory = _context.Inventories.Where(
                store => store.StoreId == StoreID
                ).Select(
                    Inventory => new Models.Inventory()
                    {
                        ISBN = Inventory.Isbn,
                        StoreID = Inventory.StoreId,
                        Quantity = Inventory.Quantity
                    }
                ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Inventory from database.");
                return null;
            }
            return inventory;
        }

        public Inventory GetInventory(int storeID, string Isbn){
            Entity.Inventory inventory;
            try{
            Log.Debug("Attempting to retrieve Inventory from the database.");
            inventory = _context.Inventories.First(invent => invent.StoreId == storeID && invent.Isbn == Isbn);
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Inventory from database.");
                return null;
            }
            return new Inventory()
            {
                ISBN = inventory.Isbn,
                StoreID = inventory.StoreId,
                Quantity = inventory.Quantity
            };
        }

        public Models.Order GetOrder(Models.Order order)
        {
            Entity.Order found;
            try {
            Log.Debug("Attempting to retrieve Inventory from the database.");
            found = _context.Orders.FirstOrDefault(DBOrder => DBOrder.OrderNumber == order.OrderNumber && DBOrder.StoreId == order.StoreID &&
            DBOrder.UserName == order.UserName);

            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Inventory from database.");
                return null;
            }
            return new Order(){
                OrderNumber = found.OrderNumber,
                StoreID = found.StoreId,
                Total = found.Total,
                Create = found.DateCreated,
                UserName = found.UserName
            };
        }

        public List<Models.Order> GetOrdersFor(int storeID)
        {
            List<Models.Order> orders;
            try{
            Log.Debug("Attempting to retrieve list of orders from the database.");
            orders = _context.Orders.Where(
                order => order.StoreId == storeID).Select(
                    order => new Models.Order()
                    {
                        OrderNumber = order.OrderNumber,
                        UserName = order.UserName,
                        StoreID = storeID,
                        Total = order.Total
                    }
                ).ToList();
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve list of orders from database.");
                return null;
            }
            return orders;

        }
        public List<Models.Order> GetOrdersFor(Models.User customer)
        {
            List<Models.Order> orders;
            try{
            Log.Debug("Attempting to retrieve list of orders from the database.");
            orders = _context.Orders.Where(
                order => order.UserName == customer.UserName).Select(
                    order => new Models.Order()
                    {
                        OrderNumber = order.OrderNumber,
                        UserName = customer.UserName,
                        StoreID = order.StoreId,
                        Total = order.Total
                    }
                ).ToList();
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve list of orders from database.");
                return null;
            }
            return orders;
        }

        public Models.Product GetProduct(Models.Product item)
        {
            return GetProduct(item.ISBN);
        }
        public Models.Product GetProduct(string ISBN)
        {
            Entity.Product found;
            try {
            Log.Debug("Attempting to retrieve product with ISBN: " + ISBN + " from the database.");
            found = _context.Products.FirstOrDefault(product => product.Isbn == ISBN);
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve product with ISBN: " + ISBN +" from database.");
                return null;
            }
            return new Models.Product(found.Price, found.Isbn, found.ProductName);
        }

        public List<Models.Product> GetProducts()
        {
            List<Models.Product> products;
            try{
            Log.Debug("Attempting to retrieve list of products from the database.");
            products = _context.Products.Select(
                product => new Models.Product()
                { Price = product.Price,
                ISBN = product.Isbn,
                Name = product.ProductName
                }
            ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve a list of products from database.");
                return null;
            }
            return products;
        }


        public Models.Store GetStore(Models.Store store)
        {
            Entity.Store found;
            try {
            Log.Debug("Attempting to retrieve Store: " + store + " from the database.");
            found = _context.Stores.FirstOrDefault(DBStore => DBStore.StoreCity == store.StoreCity &&
            DBStore.StoreState == DBStore.StoreState);
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Store: " + store + " from database.");
                return null;
            }

            List<Models.Inventory> inventory = getInventory(found.StoreId);
            return new Models.Store(found.StoreCity, found.StoreState, inventory, found.StoreId);
        }

        public List<Models.Transaction> GetTransactions(int OrderNumber)
        {
            List<Models.Transaction> transactions;
            try {
            Log.Debug("Attempting to retrieve Transaction related to OrderNumber: " + OrderNumber + " from the database.");
            transactions = _context.Transactions.Where(
                transaction => transaction.OrderNumber == OrderNumber
            ).Select(
                transaction => new Models.Transaction()
                {
                    ISBN = transaction.Isbn,
                    OrderNumber = transaction.OrderNumber,
                    Quantity = transaction.Quantity
                }
            ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message ,"Failed to retrieve Transaction related to OrderNumber: " + OrderNumber + " from the database.");
                return null;
            }
            return transactions;
        }

        public Models.User GetUser(string UserName)
        {
            Entity.Account found;
            try {
            Log.Debug("Attempting to retrieve User: " + UserName + " from the database.");
            found = _context.Accounts.FirstOrDefault(user => user.UserName == UserName);
            } catch(Exception e)
            {
                Log.Error(e.Message ,"Failed to retrieve User: " + UserName + " from the database.");
                return null;
            }

            Models.User account = new Models.User(found.UserName, found.UserPassword, found.FirstName, found.LastName, found.EmployeeId, found.Created);
            if (account == null) return null;
            return account;
        }

        public bool RemoveProduct(Product product)
        {
            Entity.Product found;
            try {
            Log.Debug("Attempting to retrieve Product: " + product + " from the database.");
            found = _context.Products.First(prod => prod.Isbn == product.ISBN);
            _context.Products.Remove(found);
            } catch(Exception e)
            {
            Log.Error(e.Message ,"Failed to retrieve Product: " + product + " from the database.");
            return false;
            }
            _context.SaveChanges();
            return true;
        }

        public bool UpdateProduct(Product updatedProduct)
        {
            Entity.Product found;
            try {
            Log.Debug("Attempting to Update Product: " + updatedProduct + " from the database.");
            found = _context.Products.Find(updatedProduct.ISBN);
            if (found != null){
                found.ProductName = updatedProduct.Name;
                found.Price = updatedProduct.Price;
                _context.SaveChanges();
            }
            } catch (Exception e)
            {   
                Log.Error(e.Message ,"Failed to Update Product: " + updatedProduct + " from the database.");
                return false;
            }
            return true;
        }

        public bool UpdateInventory(Inventory inventory)
        {   
            Entity.Inventory found;
            try{
            Log.Debug("Attempting to Update Inventory: " + inventory + " from the database.");
            found = _context.Inventories.FirstOrDefault(inv => inv.Isbn == inventory.ISBN && inv.StoreId == inventory.StoreID);
            found.Quantity = inventory.Quantity;
            found.Isbn = inventory.ISBN;
            found.StoreId = inventory.StoreID;
            } catch (Exception e)
            {
                Log.Error(e.Message , "Failed to Update Inventory: " + inventory + " from the database.");
                return false;
            }
            _context.SaveChanges();
            return true;
        }
    }
}