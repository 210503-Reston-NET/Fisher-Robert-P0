using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public DateTime StartDate { get; set; }
        public int AccessLevel { get; set; }
        public string UserName { get; set; }
        public int? CustId { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Account UserNameNavigation { get; set; }
    }
}
