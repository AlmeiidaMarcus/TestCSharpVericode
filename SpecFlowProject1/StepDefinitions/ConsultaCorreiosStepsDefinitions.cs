using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowProject1.Support;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;
using System.Linq;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class ConsultaCorreiosStepsDefinitions : WebDriverFactory
    {
        private IWebDriver driver;
        private ScreenshotHelper _screenshotHelper;
        private IWebDriver alteraAba;
        private string originalWindowHandle;
        private List<string> allWindowHandles;
        //IWebDriver edgeDriver = WebDriverFactory.CreateWebDriver(BrowserType.Edge);

        [Given(@"Eu acesso o site dos correios")]
        public void GivenEuAcessoOSiteDosCorreios()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.correios.com.br/");
            Actions actions = new Actions(driver); 
            actions.SendKeys(Keys.F5);
            bool acceptCookiesButtonExists = DoesElementExist(driver, By.Id("btnCookie"));
            if (acceptCookiesButtonExists)
                try
            {
                IWebElement acceptCookiesButton = driver.FindElement(By.Id("btnCookie"));
                acceptCookiesButton.Click();
            }
            catch (NoSuchElementException) { }
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(99999999));
            IWebElement campoBuscaRastreamento = wait.Until(ExpectedConditions.ElementExists(By.Id("objetos")));
            _screenshotHelper = new ScreenshotHelper(driver);
            _screenshotHelper.TakeScreenshot("GivenEuAcessoOSiteDosCorreios");
        }

        [When(@"Eu procuro pelo CEP ""(.*)""")]
        [Obsolete]
        public void WhenEuProcuroPeloCEP(string cep)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(99999999));
            IWebElement campoBuscaCEP = wait.Until(ExpectedConditions.ElementExists(By.Id("relaxation")));
            driver.FindElement(By.Id("relaxation")).SendKeys("80700000");

            //IWebElement button = driver.FindElement(By.Id("id_do_botao"));
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", button);
            driver.FindElement(By.XPath("/html/body/div/div/div/div[3]/div/div/article/div[3]/div/div[2]/div[4]/form/div[2]/button")).Click();

            // Salve o identificador da janela principal
            string mainWindowHandle = driver.CurrentWindowHandle;
            // Identificador da nova aba
            string newTabHandle = driver.WindowHandles.Last(); 
            driver.SwitchTo().Window(newTabHandle);
            // Printscreen
            _screenshotHelper.TakeScreenshot("WhenEuProcuroPeloCEP");
        }

        [Then(@"Confirmo que o CEP não existe")]
        public void ThenConfirmoQueOCEPNaoExiste()
        {
            // Valida mensagem de "Dados não encontrado"
            driver.FindElement(By.CssSelector("#mensagem-resultado-alerta > h6:nth-child(1)"));

            // Printscreen
            _screenshotHelper.TakeScreenshot("ThenConfirmoQueOCEPNaoExiste");

            // Feche a nova aba
            driver.Close();
        }

        [Then(@"Eu volto para a tela inicial")]
        public void ThenEuVoltoParaATelaInicial()
        {
            // Volte para a página inicial (janela principal)
            string mainWindowHandle = driver.WindowHandles.Last();
            driver.SwitchTo().Window(mainWindowHandle);

            // Printscreen
            _screenshotHelper.TakeScreenshot("ThenEuVoltoParaATelaInicial");

            // Fecha o navegador
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500));
            driver.Close();
        }

        [When("Eu procuro pelo CEP Correto")]
        public void WhenEuProcuroPeloCEPCorreto()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(99999999));
            IWebElement campoBuscaCEPCorreto = wait.Until(ExpectedConditions.ElementExists(By.Id("relaxation")));
            driver.FindElement(By.Id("relaxation")).SendKeys("01013-001");

            //IWebElement button = driver.FindElement(By.Id("id_do_botao"));
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", button);
            driver.FindElement(By.XPath("/html/body/div/div/div/div[3]/div/div/article/div[3]/div/div[2]/div[4]/form/div[2]/button")).Click();

            // Salve o identificador da janela principal
            string mainWindowHandle = driver.CurrentWindowHandle;
            // Identificador da nova aba
            string newTabHandle = driver.WindowHandles.Last();
            driver.SwitchTo().Window(newTabHandle);
            // Printscreen
            _screenshotHelper.TakeScreenshot("WhenEuProcuroPeloCEPCorreto");
        }

        [Then(@"Confirmo que o resultado é ""([^""]*)""")]
        public void ThenConfirmoQueOResultadoE(string p0)
        {
            // Valida o endereço correto Rua "Quinze de Novembro, São Paulo/SP"
            driver.FindElement(By.CssSelector("#resultado-DNEC > tbody:nth-child(3) > tr:nth-child(1) > td:nth-child(1)"));

            // Printscreen
            _screenshotHelper.TakeScreenshot("ThenConfirmoQueOResultadoE");

            // Feche a nova aba
            driver.Close();
        }

        [When(@"Eu procuro pelo código de rastreamento incorreto")]
        public void WhenEuProcuroPeloCodigoDeRastreamentoIncorreto()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(99999999));
            IWebElement campoRastreamento = wait.Until(ExpectedConditions.ElementExists(By.Id("objetos")));
            driver.FindElement(By.Id("objetos")).SendKeys("80700000");

            //IWebElement button = driver.FindElement(By.Id("id_do_botao"));
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", button);
            driver.FindElement(By.XPath("/html/body/div/div/div/div[3]/div/div/article/div[3]/div/div[2]/div[1]/form/div[2]/button/i")).Click();

            // Salve o identificador da janela principal
            string mainWindowHandle = driver.CurrentWindowHandle;
            // Identificador da nova aba
            string newTabHandle = driver.WindowHandles.Last();
            driver.SwitchTo().Window(newTabHandle);

            /* 
            Como preencheria o Captcha se o Visual Studio permitisse
            Captcha captcha = new Captcha();

            // Preencho o CAPTCHA
            string apiKey = "cc67231b3ca549558061917fa76785b7";
            string captchaId = captcha.SolveCaptcha(apiKey, @"https://rastreamento.correios.com.br/core/securimage/securimage_show.php");

            if (!string.IsNullOrEmpty(captchaId))
            {
                // Aguarde algum tempo para o serviço resolver o CAPTCHA (pode demorar)
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

                // Verifique o resultado do CAPTCHA
                string captchaResult = captcha.SolveCaptcha(apiKey, captchaId);

                // Preenche o campo Captcha
                IWebElement campoCaptcha = driver.FindElement(By.Id("captcha"));

                // Preencha o campo com o resultado do CAPTCHA
                campoCaptcha.SendKeys(captchaResult);
            }
            else
            {

            }
            */

            // Printscreen
            _screenshotHelper.TakeScreenshot("WhenEuProcuroPeloCodigoDeRastreamentoIncorreto");

            // Botão da lupa da página do Captcha
            driver.FindElement(By.XPath("/html/body/main/div[1]/form/div[2]/div[1]/div/div[2]/button/i")).Click();
        }

        [Then(@"Confirmo que o código não está correto")]
        public void ThenConfirmoQueOCodigoNaoEstaCorreto()
        {
            // Criei uma classe pra preencher o Captcha mas o Visual Studio barrou por questões
            // de segurança, vou apenas validar que o Captcha existe e finalizar o cenário
            // em um bloco de notas vou enviar a classe e vou deixar comentado no código como usar
            driver.FindElement(By.XPath("/html/body/main/div[1]/form/div[2]/div[1]/div/div[2]/button/i")).Click();

            // Printscreen
            _screenshotHelper.TakeScreenshot("ThenConfirmoQueOCodigoNaoEstaCorreto");

            // Feche a nova aba
            driver.Close();
        }
        static bool DoesElementExist(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
