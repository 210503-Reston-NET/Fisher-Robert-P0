namespace StoreModels
{
    public class User
    {
        public int Code { get;  set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public User ()
        {
            this.Code = 1;
            this.UserName = "";
            this.Password = "";
        }
        public User (string userName, string password): this()
        {
            this.UserName = userName;
            this.Password = password;
        }
        public User (string userName, string password, int Code): this(userName, password)
        {
            this.Code = Code;
        }
    }
}