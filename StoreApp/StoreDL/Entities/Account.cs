using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Account
    {
        public Account()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
        }

        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
