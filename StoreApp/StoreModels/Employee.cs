namespace StoreModels
{
    public abstract class Employee : User
    {
        public string EmployeeID { get; set; }
        public bool IsManager { get; set; }
    
    }
}