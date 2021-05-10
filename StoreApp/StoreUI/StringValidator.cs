using System;

namespace StoreUI
{
    public class StringValidator : MyValidate
    {
        public string ValidateString(string prompt)
        {
            string response;
            bool repeat;
            do
            {
                Console.WriteLine(prompt);
                response = Console.ReadLine();
                repeat = String.IsNullOrWhiteSpace(response);
                if (repeat) Console.WriteLine("Please input a non empty string");
            } while (repeat);
            return response;
        }
        public double ValidateDouble(string prompt)
        {
            double response = 0.0;
            bool repeat = true;
            do
            {
                try 
                {
                    Console.WriteLine(prompt);
                    response = double.Parse(Console.ReadLine());

                    repeat = false;
                } catch(Exception e)
                {
                    System.Console.WriteLine(e.Message);
                    System.Console.WriteLine("Please input a vaild Price (Include Integers and Decimal Points");
                }
            } while (repeat);
            return response;
        }
    }
}