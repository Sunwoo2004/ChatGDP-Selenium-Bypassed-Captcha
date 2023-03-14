using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ChatGPT
{
    internal class Program
    {
        static ChromeDriverService _driverService = null;
        static ChromeOptions _options = null;
        static ChromeDriver _driver = null;
        
        static void Initialize()
        {
            _driverService = ChromeDriverService.CreateDefaultService();
            _driverService.HideCommandPromptWindow = true;
            _options = new ChromeOptions();
            _options.AddArgument("disable-gpu");
            
            

            //_options.AddArguments("--incognito");
        }
        static void Main(string[] args)
        {
            Initialize();


            //Start
            _driver = new ChromeDriver(_driverService, _options);
            _driver.Navigate().GoToUrl("https://chat.openai.com/chat");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //쿠키
            Cookie cookie2 = new Cookie("__cf_bm", "");
            Cookie cookie3 = new Cookie("__Host-next-auth.csrf-token", "");
            Cookie cookie4 = new Cookie("__Secure-next-auth.callback-url", "https%3A%2F%2Fchat.openai.com%2Fchat");
            Cookie cookie5 = new Cookie("__Secure-next-auth.session-token", "");
            Cookie cookie6 = new Cookie("_cfuvid", "");
            Cookie cookie7 = new Cookie("_ga", "");
            Cookie cookie8 = new Cookie("_ga_9YTZJE58M9", "");
            Cookie cookie9 = new Cookie("_gid", "");
            Cookie cookie10 = new Cookie("cf_clearance", "");
            Cookie cookie11 = new Cookie("cf_clearance", ""); //이부분이 캡챠
            Cookie cookie12 = new Cookie("intercom-device-id-dgkjq2bp", "");
            Cookie cookie13 = new Cookie("intercom-session-dgkjq2bp", "");

            _driver.Manage().Cookies.AddCookie(cookie2);
            _driver.Manage().Cookies.AddCookie(cookie3);
            _driver.Manage().Cookies.AddCookie(cookie4);
            _driver.Manage().Cookies.AddCookie(cookie5);
            _driver.Manage().Cookies.AddCookie(cookie6);
            _driver.Manage().Cookies.AddCookie(cookie7);
            _driver.Manage().Cookies.AddCookie(cookie8);
            _driver.Manage().Cookies.AddCookie(cookie9);
            _driver.Manage().Cookies.AddCookie(cookie10);
            _driver.Manage().Cookies.AddCookie(cookie11);
            _driver.Manage().Cookies.AddCookie(cookie12);
            _driver.Manage().Cookies.AddCookie(cookie13);
            //

            Console.WriteLine("쿠키 설정 완료.");

            Console.Write("초기 로그인입니다. 진행하시겠습니까? (Y/N) : ");
            string szResult = Console.ReadLine();

            if (szResult != "Y" && szResult != "y")
            {
                Console.WriteLine("프로그램을 종료합니다.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                _driver.Navigate().GoToUrl("https://chat.openai.com/chat");
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Console.Write("검색할 문자를 입력 해 주세요 : ");
                string szCommand = Console.ReadLine();

                if (szCommand == "!exit")
                {
                    _driver.Quit();
                    Environment.Exit(0);
                }

                //var HumanCheck = _driver.FindElement(By.TagName("textarea")); //_driver.FindElementByXPath("//*[@id='cf-stage']/div[6]/label/input");
                //Console.WriteLine(HumanCheck.TagName);
                //HumanCheck.Click();
                //사람체크는 손으로 해야함 처음만


                var szMessageText = _driver.FindElement(By.TagName("textarea")); 
                szMessageText.SendKeys(szCommand);
                szMessageText.SendKeys(Keys.Enter);
                Console.WriteLine("SendKey OK");

                while (true)
                {
                    Console.WriteLine("Checking...");
                    var szCheckText = _driver.FindElement(By.XPath("//*[@id='__next']/div[1]/div[1]/main/div[2]/form/div/div[1]/button"));
                    if (szCheckText.Text == "Regenerate response")
                    {
                        Console.WriteLine("Checked!");
                        break;
                    }
                    Thread.Sleep(1000);
                }

                var szTextGet = _driver.FindElement(By.XPath("//*[@id='__next']/div[1]/div/main/div[1]/div/div/div/div[2]/div"));
                string szText = szTextGet.Text;
                Console.WriteLine(szText);

                Console.ReadKey();

                Console.Clear();
            }
        }
    }
}
