using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
	[TestFixture]
	public class InteractionsDemo : FirefoxInitialise
	{
		private IWebDriver _driver;
		private Actions _actions;
		private WebDriverWait _wait;

		[SetUp]
		public void Setup()
		{
			_driver = FirefoxInitialise.Init();
			_actions = new Actions(_driver);
			_wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		}

		[TearDown]
		public void Teardown()
		{
			_driver.Close();
			_driver.Quit();
		}

		[Test]
		public void DragDropExample1()
		{
			_driver.Navigate().GoToUrl("https://jqueryui.com/droppable/");
			_wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.ClassName("demo-frame")));

			IWebElement targetElement = _driver.FindElement(By.Id("droppable")); 
			IWebElement sourceElement = _driver.FindElement(By.Id("draggable"));
			_actions.DragAndDrop(sourceElement, targetElement).Perform();

			Assert.AreEqual("Dropped!", targetElement.Text);

		}

		[Test]
		public void DragDropExample2()
		{
			_driver.Navigate().GoToUrl("https://jqueryui.com/droppable/");
			_wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.ClassName("demo-frame")));

			IWebElement targetElement = _driver.FindElement(By.Id("droppable"));
			IWebElement sourceElement = _driver.FindElement(By.Id("draggable"));

			var drag = _actions
				.ClickAndHold(sourceElement)
				.MoveToElement(targetElement)
				.Release(targetElement)
				.Build();

			drag.Perform();

			Assert.AreEqual("Dropped!", targetElement.Text);
		}

		//Not supported by Selenium Webdriver - http://stackoverflow.com/questions/29381233/how-to-simulate-html5-drag-and-drop-in-selenium-webdriver
		//https://github.com/seleniumhq/selenium-google-code-issue-archive/issues/6315
		[Test]
		public void DragDropHtml5_Quiz()
		{
			_driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/drag_and_drop");
			var source = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("column-a")));

			var jsFile = File.ReadAllText(@"c:\GIT_Solutions_RK\Training\Selenium\UdemySeleniumC\UserInteractionsTutorial-master\drag_and_drop_helper.js");
			IJavaScriptExecutor js = _driver as IJavaScriptExecutor;
			//Execute java script - #{{id value}}
			js.ExecuteScript(jsFile + "$('#column-a').simulateDragDrop({ dropTarget: '#column-b'});");

			Assert.AreEqual("A", _driver.FindElement(By.XPath("//*[@id='column-b']/header")).Text);
		}

		[Test]
		public void jQueryDragDropQuiz()
		{
			_driver.Navigate().GoToUrl("http://www.pureexample.com/jquery-ui/basic-droppable.html");

			_wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("ExampleFrame-94")));

			IWebElement sourceElement = _driver.FindElement(By.XPath("//*[@class='square ui-draggable']"));
			IWebElement targetElement = _driver.FindElement(By.XPath("//*[@class='squaredotted ui-droppable']"));
			_actions.DragAndDrop(sourceElement, targetElement).Perform();

			var droppedText = _driver.FindElement(By.Id("info")).Text;

			Assert.AreEqual("dropped!", droppedText);
		}

		[Test]

		public void ResizingExample()
		{
			_driver.Navigate().GoToUrl("http://jqueryui.com/resizable/");
			_wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.ClassName("demo-frame")));

			IWebElement resizeHandle = _driver.FindElement(By.XPath("//*[@class='ui-resizable-handle ui-resizable-se ui-icon ui-icon-gripsmall-diagonal-se']"));

			_actions.ClickAndHold(resizeHandle).MoveByOffset(100, 100).Perform();

			Assert.IsTrue(_driver.FindElement(By.XPath("//*[@id='resizable' and @style]")).Displayed);
		}

		[Test]
		public void OpenNetworkTabUsingFirefox()
		{
			_driver.Navigate().GoToUrl("http://www.google.com");
			_actions.KeyDown(Keys.Control).KeyDown(Keys.Shift).SendKeys("e").Perform();

			_actions.KeyUp(Keys.Control).KeyUp(Keys.Shift).Perform();
			_driver.Navigate().GoToUrl("http://www.pureexample.com/jquery-ui/basic-droppable.html");
		}

		//Not supported by Selenium Webdriver - http://stackoverflow.com/questions/29381233/how-to-simulate-html5-drag-and-drop-in-selenium-webdriver
		//https://github.com/seleniumhq/selenium-google-code-issue-archive/issues/6315

	}
}
