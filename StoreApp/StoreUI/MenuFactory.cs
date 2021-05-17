using StoreModels;
using StoreDL.Entities;
using StoreBL;
using StoreDL;  
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StoreUI
{
    /// <summary>
    /// Class to menufacture menus using factory dp
    /// </summary>

    public class MenuFactory
    {
        public static StoreMenu GetMenu(string menuType, User CurrentUser)
        {
            // getting configurations from a config file
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            // setting up my db connections
            string connectionString = configuration.GetConnectionString("BearlyCampingDB");
            // we're building the dbcontext using the constructor that takes in options, we're setting the connection
            // string outside the context def'n
            DbContextOptions<BearlyCampingDataContext> options = new DbContextOptionsBuilder<BearlyCampingDataContext>()
            .UseSqlServer(connectionString)
            .Options;
            // passing the options we just built
            var context = new BearlyCampingDataContext(options);

            StoreBLInterface BussinessLayer = new StoreBussinessLayer(new RepoDB(context));

            switch (menuType.ToLower())
            {
                case "login":
                    return new LoginMenu(BussinessLayer);
                case "editproduct":
                    return new EditProductMenu(BussinessLayer);
                case "home":
                    return new HomeMenu(CurrentUser);
                case "order":
                    return new OrderMenu(BussinessLayer, CurrentUser);
                default:
                    return null;
            }
        }
    }
}