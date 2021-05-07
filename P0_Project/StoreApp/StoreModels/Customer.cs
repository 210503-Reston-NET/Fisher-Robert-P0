namespace StoreModels
{
    public class Customer : User
    {
        public Customer(string FirstName, string LastName, string Address)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.HomeAdress = Address;
        }
        
    }
}