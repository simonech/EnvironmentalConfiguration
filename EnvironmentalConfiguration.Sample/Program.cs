using System;
using System.Configuration;

namespace EnvironmentalConfiguration.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string key1 = ConfigurationManager.AppSettings["myKey"];
            Console.WriteLine(key1);
            Console.ReadLine();
        }
    }
}
