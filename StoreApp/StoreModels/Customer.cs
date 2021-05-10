namespace StoreModels
{
    public class Customer : User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Customer(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        public override string ToString()
        {
            return "Username: " + this.UserName + "\tPassword: " + this.Password;
        }

        
    }
}