using System.Collections.Generic;
using StoreModels;
using System;

namespace StoreDL
{
    public class CustomerRepo : Repo
    {

        public CustomerRepo()
            :base("Customer")
        {
            
        }

        public Customer GetCustomer(Customer customer)
        {
            return (Customer)base.getItem(customer);
        }

        public List<Customer> GetCustomers()
        {
            List<Object> temp = (base.GetItems());
            List<Customer> result = new List<Customer>();

            foreach(Object obj in temp)
                result.Add((Customer)obj);

            return result;
        }
        public bool AddCustomer(Customer customer)
        {
            return base.AddItem(customer);
        }

    }
}