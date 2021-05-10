using System.Collections.Generic;
using StoreModels;
using System.IO; 
using System.Text.Json; 
using System;
using System.Linq;


namespace StoreDL
{
    public abstract class Repo
    {
        private string file;
        private string JsonString;
        public Repo(string type)
        {
            this.file = "../" + type + ".Json";
        }
        public List<Object> GetItems()
        {
            try
            {
                JsonString = File.ReadAllText(file);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new List<Object>();
            }
            return JsonSerializer.Deserialize<List<Object>>(JsonString);
        }

        public Object getItem(Object obj)
        {
            return GetItems().FirstOrDefault(item => item.Equals(obj));
        }

        public bool AddItem(Object obj)
        {
            List<Object> restaurantsFromFile = GetItems();
            restaurantsFromFile.Add(obj);
            JsonString = JsonSerializer.Serialize(restaurantsFromFile);
            File.WriteAllText(file, JsonString);
            return true;
        }
    }
}