using WebCrawler.Esportal;
using WebCrawler.Esportal.Model;

var crawler = new EsportalCrawler(ProfileRequestConfig.AllTrue(7476399));

var response = crawler.Start();
Console.WriteLine(response);

Console.ReadLine();