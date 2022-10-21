using Database;
using Microsoft.AspNetCore.Mvc;

using Api.Services;
using Api.Dto;
using Api.Dto.Query;

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

    [HttpGet("Username/{q}")]
    public async Task<ServiceResponse<IEnumerable<QueryResponse>>> Username([FromRoute] string q)
    {
        var res = new ServiceResponse<IEnumerable<QueryResponse>>();
        try
        {
            _logger.LogInformation("Search: \"{}\"", q);
            res.Data = await _query.Search(q);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Search error: \"{}\"", e.Message);
            res.Success = false;
            res.Error = "Search failed for some reason :'(";
        }
        return res;
    }
}