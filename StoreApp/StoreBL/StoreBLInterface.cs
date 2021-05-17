using System.Collections.Generic;
using StoreModels;

namespace StoreBL
{
    public interface StoreBLInterface
    {
        List<Product> GetInventory ();
        bool AddCustomer(Customer customer);
        Customer GetCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        bool AddProduct(Product product);
        Product GetProduct(Product product);
        List<Product> GetAllProducts();
        User GetUser(string UserName, string UserPassword);
        List<User> GetAllUsers();
    }
}