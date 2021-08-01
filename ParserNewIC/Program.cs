using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ParserNewIc;
using System;
using System.Linq;

namespace ParserNewIC
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            using (var driver = new FirefoxDriver())
            {
                var linq = "https://ua.e-cat.intercars.eu/uk/%D0%92%D1%81%D0%B5-%D0%BA%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D0%B8%D0%B8/%D0%94%D0%B2%D0%B8%D0%B3%D1%83%D0%BD/%D0%91%D0%BB%D0%BE%D0%BA-%D0%B4%D0%B2%D0%B8%D0%B3%D1%83%D0%BD%D0%B0/%D0%91%D0%BB%D0%BE%D0%BA-%D0%B4%D0%B2%D0%B8%D0%B3%D1%83%D0%BD%D0%B0/c/tecdoc-6200000-6211000-6010113?q=%3Adefault%3AbranchAvailability%3AALL&page=0&sort=default";
                
                var userAuthorization = new UserAuthorization(linq, driver);

                userAuthorization.Login();

                var i = 28;

                while (i<40)
                {
                    Console.WriteLine(i);

                    var link = $"https://ua.e-cat.intercars.eu/uk/%D0%92%D1%81%D0%B5-%D0%BA%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D0%B8%D0%B8/%D0%94%D0%B2%D0%B8%D0%B3%D1%83%D0%BD/%D0%91%D0%BB%D0%BE%D0%BA-%D0%B4%D0%B2%D0%B8%D0%B3%D1%83%D0%BD%D0%B0/%D0%91%D0%BB%D0%BE%D0%BA-%D0%B4%D0%B2%D0%B8%D0%B3%D1%83%D0%BD%D0%B0/c/tecdoc-6200000-6211000-6010113?q=%3Adefault%3AbranchAvailability%3AALL&page={i.ToString()}&sort=default";

                    var productLoader = new ProductLoader(link, driver);
                
                    var products = productLoader.LoadProductsFromLinq().ToList();

                    i++;
                }
                      
                
            }
            
            

            
            
            

            
            
        }
    }
}
