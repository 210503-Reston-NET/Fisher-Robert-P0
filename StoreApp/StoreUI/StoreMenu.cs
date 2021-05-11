using StoreModels;

namespace StoreUI
{
    public class StoreMenu : Accessible
    {
        public User CurrentUser { get; set; }

        public virtual void Start()
        {
        }
    }
}