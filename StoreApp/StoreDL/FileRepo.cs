using System.Collections.Generic;
using StoreModels;
using System.IO; 
using System.Text.Json; 
using System;
using System.Linq;

namespace StoreDL
{
    public class FileRepo : IRepo
    {
        private const string ProductFilePath = "../StoreDL/Products.json";
        private const string OrderFilePath = "../StoreDL/Orders.json";
        private const string CustomerFilePath = "../StoreDL/Customers.json";
        private string JsonString;
        /// <summary>
        /// Returns all products recorded
        /// </summary>
        public List<Product> GetProducts()
        {
            try
            {
                JsonString = File.ReadAllText(ProductFilePath);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new List<Product>();
            }
            return JsonSerializer.Deserialize<List<Product>>(JsonString);
        }
        /// <summary>
        /// Returns the product found given the specific ISBN
        /// </summary>
        public Product GetProduct(string ISBN)
        {
            return GetProducts().FirstOrDefault(pr => pr.ISBN.Equals(ISBN));
        }
        /// <summary>
        /// Creates an instance of a certain product and then writes it down
        /// </summary>
        public bool AddProduct(Product product)
        {
            try {
                List<Product> ProductsFromFile = GetProducts();
                ProductsFromFile.Add(product);
                JsonString = JsonSerializer.Serialize(ProductsFromFile);
                File.WriteAllText(ProductFilePath, JsonString);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public List<Product> GetProductsFor(Location Location)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        public List<Customer> GetCustomers()
        {
            try
            {
                JsonString = File.ReadAllText(CustomerFilePath);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new List<Customer>();
            }
            return JsonSerializer.Deserialize<List<Customer>>(JsonString);
        }

        public Order GetOrder(string OrderNumber)
        {
            throw new NotImplementedException();
        }
        public List<Order> GetOrders()
        {
            try
            {
                JsonString = File.ReadAllText(OrderFilePath);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new List<Order>();
            }
            return JsonSerializer.Deserialize<List<Order>>(JsonString);
        }

        public List<Order> GetOrdersFor(Customer Customer)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersFor(Location Location)
        {
            throw new NotImplementedException();
        }

        public bool AddOrder(Order order)
        {
            try {
                List<Order> ProductsFromFile = GetOrders();
                ProductsFromFile.Add(order);
                JsonString = JsonSerializer.Serialize(ProductsFromFile);
                File.WriteAllText(OrderFilePath, JsonString);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool AddCustomer(Customer customer)
        {
            try {
                List<Customer> CustomersFromFile = GetCustomers();
                CustomersFromFile.Add(customer);
                JsonString = JsonSerializer.Serialize(CustomersFromFile);
                File.WriteAllText(CustomerFilePath, JsonString);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public Customer GetCustomer(string CustomerID)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}