using WebCrawler.Esportal;

var crawler = new EsportalCrawler();
Console.WriteLine(crawler.Bootstrap());
//await Task.Delay(5000);
//Console.WriteLine(crawler.Stop());

Console.ReadLine();