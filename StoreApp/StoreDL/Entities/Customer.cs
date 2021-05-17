using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Employees = new HashSet<Employee>();
            Orders = new HashSet<Order>();
        }

        public int CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Code { get; set; }
        public string UserName { get; set; }

        public virtual Account UserNameNavigation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
