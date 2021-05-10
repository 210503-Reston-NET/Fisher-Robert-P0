using System.Collections.Generic;
using StoreModels;

namespace StoreBL
{
    public interface StoreBLInterface
    {
        List<Product> GetInventory ();
    }
}