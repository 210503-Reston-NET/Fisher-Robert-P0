using System.Collections.Generic;
using StoreModels;

namespace StoreBL
{
    public interface StoreBLInterface
    {
        List<Inventory> GetInventory (int storeId);
        Inventory GetInventory (int storeId, string ISBN);
        bool AddProduct(Product product);
        Product GetProduct(string ISBN);
        bool RemoveProduct(Product product);
        bool UpdateProduct(Product EditedProduct);
        bool UpdateInventory(Inventory inventory);
        List<Product> GetAllProducts();
        User GetUser(string UserName, string Password);
        List<User> GetAllUsers();
        bool AddUser(User user);
        Order AddOrder(Order order);
        bool AddTransaction(Transaction transact);
        Store GetStore(Store TargetStore);
        List<Store> GetAllStores();
        Order GetOrder(Order order);
        List<Order> GetAllOrders(int storeID);
        List<Order> GetAllOrders(User user);
        List<Inventory> GetInventoryFor(int storeID);
        List<Transaction> GetTransactions(int OrderNumber);
    }
}