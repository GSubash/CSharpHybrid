using OpenQA.Selenium;
using RAFT.Base;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFT.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.XPath, Using = "//*[@id='page-site-index']/div[3]/h4/strong")]
        public IWebElement resetText { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='inst675']/div/div/p/img")]
        public IWebElement welcomeImage { get; set; }

        #region constructor
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        #endregion

        #region methods

        public void Navigatetourl(String url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public string getResetTimer()
        {
            return resetText.Text;
        }
        #endregion



    }
}
