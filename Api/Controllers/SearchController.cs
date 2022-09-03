using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Database.Repositories;
using Database;
using Api.Dto;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private DataContext _context;

    public SearchController(DataContext context)
    {
        _context = context;
    }

    private void Log(string input)
    {
        Console.WriteLine($"[Search] {input}");
    }

    [HttpGet("Username")]
    public async Task<ServiceResponse<List<ProfileDto>>> Username(string q)
    {
        var res = new ServiceResponse<List<ProfileDto>>();
        try
        {
            var resultList = await SearchRepository.NameSearch(q, _context);
            List<ProfileDto> dtoList = resultList.Select(p => ApiMapper.Mapper.Map<ProfileDto>(p)).ToList();
            Log($"Found {resultList.Count()} results from username search: \"{q}\"");
            res.Data = dtoList;
        }
        catch (System.Exception e)
        {
            res.Success = false;
            res.Error = e.Message;
        }
        return res;
    }
}