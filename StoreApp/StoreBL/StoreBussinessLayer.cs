using System.Collections.Generic;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public class StoreBussinessLayer : StoreBLInterface
    {
        private DAO _repo;
        private RepoDB _repoDB;
        public StoreBussinessLayer(RepoDB repo)
        {
            _repoDB = repo;
        }
        public List<Product> GetInventory()
        {
            return _repoDB.GetProducts();
        }
        public bool AddProduct(Product product)
        {
            return _repo.AddProduct(product);
        }
        public bool AddOrder(Order order)
        {
            return _repo.AddOrder(order);
        }
        public bool AddCustomer(Customer customer)
        {
            return _repoDB.AddCustomer(customer);
        }
        public List<Customer> GetAllCustomers()
        {
            return _repoDB.GetCustomers();
        }

        public Customer GetCustomer(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public Product GetProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            return _repoDB.GetProducts();
        }

        public User GetUser(string UserName, string UserPassword)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }
    }
}