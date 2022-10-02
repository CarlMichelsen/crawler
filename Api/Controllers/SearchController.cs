using Microsoft.AspNetCore.Mvc;
using Database.Repositories;
using Database;
using Api.Dto;
using Database.Search;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly DataContext _context;

    public SearchController(DataContext context)
    {
        _context = context;
    }

    private static void Log(string input)
    {
        Console.WriteLine($"[Search] {input}");
    }

    [HttpGet("Username/{q}")]
    public async Task<ServiceResponse<List<ProfileDto>>> Username([FromRoute] string q)
    {
        var res = new ServiceResponse<List<ProfileDto>>();
        try
        {
            Log($"Username Search: {q}");
            var resultList = await SearchRepository.NameSearch(q, _context);
            List<ProfileDto> dtoList = resultList.Select(p => ApiMapper.Mapper.Map<ProfileDto>(p)).ToList();
            Log($"Found {resultList.Count} results from username search: \"{q}\"");
            res.Data = dtoList;
        }
        catch (System.Exception e)
        {
            Log(e.Message);
            res.Success = false;
            res.Error = "Search failed.";
        }
        return res;
    }

    [HttpPost("Filter")]
    public async Task<ServiceResponse<List<ProfileDto>>> Filter([FromBody] SearchFilter search)
    {
        var res = new ServiceResponse<List<ProfileDto>>();
        try
        {
            Log($"Username Search: {search}");
            var resultList = await SearchRepository.FilterSearch(search, _context);
            List<ProfileDto> dtoList = resultList.Select(p => ApiMapper.Mapper.Map<ProfileDto>(p)).ToList();
            Log($"Found {resultList.Count} results from filter search: \"{search}\"");
            res.Data = dtoList;
        }
        catch (System.Exception e)
        {
            Log(e.Message);
            res.Success = false;
            res.Error = "Search failed.";
        }
        return res;
    }
}
