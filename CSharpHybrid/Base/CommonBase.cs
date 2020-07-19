using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFT.Base
{
    public class CommonBase
    {
        private static IWebDriver _webDriver;        

        public static IWebDriver WebDriver
        {
            get { return _webDriver ; }
        }
       
        public static void startBrowser()
        {

            String browserName = "chrome";
            switch (browserName.ToLower())
            {

                case "internetexplorer":
                case "ie":
                    _webDriver = StartInternetExplorer();
                    //VNTestBaseExt.UnitTestlogger.AppendLogMessage("Internet Explorer Browser Started");
                    _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                    break;

                case "firefox":
                case "ff":
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("\\packages\\Selenium.WebDriver.GeckoDriver.0.23.0\\driver\\win32", "geckodriver.exe");

                    service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                    _webDriver = new FirefoxDriver(service);
                    //VNTestBaseExt.UnitTestlogger.AppendLogMessage("FireFox Browser Started");
                    _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

                    break;

                case "chrome":                
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("start-maximized");
                    options.AddArgument("ignore-certificate-errors");
                    //options.AddArgument("headless");
                    options.BinaryLocation = @"C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
                    string path1 = @"D:\SCB\Automation\Flyers_Soft\RAFT\packages\Selenium.WebDriver.ChromeDriver.83.0.4103.3910\driver\win32";
                    _webDriver = new ChromeDriver(path1, options);
                    //_webDriver.Navigate().GoToUrl("https://www.google.co.in");
                    break;

                /*case "safari":
                    _webDriver = new SafariDriver();
                    _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                    VNTestBaseExt.UnitTestlogger.AppendLogMessage("Safari Browser Started");
                    break;*/
            }
        }

        private static InternetExplorerDriver StartInternetExplorer()
        {
            var internetExplorerOptions = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                InitialBrowserUrl = "about:blank",
                EnableNativeEvents = false,
                EnablePersistentHover = false,
                EnsureCleanSession = true,
                IgnoreZoomLevel = true,
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                ElementScrollBehavior = InternetExplorerElementScrollBehavior.Bottom,
                RequireWindowFocus = true,
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            string AppPath1 = Environment.CurrentDirectory;
            //string path1 = Path.GetFullPath(Path.Combine(AppPath1, @ReadAppProperties.BrowserDirectory())); ///IEDriverServer.exe
            string path1 = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe";
            return new InternetExplorerDriver(path1, internetExplorerOptions); //Directory.GetCurrentDirectory()
        }

    }
}
