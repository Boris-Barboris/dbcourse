using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTests
{
	[TestClass]
	public partial class LoginTests
	{
		public static string BaseUrl = "localhost:1652/WebForms/";

		public static string driver_path = @"C:\Program Files (x86)\Reference Assemblies\Selenium\";

		// the max. time to wait for WebElement appearance before timing out.
		public const int TimeOut = 20;

		IWebDriver driver;

		[TestInitialize]
		public void InitializeDriver()
		{
			driver = new ChromeDriver(driver_path);
			driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(TimeOut));
		}

		[TestCleanup]
		public void CloseDriver()
		{
			driver.Close();
		}

		[TestMethod]
		public void AdminLoginClickTestMethod()		// Возможность зайти на страницу логина и стать админом
		{
			driver.Navigate().GoToUrl(BaseUrl + "Common/Default.aspx");
			Assert.AreEqual("SkyShark Airlines master page", driver.Title);
			IWebElement login_btn = driver.FindElement(By.LinkText("Войти"));
			login_btn.Click();
			IWebElement enter_label = driver.FindElement(By.Id("contentPlaceholder_loginPanel"));
			
			// Вводим логин
			IWebElement login_box = driver.FindElement(By.Id("contentPlaceholder_loginBox"));
			login_box.SendKeys("admin");

			// Вводим пароль
			IWebElement password_box = driver.FindElement(By.Id("contentPlaceholder_passwordBox"));
			password_box.SendKeys("1234");
			
			// Нажимаем кнопку войти
			IWebElement do_login_btn = driver.FindElement(By.Id("contentPlaceholder_loginButton"));
			do_login_btn.Click();
			Thread.Sleep(1000);

			// Проверяем, вошли ли мы
			Assert.IsTrue(driver.Url.Contains(@"Common/Default.aspx"));
			IWebElement admin_label = driver.FindElement(By.Id("loginLabel"));
			Assert.AreEqual("admin", admin_label.Text);

			// И можем ли выйти
			IWebElement logoff_btn = driver.FindElement(By.LinkText("Выйти"));
			logoff_btn.Click();
			Thread.Sleep(100);
			admin_label = driver.FindElement(By.Id("loginLabel"));
			Assert.AreEqual("вход не выполнен", admin_label.Text, true);
		}

        [TestMethod]
        public void ManagerLoginClickTestMethod()		// Возможность зайти на страницу логина и стать менеджером
        {
            driver.Navigate().GoToUrl(BaseUrl + "Common/Default.aspx");
            Assert.AreEqual("SkyShark Airlines master page", driver.Title);
            IWebElement login_btn = driver.FindElement(By.LinkText("Войти"));
            login_btn.Click();
            IWebElement enter_label = driver.FindElement(By.Id("contentPlaceholder_loginPanel"));

            // Вводим логин
            IWebElement login_box = driver.FindElement(By.Id("contentPlaceholder_loginBox"));
            login_box.SendKeys("manager");

            // Вводим пароль
            IWebElement password_box = driver.FindElement(By.Id("contentPlaceholder_passwordBox"));
            password_box.SendKeys("1234");

            // Нажимаем кнопку войти
            IWebElement do_login_btn = driver.FindElement(By.Id("contentPlaceholder_loginButton"));
            do_login_btn.Click();
            Thread.Sleep(1000);

            // Проверяем, вошли ли мы
            Assert.IsTrue(driver.Url.Contains(@"Common/Default.aspx"));
            IWebElement admin_label = driver.FindElement(By.Id("loginLabel"));
            Assert.AreEqual("manager", admin_label.Text);

            // И можем ли выйти
            IWebElement logoff_btn = driver.FindElement(By.LinkText("Выйти"));
            logoff_btn.Click();
            Thread.Sleep(100);
            admin_label = driver.FindElement(By.Id("loginLabel"));
            Assert.AreEqual("вход не выполнен", admin_label.Text, true);
        }

        [TestMethod]
        public void IncorrectLoginTestMethod()
        {
            driver.Navigate().GoToUrl(BaseUrl + "Common/Default.aspx");
            Assert.AreEqual("SkyShark Airlines master page", driver.Title);
            IWebElement login_btn = driver.FindElement(By.LinkText("Войти"));
            login_btn.Click();
            IWebElement enter_label = driver.FindElement(By.Id("contentPlaceholder_loginPanel"));

            // Вводим логин
            IWebElement login_box = driver.FindElement(By.Id("contentPlaceholder_loginBox"));
            login_box.SendKeys("manager");

            // Вводим пароль
            IWebElement password_box = driver.FindElement(By.Id("contentPlaceholder_passwordBox"));
            password_box.SendKeys("12345");

            // Нажимаем кнопку войти
            IWebElement do_login_btn = driver.FindElement(By.Id("contentPlaceholder_loginButton"));
            do_login_btn.Click();
            Thread.Sleep(1000);

            // Проверяем, вошли ли мы
            IWebElement admin_label = driver.FindElement(By.Id("contentPlaceholder_loginFailed"));
        }
	}
}
