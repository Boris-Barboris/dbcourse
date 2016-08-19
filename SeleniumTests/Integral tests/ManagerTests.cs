using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Linq;
using System.Linq.Expressions;

namespace SeleniumTests
{
	[TestClass]
	public partial class ManagerTests
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
            driver.Manage().Window.Maximize();
		}

		[TestCleanup]
		public void CloseDriver()
		{
			driver.Close();
		}

        void login_manager()
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
        }

        [TestMethod]
        public void FinantialReportTestMethod()
        {
            login_manager();

            IWebElement finreport_btn = driver.FindElement(By.LinkText("Финансовый отчёт"));
            finreport_btn.Click();
            Thread.Sleep(1000);

            // Проверяем, попали ли мы на страницу финансового отчёта
            Assert.IsTrue(driver.Url.Contains(@"BusinessManager/FinReport.aspx"));
            IWebElement report_table = driver.FindElement(By.Id("contentPlaceholder_reportGridView"));
        }

        [TestMethod]
        public void TestAccountCreationMethod()
        {
            login_manager();

            IWebElement accounts_btn = driver.FindElement(By.LinkText("Аккаунты"));
            accounts_btn.Click();
            Thread.Sleep(1000);

            Assert.IsTrue(driver.Url.Contains(@"NetworkAdministrator/ManageUsers.aspx"));
            IWebElement username_edit = driver.FindElement(
                By.Id(@"contentPlaceholder_newUserForm_ctl04_ctl00___Username_TextBox1"));
            IWebElement password_edit = driver.FindElement(
                By.Id(@"contentPlaceholder_newUserForm_ctl04_ctl01___Password_TextBox1"));
            SelectElement role_edit = new SelectElement(driver.FindElement(
                By.Id(@"contentPlaceholder_newUserForm_ctl04_ctl02___Role_DropDownList1")));
            IWebElement email_edit = driver.FindElement(
                By.Id(@"contentPlaceholder_newUserForm_ctl04_ctl04___EMail_TextBox1"));
            IWebElement create_btn = driver.FindElement(
                By.Name(@"ctl00$contentPlaceholder$newUserForm$ctl02"));

            username_edit.SendKeys("testusername");
            password_edit.SendKeys("testpassword");
            role_edit.SelectByText("Client");
            email_edit.SendKeys("testemail");
            create_btn.Click();

            // test wether user was created
            Thread.Sleep(2000);
            using (WebPortal.DataBase.SkySharkDbContainer db = new WebPortal.DataBase.SkySharkDbContainer())
            {
                var user = db.UserSet.Find("testusername");
                Assert.IsNotNull(user);
                Assert.AreEqual(user.Password, "testpassword");
                Assert.AreEqual(user.Role, WebPortal.DataBase.UserRole.Client);
                Assert.AreEqual(user.EMail, "testemail");
                db.UserSet.Remove(user);
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void PassangerPageAccess()
        {
            login_manager();

            IWebElement accounts_btn = driver.FindElement(By.LinkText("Пассажиры"));
            accounts_btn.Click();
            Thread.Sleep(1000);
            Assert.IsTrue(driver.Url.Contains(@"BusinessManager/PassengerList.aspx"));
        }

        [TestMethod]
        public void FlightCreationTest()
        {
            login_manager();

            IWebElement accounts_btn = driver.FindElement(By.LinkText("Новый рейс"));
            accounts_btn.Click();
            Thread.Sleep(1000);
            Assert.IsTrue(driver.Url.Contains(@"BusinessManager/NewFlight.aspx"));

            // Entering new flight data
            IWebElement flignt_no = driver.FindElement(
                By.Id(@"contentPlaceholder_newFlightForm_ctl04_ctl00___FlightNo_TextBox1"));
            IWebElement flight_origin = driver.FindElement(
                By.Id(@"contentPlaceholder_newFlightForm_ctl04_ctl01___Origin_TextBox1"));
            IWebElement flight_destination = driver.FindElement(
                By.Id(@"contentPlaceholder_newFlightForm_ctl04_ctl02___Destination_TextBox1"));
            IWebElement flight_departure = driver.FindElement(
                By.Id(@"contentPlaceholder_newFlightForm_ctl04_ctl03___DepTime_TextBox1"));
            IWebElement flight_arrival = driver.FindElement(
                By.Id(@"contentPlaceholder_newFlightForm_ctl04_ctl04___ArrTime_TextBox1"));
            IWebElement plane_type = driver.FindElement(
                By.Id(@"contentPlaceholder_newFlightForm_ctl04_ctl05___AircraftType_TextBox1"));
            IWebElement add_btn = driver.FindElement(
                By.Name(@"ctl00$contentPlaceholder$newFlightForm$ctl02"));

            flignt_no.SendKeys("testflight01");
            flight_origin.SendKeys("testOrigin");
            flight_destination.SendKeys("testDestination");
            DateTime dep_time = DateTime.Now + new TimeSpan(1, 0, 0, 0);
            dep_time -= new TimeSpan(0, dep_time.Hour, dep_time.Minute, dep_time.Second, dep_time.Millisecond);
            DateTime arr_time = dep_time + new TimeSpan(1, 0, 0, 0);
            flight_departure.SendKeys(dep_time.ToString(@"dd\/MM\/yyyy"));
            flight_arrival.SendKeys(arr_time.ToString(@"dd\/MM\/yyyy"));
            plane_type.SendKeys("testPlaneType");
            add_btn.Click();

            // test wether flight was created
            Thread.Sleep(2000);
            using (WebPortal.DataBase.SkySharkDbContainer db = new WebPortal.DataBase.SkySharkDbContainer())
            {
                var flight = db.FlightDetailsSet.Find("testflight01");
                Assert.IsNotNull(flight);
                Assert.AreEqual(flight.Origin, "testOrigin");
                Assert.AreEqual(flight.Destination, "testDestination");
                Assert.AreEqual(flight.DepTime.ToString(), dep_time.ToString());
                Assert.AreEqual(flight.ArrTime.ToString(), arr_time.ToString());
                Assert.AreEqual(flight.AircraftType, "testPlaneType");
                db.FlightDetailsSet.Remove(flight);
                db.SaveChanges();
            }
        }
	}
}
