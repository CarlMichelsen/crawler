using Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

CrawlerSingleton.Instance.Start();

app.MapGet("/", () => "Weeee!");

app.MapGet("/start", () => CrawlerSingleton.Instance.Start());

app.MapGet("/stop", () => CrawlerSingleton.Instance.Stop());

app.MapGet("/bootstrap", async () => await CrawlerSingleton.Instance.Bootstrap());

Console.WriteLine("Started");
app.Run();
