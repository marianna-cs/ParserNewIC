using OpenQA.Selenium;
using ParserNewIc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserNewIC
{
    class ProductCreator
    {
        private string _productLinq;
        private IWebDriver _driver;

        public ProductCreator(string productLinq, IWebDriver driver)
        {
            _productLinq = productLinq ?? throw new ArgumentNullException(nameof(productLinq));
            _productLinq = _productLinq.Trim().Replace("%20", "").Replace(" ", "");
            _driver = driver;
        }

        private void LoadProductPage()
        {

            _driver.Url = _productLinq;
            WaitHelpers.WaitUntilElementClickable(_driver, By.ClassName(@"productinfo"), 40);
        }
        private string LoadBrand()
        {
            try
            {
                string brand = _driver.FindElement(By.XPath(@".//img[@class='productinfo__manufacturerimg']")).GetAttribute("title");

                return brand;
            }
            catch
            {
                string brand = _driver.FindElement(By.XPath(@".//div[@class='productinfo__manufacturer ']")).Text;

                return brand;
            }
        }
        private string LoadProductIndex()
        {
            var index = _driver.FindElement(By.XPath(@".//div[@class='productinfo__sku']/div")).Text;

            return index.Replace("/", "").Replace("*", "");
        }
        private IEnumerable<string> LoadProductImages()
        {
           // WaitHelpers.WaitUntilElementClickable(_driver, By.XPath(@".//div[@class='productinfo__name']"), 40);
            _driver.FindElement(By.XPath(@".//div[@class='productcarousel__mainitem slick-slide slick-current slick-active']")).Click();

            var pic = By.ClassName("productgallery__header");

            WaitHelpers.WaitUntilElementClickable(_driver, pic, 40);

            var result = new List<string>();

            try
            {
                var imgLinq = _driver.FindElement(By.XPath(@".//img[@class='productgallery__singleimg']")).GetAttribute("src");
                result.Add(imgLinq);
            }
            catch
            {
                var imgLinq = _driver.FindElement(By.XPath(@".//img[@class='productgallery__mainimg']")).GetAttribute("src");
                result.Add(imgLinq);
            }
            
            return result;
        }

        public Product CreateProductOrNull()
        {
            LoadProductPage();
            try
            {
                var photoAvilability = _driver.FindElement(By.XPath(@".//img[@class='productcarousel__noimage']")).GetAttribute("src");
                return null;
            }
            catch 
            {
                var product = new Product(LoadProductImages(), LoadProductIndex(), LoadBrand());

                return product;
            }

        }
    }
}
