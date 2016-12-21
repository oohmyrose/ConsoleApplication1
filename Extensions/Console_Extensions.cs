using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium;
using System.IO;


namespace ConsoleApplication1.Extensions
{
    class WriteConsole
    {
        public static void Green(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Red(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void Yellow(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DarkCyan(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void DarkMagenta(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Cyan(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DarkRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static class Report
    {
        public static string automationPath = "C:\\Automation";
        public static string reportPath     = automationPath + String.Format("\\Report_{0}.html", DateTime.Now.ToString("yyyyMMdd-HHmmss"));
        public static string imgPath        = automationPath + "\\screenshot";
       
        public static ExtentReports extent = new ExtentReports(reportPath, true);
        public static ExtentTest test;

        public static void Case(string caseName, string caseDescription)
        {
            System.IO.Directory.CreateDirectory(automationPath);
            System.IO.Directory.CreateDirectory(imgPath);

            test = extent.StartTest(caseName, caseDescription);
        }
        public static void Log(LogStatus status, string step ,string detail)
        {
            test.Log(status, step, detail);
        }
        public static void PrintScreen(this IWebDriver driver)
        {
            string imgName = String.Format("{0}\\{1}.png", imgPath, DateTime.Now.ToString("yyyyMMdd-HHmmss") );
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(imgName, System.Drawing.Imaging.ImageFormat.Png);

            test.Log(LogStatus.Info, "Please see screenshot below: " + test.AddScreenCapture(imgName));
        }
        public static void EndAutomation()
        {
            extent.EndTest(test);
            extent.Flush();
            System.Diagnostics.Process.Start(Report.reportPath);
            WriteConsole.Green("Automation has succeeded");
        }
       
    }
}
