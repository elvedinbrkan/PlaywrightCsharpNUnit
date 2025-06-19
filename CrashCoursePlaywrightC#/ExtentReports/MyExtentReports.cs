using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;


namespace CrashCoursePlaywrightC_.ExtentReportsDemo
{
    //Reporting Utility Class - initialize and manage the Extnat Report lifecycle
    public class MyExtentReports 
    {
        private static ExtentReports _extent;
        public static ExtentReports GetInstance() //will be executed before any test case at suite level
        {
            if (_extent == null)
            {
                DateTime currentTime = DateTime.Now;
                string fileName = "ExtentReport" + currentTime.ToString("yyy-MM-dd-HH-mm-ss") + ".html";

                var htmlReporter = new ExtentSparkReporter("C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Reports\\"+fileName);
                htmlReporter.Config.Theme = Theme.Dark;
                htmlReporter.Config.DocumentTitle = "Olx report";
                htmlReporter.Config.ReportName = "Automation test results";
                htmlReporter.Config.Encoding = "utf-8";

                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);

                _extent.AddSystemInfo("Automation task", "OlX");
                _extent.AddSystemInfo("Elvedin Brkan", "QA");
            }
                return _extent;  
        }
    }
}
