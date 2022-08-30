using Api.Handlers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/start", () => CrawlerHandler.Instance.Start());

app.MapGet("/stop", () => CrawlerHandler.Instance.Stop());

Console.WriteLine("Started");
app.Run();
