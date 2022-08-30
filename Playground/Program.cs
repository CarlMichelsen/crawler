using WebCrawler.Esportal;

var crawler = new EsportalCrawler();
Console.WriteLine(crawler.Start());
//await Task.Delay(5000);
//Console.WriteLine(crawler.Stop());

Console.ReadLine();