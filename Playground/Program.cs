using WebCrawler;
using WebCrawler.Esportal;
using WebCrawler.Esportal.Model;

var crawler = new EsportalCrawler(ProfileRequestConfig.AllTrue(7476399));

var response = crawler.Start();
Console.WriteLine(response);




/*
using System.Net.Http.Headers;

WebRequestQueue queue = new();

IWebRequestQueue.HandleQueueItem handleItem = async (string content, HttpResponseHeaders headers) => {
    if (content is null) return false;
    await Task.Delay(10);
    Console.WriteLine(content);
    return true;
};

TimerCallback callback = async (object? queueObject) => {
    if (queueObject is not IWebRequestQueue || queueObject is null) throw new InvalidDataException("Time object should be instance of object that implements IWebRequestQueue");
    var queue = (WebRequestQueue)queueObject;
    await queue.Next(handleItem);
};

for (int i = 0; i < 100; i++)
{
    queue.Push("https://esportal.com/api/user_profile/get?_=1661712230409&id=957283692&current_match=1&rank=1");
}

Timer timer = new(callback, queue, TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(2500));
*/
Console.ReadLine();