using System;

namespace StoreModels
{
    public class ErrorLog
    {
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public ErrorLog(string recieved) 
        {
            this.Message = recieved;
            this.DateCreated = DateTime.Now;
        }
    }
}