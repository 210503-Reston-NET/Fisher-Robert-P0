using System.Collections.Generic;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public class CustomerBL : StoreBLInterface
    {
        public CustomerRepo _repo;
        public CustomerBL(CustomerRepo repo)
        {
            _repo = repo;
        }
        public bool AddCustomer(Customer customer)
        {
            return _repo.AddCustomer(customer);
        }
        public List<Product> GetInventory()
        {
            throw new System.NotImplementedException();
        }
    }
}