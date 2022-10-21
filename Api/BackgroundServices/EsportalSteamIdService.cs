using WebCrawler.Esportal;

namespace Api.BackgroundServices;

public class EsportalSteamIdBackgroundService : BackgroundService
{
    private readonly ILogger<EsportalSteamIdBackgroundService> _logger;
    private readonly EsportalSteamIdCrawler _crawler;
    private double _retries;
    private readonly double _baseDelay;
    private double _currentDelay;

    private PeriodicTimer _timer;


    public EsportalSteamIdBackgroundService(ILogger<EsportalSteamIdBackgroundService> logger, EsportalSteamIdCrawler crawler)
    {
        _logger = logger;
        _crawler = crawler;
        _retries = 0;
        _baseDelay = 8000;
        _currentDelay = _baseDelay;
        _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(_baseDelay));
    }

    private async Task<int> Action()
    {
        if (_retries > 100) throw new Exception("Too many retries.");

        var profileEntity = await _crawler.Next();
        var userId = profileEntity?.Id;
        var success = await _crawler.Act(userId);

        if (success)
        {
            _retries = 0;
        }
        else
        {
            _retries++;
        }

        var delay = _baseDelay + _baseDelay * Math.Pow(_retries * 0.2, 2) * 10;
        return (int)delay;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            var delay = await Action();

            if (delay != _currentDelay)
            {
                _currentDelay = delay;
                var seconds = Math.Round(((double)delay) / 1000 * 10) / 10;
                _logger.LogWarning("Backing off for {seconds} seconds. At attempt number {attempt}", seconds, _retries);
                _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(_currentDelay));
            }
        }
    }
}