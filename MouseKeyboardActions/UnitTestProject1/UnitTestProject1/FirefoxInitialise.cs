using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
	public class FirefoxInitialise
	{
		private static IWebDriver Driver { get; set; }

		public static IWebDriver Init()
		{
			// Geckodriver as a proxy service to use FireFox browser because FF is not supported by selenium more than 3.X.X
			// More details abou geckodriver here: https://github.com/mozilla/geckodriver

			FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"c:\Selenium\geckodriver-v0.23.0-win64\");
			service.FirefoxBinaryPath = @"c:\Program Files (x86)\Mozilla Firefox\firefox.exe";
			FirefoxOptions options = new FirefoxOptions();
			TimeSpan time = TimeSpan.FromSeconds(10);
			Driver = new FirefoxDriver(service, options, time);
			return Driver;
		}
	}
}
