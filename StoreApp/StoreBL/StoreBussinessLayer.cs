using System.Collections.Generic;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public class StoreBussinessLayer : StoreBLInterface
    {
        private IRepo _repo;
        public StoreBussinessLayer(IRepo repo)
        {
            _repo = repo;
        }
        public List<Product> GetInventory()
        {
            return _repo.GetProducts();
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
            return _repo.AddCustomer(customer);
        }
        public List<Customer> GetCustomers()
        {
            return _repo.GetCustomers();
        }
    }
}