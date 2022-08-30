using Api.Handlers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Weeee!");

app.MapGet("/start", () => CrawlerHandler.Instance.Start());

app.MapGet("/stop", () => CrawlerHandler.Instance.Stop());

app.MapGet("/bootstrap", async () => await CrawlerHandler.Instance.Bootstrap());

Console.WriteLine("Started");
app.Run();
