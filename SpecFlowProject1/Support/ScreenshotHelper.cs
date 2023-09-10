using OpenQA.Selenium;
using System;
using System.IO;

namespace SpecFlowProject1.Support
{
    public class ScreenshotHelper
    {
        private readonly IWebDriver _driver;
        private readonly string _screenshotsDirectory;

        public ScreenshotHelper(IWebDriver driver)
        {
            _driver = driver;

            // Obtém o diretório do projeto atual.
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;

            // Define o diretório onde os screenshots serão salvos dentro do diretório do projeto.
            _screenshotsDirectory = Path.Combine(projectDirectory, "Screenshots");
        }

        public void TakeScreenshot(string scenarioName)
        {
            // Verifica se o diretório de screenshots existe, caso contrário, cria-o.
            if (!Directory.Exists(_screenshotsDirectory))
            {
                Directory.CreateDirectory(_screenshotsDirectory);
            }

            // Gera um nome de arquivo único com base no timestamp.
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string screenshotFileName = $"{scenarioName}_{timestamp}.png";

            // Captura o screenshot da página.
            Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

            // Salva o screenshot na pasta especificada.
            string screenshotFilePath = Path.Combine(_screenshotsDirectory, screenshotFileName);
            screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);

            Console.WriteLine($"Screenshot salvo em: {screenshotFilePath}");
        }
    }
}
