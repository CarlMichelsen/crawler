using WebCrawler.Esportal;
using WebCrawler.Esportal.Model;

var crawler = new EsportalCrawler(ProfileRequestConfig.AllTrue(7476399));

Console.WriteLine(crawler.Start());

await Task.Delay(5000);

Console.WriteLine(crawler.Stop());

Environment.GetEnvironmentVariable("DATABASE");

Console.ReadLine();