using System.Collections.Generic;
using StoreModels;

namespace StoreBL
{
    public interface StoreBLInterface
    {
        List<Inventory> GetInventory (int storeId);
        bool AddProduct(Product product);
        Product GetProduct(string ISBN);
        bool RemoveProduct(Product product);
        bool UpdateProduct(Product EditedProduct);
        List<Product> GetAllProducts();
        User GetUser(string UserName, string Password);
        List<User> GetAllUsers();
        bool AddUser(User user);
        bool AddOrder(Order order);
        bool AddTransaction(Transaction transact);
        Store GetStore(Store TargetStore);
        Order GetOrder(Order order);
    }
}