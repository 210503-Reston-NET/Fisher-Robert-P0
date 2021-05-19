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
        Product GetProduct(string product);
        bool AddProduct(Product product);
        bool RemoveProduct(Product product);
        Order GetOrder(Order order);
        Order AddOrder(Order order);
        List<Order> GetOrdersFor(User Customer);
        List<Order> GetOrdersFor(int storeID);
        bool AddStore(Store store);
        Store GetStore(Store store);
        List<Store> GetAllStores();
        List<User> GetAllUsers();
        List<Inventory> getInventory(int StoreID);
        Inventory GetInventory(int storeID, string Isbn);
        List<Transaction> GetTransactions(int OrderNumber);
        User GetUser(string UserName);
        bool AddUser(User user);
        bool UpdateProduct(Product product);
        bool UpdateInventory(Inventory inventory);
    }
}