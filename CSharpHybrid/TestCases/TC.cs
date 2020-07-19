using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using RAFT.Base;
using RAFT.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFT.TestCases
{   
    [TestFixture]
    public class TC:ReportsGenerationClass
    {
        public IWebDriver driver;        
        [Test,Category("SmokeTest")]
        public void ReportResetText()
        {
            this.driver = ReportsGenerationClass.WebDriver;
            LoginPage login = new LoginPage(this.driver);
            login.Navigatetourl(ConfigurationManager.AppSettings["URL"]);
            //string text = login.getResetTimer();            
        }

        [Test, Category("SmokeTest")]
        public void Test2()
        {
            this.driver = ReportsGenerationClass.WebDriver;
            LoginPage login = new LoginPage(this.driver);
            login.Navigatetourl("https://demo.totaralearning.com/");
            Assert.IsTrue(false);
        }

        [Test, Category("TestData")]
        public void ReadExcelData()
        {
            this.driver = ReportsGenerationClass.WebDriver;
            string xlFilePath = "D:\\SCB\\Automation\\Flyers_Soft\\RAFT\\RAFT\\TestData\\testdata.xlsx";
            ExcelAPI exl = new ExcelAPI(xlFilePath);
            string cellValue1 = exl.GetCellData("Sheet1",1 ,1);
            string cellValue2 = exl.GetCellData("Sheet1",2 ,1);
            Console.WriteLine("Cell Value using Column Number:" +cellValue1 + " & " + cellValue2 );
            if(exl.SetCellData("Sheet1", 3, 1,"setData"))
            {
                Console.WriteLine("DataWritten");
            }
        }


    }
}
