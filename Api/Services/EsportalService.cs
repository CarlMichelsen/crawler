using WebCrawler.Esportal;

namespace Services;

public class EsportalService : BackgroundService
{
    private readonly ILogger<EsportalService> _logger;
    private readonly EsportalCrawler _crawler;
    private double _retries;
    private readonly double _baseDelay;
    private double _currentDelay;
    
    private PeriodicTimer _timer;
    

    public EsportalService(ILogger<EsportalService> logger, EsportalCrawler crawler)
    {
        _logger = logger;
        _crawler = crawler;
        _retries = 0;
        _baseDelay = 2500;
        _currentDelay = _baseDelay;
        _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(_baseDelay));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            var delay = await Action();

            if (delay != _currentDelay)
            {
                _currentDelay = delay;
                var seconds = Math.Round(((double)delay)/1000*10)/10;
                _logger.LogWarning("Backing off for {seconds} seconds. At attempt number {attempt}", seconds, _retries);
                _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(_currentDelay));
            }
        }
    }

    private async Task<int> Action()
    {
        if (_retries>100) throw new Exception("Too many retries.");

        var item = await _crawler.Next();
        var success = await _crawler.Act(item?.User.Id);

        if (success)
        {
            _retries = 0;
        }
        else
        {
            _retries++;
        }

        var delay = _baseDelay + _baseDelay * Math.Pow(_retries, 2) * 10;
        return (int)(delay);
    }
}