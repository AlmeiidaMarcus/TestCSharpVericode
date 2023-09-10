using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Net;

public enum BrowserType
{
    Chrome,
    Firefox,
    Edge
}

public class WebDriverFactory
{
    private static string driversDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Drivers");

    public static IWebDriver CreateWebDriver(BrowserType browserType)
    {
        string driverPath = GetDriverPath(browserType);

        if (string.IsNullOrEmpty(driverPath))
        {
            Console.WriteLine($"Driver for {browserType} not found. Attempting to download...");

            if (DownloadDriver(browserType))
            {
                driverPath = GetDriverPath(browserType);
            }
            else
            {
                throw new Exception($"Could not download {browserType} driver.");
            }
        }

        switch (browserType)
        {
            case BrowserType.Chrome:
                var chromeOptions = new ChromeOptions();
                // Configure Chrome options as needed
                return new ChromeDriver(driverPath, chromeOptions);

            case BrowserType.Firefox:
                var firefoxOptions = new FirefoxOptions();
                // Configure Firefox options as needed
                return new FirefoxDriver(driverPath, firefoxOptions);

            case BrowserType.Edge:
                var edgeOptions = new EdgeOptions();
                // Configure Edge options as needed
                return new EdgeDriver(driverPath, edgeOptions);

            default:
                throw new ArgumentException($"Unsupported browser type: {browserType}");
        }
    }

    private static string GetDriverPath(BrowserType browserType)
    {
        string driverFileName;
        switch (browserType)
        {
            case BrowserType.Chrome:
                driverFileName = "chromedriver.exe";
                break;

            case BrowserType.Firefox:
                driverFileName = "geckodriver.exe";
                break;

            case BrowserType.Edge:
                driverFileName = "msedgedriver.exe";
                break;

            default:
                return null;
        }

        string driverPath = Path.Combine(driversDirectory, driverFileName);
        if (File.Exists(driverPath))
        {
            return driverPath;
        }

        return null;
    }

    private static bool DownloadDriver(BrowserType browserType)
    {
        string driverFileName;
        string downloadUrl;

        switch (browserType)
        {
            case BrowserType.Chrome:
                driverFileName = "chromedriver.exe";
                downloadUrl = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE";
                break;

            case BrowserType.Firefox:
                driverFileName = "geckodriver.exe";
                downloadUrl = "https://github.com/mozilla/geckodriver/releases/latest";
                break;

            case BrowserType.Edge:
                driverFileName = "msedgedriver.exe";
                downloadUrl = "https://msedgedriver.azureedge.net/LATEST_STABLE";
                break;

            default:
                return false;
        }

        try
        {
            using (WebClient client = new WebClient())
            {
                string latestVersion = client.DownloadString(downloadUrl).Trim();
                string downloadLink = $"{latestVersion}/{driverFileName}";

                string driverPath = Path.Combine(driversDirectory, driverFileName);
                client.DownloadFile(downloadLink, driverPath);

                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to download {browserType} driver: {ex.Message}");
            return false;
        }
    }
}
