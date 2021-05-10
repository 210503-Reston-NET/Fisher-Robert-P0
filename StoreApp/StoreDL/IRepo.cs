using StoreModels;
using System.Collections.Generic;

namespace StoreDL
{
    public interface IRepo
    {
        // NOTE!!: Many of these ideas are repeated and coule be coupled into an Abstract or Interface
        List<Product> GetProducts();
        Product GetProduct(string product);
        bool AddProduct(Product product);
        List<Product> GetProductsFor(Location Location);   
        Customer GetCustomer(Customer customer);
        Order GetOrder(Order order);
        bool AddOrder(Order order);
        List<Order> GetOrdersFor(Customer Customer);
        List<Order> GetOrdersFor(Location Location);
        bool AddCustomer(Customer customer);
        List<Customer> GetCustomers();
    }
}