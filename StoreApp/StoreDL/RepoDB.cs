using System.Collections.Generic;
using StoreModels;
using Entity = StoreDL.Entities;
using System.Linq;
using System;
using System.Data;

namespace StoreDL
{
    public class RepoDB : DAO
    {
        public Entity.BearlyCampingDataContext _context;
        public RepoDB(Entity.BearlyCampingDataContext context)
        {
            _context = context;
        }
        public bool AddCustomer(Customer customer)
        {
            try { 
                _context.Customers.Add(
                new Entity.Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Code = customer.Code,
                    UserName = customer.UserName
                });
                _context.Accounts.Add(
                new Entity.Account
                {
                    UserName = customer.UserName,
                    UserPassword = customer.Password,
                    Created = DateTime.Now
                }
                );
                _context.SaveChanges();
                return true;
            } 
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(
                    new Entity.Order
                    {
                        DateCreated = DateTime.Now,
                        CustId = order.CustomerID,
                        StoreId = order.StoreID,
                        Total = order.Total
                    }
                );
                foreach(Tuple<Product, int> transact in order.Transactions)
                {
                    _context.Transactions.Add(
                        new Entity.Transaction
                        {
                            Isbn = transact.Item1.ISBN,
                            OrderNumber = order.OrderNumber,
                            Quantity = transact.Item2
                        }
                    );
                }
                _context.SaveChanges();
                return true;
            } catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddProduct(Product product)
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
                return true;
            } catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddStore(Store store)
        {
            try
            {
                _context.Stores.Add(
                    new Entity.Store
                    {
                        StoreAddress = store.Address,
                        StoreName = store.StoreName
                    }
                );
                foreach(Tuple<Product, int> inventory in store.Inventory)
                {
                    _context.Inventories.Add(
                        new Entity.Inventory{
                        StoreId = store.storeID,
                        Isbn = inventory.Item1.ISBN,
                        Quantity = inventory.Item2
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

        public List<Store> GetAllStores()
        {
            return _context.Stores
            .Select(
                store => new Store(store.StoreName, store.StoreAddress, getInventory(store.StoreId), store.StoreId)
            ).ToList();
        }

        public List<User> GetAllUsers()
        {
            return _context.Accounts
            .Select(
                account => new User(account.UserName, account.UserPassword)
            ).ToList();
        }

        public Customer GetCustomer(Customer customer)
        {
            Entity.Customer found = _context.Customers.FirstOrDefault(resto => resto.FirstName == customer.FirstName &&
            resto.LastName == customer.LastName && resto.Code == customer.Code);

            if (found == null) return null;
            return new Customer(found.FirstName, found.LastName, found.Code);
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers
            .Select(
                cust => new Customer(cust.FirstName, cust.LastName, cust.Code)
            ).ToList();
        }

        public List<Tuple<Product, int>> getInventory(int StoreID)
        {
            return _context.Inventories.Where(
                store => store.StoreId == StoreID
                ).Select(
                    Inventory => new Tuple<Product, int>

                        (GetProduct(Inventory.Isbn),
                        Inventory.Quantity)

                ).ToList();
        }

        public Order GetOrder(Order order)
        {
            Entity.Order found = _context.Orders.FirstOrDefault(DBOrder => DBOrder.OrderNumber == order.OrderNumber &&
            DBOrder.StoreId == order.StoreID && DBOrder.CustId == order.CustomerID && DBOrder.Total == order.Total);

            

            if (found == null) return null;
            return new Order(found.OrderNumber, found.StoreId, found.CustId, found.Total);
        }

        public List<Order> GetOrdersFor(Customer customer)
        {
            return _context.Orders.Where(
                order => order.CustId == customer.CustID).Select(
                    order => new Order()
                    {
                        OrderNumber = order.OrderNumber,
                        CustomerID = customer.CustID,
                        StoreID = order.StoreId,
                        Transactions = GetTransactions(order.OrderNumber),
                        Total = order.Total
                    }
                ).ToList();
        }

        public Product GetProduct(Product item)
        {
            return GetProduct(item.ISBN);
        }
        public Product GetProduct(string ISBN)
        {
            Entity.Product found = _context.Products.FirstOrDefault(product => product.Isbn == ISBN);

            if (found == null) return null;
            return new Product(found.Price, found.Isbn, found.ProductName);
        }

        public List<Product> GetProducts()
        {
            throw new System.NotImplementedException();
        }


        public Store GetStore(Store store)
        {
            Entity.Store found = _context.Stores.FirstOrDefault(DBStore => DBStore.StoreName == store.StoreName &&
            DBStore.StoreAddress == DBStore.StoreAddress);

            List<Tuple<Product, int>> inventory = getInventory(found.StoreId);

            if (found == null) return null;
            return new Store(found.StoreName, found.StoreAddress, inventory, found.StoreId);
        }

        public List<Tuple<Product, int>> GetTransactions(int OrderNumber)
        {
            return _context.Transactions.Where(
                transaction => transaction.OrderNumber == OrderNumber
            ).Select(
                transaction => new Tuple<Product, int>
                (GetProduct(transaction.Isbn), transaction.Quantity)
            ).ToList();
        }
    }
}