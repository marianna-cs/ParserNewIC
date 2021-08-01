using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using ParserNewIc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ParserNewIC
{
    public class ProductLoader
    {
        private string _porductsLinq;
        private IWebDriver _driver;
        public IEnumerable<Product> Products { get; private set; }

        public ProductLoader(string porductsLinq, IWebDriver driver)
        {
            _porductsLinq = porductsLinq ?? throw new ArgumentNullException(nameof(porductsLinq));
            _driver = driver;
        }


        private void LoadLinq()
        {
            //var lastImg = By.ClassName("productaddtocart__submit");
           // WaitHelpers.WaitUntilElementClickable(_driver, lastImg, 40);
           _driver.Url = _porductsLinq;
            var find = By.XPath(@".//div[@class='listingcollapsed__activenumbercontainer']");

            WaitHelpers.WaitUntilElementClickable(_driver, find, 40);


        }
        private IEnumerable<string> LoadProducts()
        {
            var productLinqs = _driver.FindElements(By.XPath(@".//div[@class='listingcollapsed__activenumbercontainer']/a")).Select(a => a.GetAttribute("href"));
            return productLinqs;

        }
        public IEnumerable<Product> LoadProductsFromLinq()
        {
            LoadLinq();
            var productLinqs = LoadProducts().ToList();
            var resultProductSet = new List<Product>();
            
                foreach (var productLinq in productLinqs)
                {
                    var product = new ProductCreator(productLinq, _driver).CreateProductOrNull();
                    if (product == null)
                    {
                        continue;
                    }
                    resultProductSet.Add(product);

                    var productSaver = new PicSaver(_driver, new List<Product>() { product });
                    productSaver.SavePics();


                }
                Products = resultProductSet;

                return resultProductSet;
            

        }
    }
}
