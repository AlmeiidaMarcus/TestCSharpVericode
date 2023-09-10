using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SpecFlowProject1.Support;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class ConsultaCorreiosStepsDefinitions
    {
        private IWebDriver driver;
        private ScreenshotHelper _screenshotHelper;
        IWebDriver edgeDriver = WebDriverFactory.CreateWebDriver(BrowserType.Edge);

        [Given(@"Eu acesso o site dos correios")]
        public void GivenEuAcessoOSiteDosCorreios()
        {
            // Verifica navegador
            driver = WebDriverFactory.CreateWebDriver(BrowserType.Edge);

            // Captura evid�ncia do teste
            _screenshotHelper = new ScreenshotHelper(driver);

            // Configura o driver do navegador
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();

            // Acessando a p�gina dos Correios
            driver.Navigate().GoToUrl("https://www.correios.com.br/");
        }

        [When(@"Eu procuro pelo CEP ""(.*)""")]
        public void WhenEuProcuroPeloCEP(string cep)
        {
            // Busca o campo CEP
            IWebElement campoBuscaCEP = driver.FindElement(By.Id("id=\"relaxation\""));

            // Limpa o campo de busca, se necess�rio
            campoBuscaCEP.Clear();

            // Preenche o campo CEP com o valor do par�metro
            campoBuscaCEP.SendKeys(cep);

            // Printscreen
            _screenshotHelper.TakeScreenshot("WhenEuProcuroPeloCEP");

            // Localiza o bot�o de pesquisa
            IWebElement botaoPesquisar = driver.FindElement(By.CssSelector("div.card-mais-acessados:nth-child(4) > form:nth-child(1) > div:nth-child(3) > button:nth-child(2) > i:nth-child(1)"));

            // Clica no bot�o para iniciar a busca
            botaoPesquisar.Click();
        }

        [Then(@"Confirmo que o CEP n�o existe")]
        public void ThenConfirmoQueOCEPNaoExiste()
        {
            // Verifica se a mensagem de erro est� vis�vel
            IWebElement mensagemErro = driver.FindElement(By.CssSelector("#mensagem-resultado"));

            // Printscreen
            _screenshotHelper.TakeScreenshot("ThenConfirmoQueOCEPNaoExiste");

            // Assert da mensagem de erro
            Assert.IsTrue(mensagemErro.Displayed);
        }

        [Then(@"Eu volto para a tela inicial")]
        public void ThenEuVoltoParaATelaInicial()
        {
            // AND Retorno para a tela inicial
            driver.Navigate().GoToUrl("https://www.correios.com.br/");

            // Printscreen
            _screenshotHelper.TakeScreenshot("ThenEuVoltoParaATelaInicial");
        }


        [Given(@"Eu procuro pelo CEP Correto")]
        public void WhenEuProcuroPeloCEP(decimal p0)
        {
            // Busca o campo de busca por CEP
            IWebElement campoBuscaCEP = driver.FindElement(By.Id("id=\"relaxation\""));

            // Preencha o campo com o CEP 01013-001
            campoBuscaCEP.SendKeys("01013-001");

            // Printscreen
            _screenshotHelper.TakeScreenshot("WhenEuProcuroPeloCEP");

            // Localiza o bot�o de pesquisa
            IWebElement botaoPesquisar = driver.FindElement(By.CssSelector("div.card-mais-acessados:nth-child(4) > form:nth-child(1) > div:nth-child(3) > button:nth-child(2) > i:nth-child(1)"));

            // Clique no bot�o para iniciar a busca
            botaoPesquisar.Click();
        }

        [Then(@"Confirmo que o resultado � ""([^""]*)""")]
        public void ThenConfirmoQueOResultadoE(string p0)
        {
            // Verifica o nome da rua
            IWebElement resultadoCEP = driver.FindElement(By.CssSelector("#resultado-DNEC > tbody:nth-child(3) > tr:nth-child(1) > td:nth-child(1)"));

            // Verifica se o texto est� correto
            Assert.AreEqual("Rua Quinze de Novembro, S�o Paulo/SP", resultadoCEP.Text);

            // Printscreen
            _screenshotHelper.TakeScreenshot("ThenConfirmoQueOResultadoE");

            // AND Retorno para a tela inicial
            driver.Navigate().GoToUrl("https://www.correios.com.br/");
        }

        [When(@"Eu procuro pelo c�digo de rastreamento incorreto")]
        public void WhenEuProcuroPeloCodigoDeRastreamentoIncorreto()
        {
            // Localiza o campo de busca por c�digo de rastreamento
            IWebElement campoBuscaRastreamento = driver.FindElement(By.Id("id=\"objetos\""));

            // Preenche o campo
            campoBuscaRastreamento.SendKeys("SS987654321BR");

            // Printscreen
            _screenshotHelper.TakeScreenshot("WhenEuProcuroPeloCodigoDeRastreamentoIncorreto");

            // Localiza o bot�o de pesquisa
            IWebElement botaoPesquisar = driver.FindElement(By.XPath("/html/body/div/div/div/div[3]/div/div/article/div[3]/div/div[2]/div[1]/form/div[2]/button/i"));

            // Clique no bot�o
            botaoPesquisar.Click();
        }

        [Then(@"Confirmo que o c�digo n�o est� correto")]
        public void ThenConfirmoQueOCodigoNaoEstaCorreto()
        {
            // Verifica a mensagem de erro
            IWebElement mensagemErro = driver.FindElement(By.XPath("/html/body/main/div[1]/form/div[2]/div[2]/div[2]/div[1]/label"));

            // Printscreen
            _screenshotHelper.TakeScreenshot("ThenConfirmoQueOCodigoNaoEstaCorreto");

            // Assert da mensagem
            Assert.IsTrue(mensagemErro.Displayed);

            // AND Fecho o navegador
            driver.Quit();

            // Fecha mesmo se o teste falhar
            driver?.Quit();
        }
    }
}
