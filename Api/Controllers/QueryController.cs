using Microsoft.AspNetCore.Mvc;

using Api.Services;
using Api.Dto;
using Api.Dto.Query;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QueryController : ControllerBase
{
    private readonly ILogger<QueryController> _logger;
    private readonly IQueryService _query;

    public QueryController(ILogger<QueryController> logger, IQueryService query)
    {
        _logger = logger;
        _query = query;
    }

    [HttpGet("EsportalUsername/{q}")]
    public async Task<ServiceResponse<IEnumerable<QueryResponse>>> EsportalUsername([FromRoute] string q)
    {
        var res = new ServiceResponse<IEnumerable<QueryResponse>>();
        try
        {
            _logger.LogInformation("Search: \"{}\"", q);
            res.Data = await _query.EsportalUsernameSearch(q);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Search error: \"{}\"", e.InnerException?.Message);
            res.Success = false;
            res.Error = "Search failed for some reason :'(";
        }
        return res;
    }

    [HttpGet("General/{q}")]
    public async Task<ServiceResponse<IEnumerable<QueryResponse>>> General([FromRoute] string q)
    {
        var res = new ServiceResponse<IEnumerable<QueryResponse>>();
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            throw new NotImplementedException();
            //_logger.LogInformation("Search: \"{}\"", q);
            //res.Data = await _query.EsportalUsernameSearch(q);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Search error: \"{}\"", e.InnerException?.Message);
            res.Success = false;
            res.Error = "Search failed for some reason :'(";
        }
        return res;
    }
}