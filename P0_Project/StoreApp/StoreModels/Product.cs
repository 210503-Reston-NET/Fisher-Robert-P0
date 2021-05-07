namespace StoreModels
{
    public class Product
    {
        public Product(double Price, string ISBN, string Name)
        {
            this.Price = Price;
            this.ISBN = ISBN;
            this.Name = Name;
        }

        public double Price { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
    
        public override string ToString()
        {
            return this.Name + " : $" + this.Price + "\n" + "ISBN: " + this.ISBN;
        }
        
    }
}