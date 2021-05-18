using System.Collections.Generic;
using Models = StoreModels;
using Entity = StoreDL.Entities;
using System.Linq;
using System;
using System.Data;
using StoreModels;

namespace StoreDL
{
    public class RepoDB : DAO
    {
        public Entity.BearlyCampingDataContext _context;
        public RepoDB(Entity.BearlyCampingDataContext context)
        {
            _context = context;
        }

        public Models.Order AddOrder(Models.Order order)
        {
            try
            {
                _context.Orders.Add(new Entity.Order
                {
                        DateCreated = order.Create,
                        UserName = order.UserName,
                        StoreId = order.StoreID,
                        Total = order.Total
                }
                );
                _context.SaveChanges();
                return GetOrder(order);
            } catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool AddTransaction(Models.Transaction transact)
        {
            try {
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
                System.Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool AddProduct(Models.Product product)
        {
            try
            {
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
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddStore(Models.Store store)
        {
            try
            {
                _context.Stores.Add(
                    new Entity.Store
                    {
                        StoreCity = store.StoreCity,
                        StoreState = store.StoreState
                    }
                );
                foreach(Models.Inventory inventory in store.Inventory)
                {
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
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddUser(Models.User user)
        {
            try {
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
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Models.Store> GetAllStores()
        {
            return _context.Stores
            .Select(
                store => new Models.Store(store.StoreCity, store.StoreCity, getInventory(store.StoreId), store.StoreId)
            ).ToList();
        }

        public List<Models.User> GetAllUsers()
        {
            return _context.Accounts
            .Select(
                account => new Models.User(account.UserName, account.UserPassword, account.FirstName, account.LastName, account.EmployeeId, account.Created)
            ).ToList();
        }

        public List<Models.Inventory> getInventory(int StoreID)
        {
            return _context.Inventories.Where(
                store => store.StoreId == StoreID
                ).Select(
                    Inventory => new Models.Inventory()
                    {
                        ISBN = Inventory.Isbn,
                        StoreID = Inventory.StoreId,
                        Quantity = Inventory.Quantity
                    }
                ).ToList();
        }

        public Models.Order GetOrder(Models.Order order)
        {
            List<Models.Order> found = _context.Orders.Where(DBOrder => DBOrder.StoreId == order.StoreID &&
            DBOrder.UserName == order.UserName && DBOrder.DateCreated == order.Create).Select(
                DBOrder => new Models.Order()
                {
                    OrderNumber = DBOrder.OrderNumber,
                    StoreID = DBOrder.StoreId,
                    UserName = DBOrder.UserName,
                    Total = DBOrder.Total,
                    Create = DBOrder.DateCreated
                }).ToList();
            return found[0];
        }

        public List<Models.Order> GetOrdersFor(Models.User customer)
        {
            return _context.Orders.Where(
                order => order.UserName == customer.UserName).Select(
                    order => new Models.Order()
                    {
                        OrderNumber = order.OrderNumber,
                        UserName = customer.UserName,
                        StoreID = order.StoreId,
                        Transactions = GetTransactions(order.OrderNumber),
                        Total = order.Total
                    }
                ).ToList();
        }

        public Models.Product GetProduct(Models.Product item)
        {
            return GetProduct(item.ISBN);
        }
        public Models.Product GetProduct(string ISBN)
        {
            Entity.Product found = _context.Products.FirstOrDefault(product => product.Isbn == ISBN);

            if (found == null) return null;
            return new Models.Product(found.Price, found.Isbn, found.ProductName);
        }

        public List<Models.Product> GetProducts()
        {
            return _context.Products.Select(
                product => new Models.Product()
                { Price = product.Price,
                ISBN = product.Isbn,
                Name = product.ProductName
                }
            ).ToList();
        }


        public Models.Store GetStore(Models.Store store)
        {
            Entity.Store found = _context.Stores.FirstOrDefault(DBStore => DBStore.StoreCity == store.StoreCity &&
            DBStore.StoreState == DBStore.StoreState);

            if (found == null) return null;
            List<Models.Inventory> inventory = getInventory(found.StoreId);
            return new Models.Store(found.StoreCity, found.StoreState, inventory, found.StoreId);
        }

        public List<Models.Transaction> GetTransactions(int OrderNumber)
        {
            return _context.Transactions.Where(
                transaction => transaction.OrderNumber == OrderNumber
            ).Select(
                transaction => new Models.Transaction()
                {
                    ISBN = transaction.Isbn,
                    OrderNumber = transaction.OrderNumber,
                    Quantity = transaction.Quantity
                }
            ).ToList();
        }

        public Models.User GetUser(string UserName)
        {
            Entity.Account found = _context.Accounts.FirstOrDefault(user => user.UserName == UserName);

            if(found == null) return null;
            return new Models.User(found.UserName, found.UserPassword, found.FirstName, found.LastName, found.EmployeeId, found.Created);

        }

        public bool RemoveProduct(Product product)
        {
            Entity.Product found = _context.Products.First(prod => prod.Isbn == product.ISBN);
            _context.Products.Remove(found);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateProduct(Product updatedProduct)
        {
            Entity.Product found = _context.Products.Find(updatedProduct.ISBN);
            if (found != null){
                found.ProductName = updatedProduct.Name;
                found.Price = updatedProduct.Price;
                _context.SaveChanges();
            }
            return true;
        }
    }
}