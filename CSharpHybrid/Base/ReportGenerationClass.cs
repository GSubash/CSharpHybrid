using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;

namespace RAFT.Base
{
    [SetUpFixture]
    public abstract class ReportsGenerationClass : CommonBase
    {
        protected ExtentReports _extent;
        protected ExtentTest _test;
        public static IWebDriver _driver;
        [OneTimeSetUp]
        protected void Setup()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");            
            var Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            var reportPath = projectPath + "Reports\\ExtentReport"+Timestamp+".html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            _extent = new ExtentReports();            
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Host Name", "LocalHost");
            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("UserName", "TestUser");
        }
        [OneTimeTearDown]
        protected void TearDown()
        {
            _extent.Flush();
        }
        [SetUp]
        public void startWebDriver()
        {
            CommonBase.startBrowser();            
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? ""
: string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    String fileName = "Screenshot_" +time.ToString("h_mm_ss") + ".png";
                    String screenShotPath = Capture(_driver, fileName);
                    _test.Log(Status.Fail, "Fail");
                    _test.Log(Status.Fail, "Snapshot below: " +_test.AddScreenCaptureFromPath("Screenshots\\" + screenShotPath));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            _test.Log(logstatus, "Test ended with " +logstatus + stacktrace);
            _extent.Flush();
            _driver.Quit();
        }
        public IWebDriver GetDriver()
        {
            return _driver;
        }

        public static IWebDriver WebDriver
        {
            get { return CommonBase.WebDriver; }
        }
        public static string Capture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" +screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return reportPath;
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
