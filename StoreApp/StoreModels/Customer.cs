namespace StoreModels
{
    public class Customer : User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustID { get; set; }
        public int Code { get; set; }
        public Customer () 
        {
            
        }
        public Customer (string FirstName, string LastName, int Code)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Code = Code;
        }
        public Customer(string UserName, string Password, string FirstName, string LastName)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        public Customer(string Name, string Pass, string First, string Last, int code) : this(Name, Pass, First, Last)
        {
            this.Code=code;
        }
        public Customer(string Name, string Pass, string First, string Last, int code, int CustID) : this(Name, Pass, First, Last, CustID)
        {
            this.CustID=CustID;
        }

        public override string ToString()
        {
            return "Username: " + this.UserName + "\tPassword: " + this.Password;
        }

        
    }
}