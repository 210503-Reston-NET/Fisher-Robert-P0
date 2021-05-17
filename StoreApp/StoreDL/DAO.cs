using StoreModels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StoreDL
{
    public interface DAO
    {
        // NOTE!!: Many of these ideas are repeated and coule be coupled into an Abstract or Interface
        List<Product> GetProducts();
        Product GetProduct(Product product);
        bool AddProduct(Product product); 
        Customer GetCustomer(Customer customer);
        Order GetOrder(Order order);
        bool AddOrder(Order order);
        List<Order> GetOrdersFor(Customer Customer);
        bool AddCustomer(Customer customer);
        List<Customer> GetCustomers();
        bool AddStore(Store store);
        Store GetStore(Store store);
        List<Store> GetAllStores();
        List<User> GetAllUsers();
        List<Tuple<Product, int>> getInventory(int StoreID);
        List<Tuple<Product, int>> GetTransactions(int OrderNumber);
    }
}