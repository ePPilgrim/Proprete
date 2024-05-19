using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proprette.Domain.Data.Entities.Category;
using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Models;
using Proprette.Domain.Services;

namespace Proprette.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ItemRequests itemRequests;
    private readonly PropretteDbContext context;

    public ItemController(PropretteDbContext context)
    {
        this.context = context;
        itemRequests = new ItemRequests(context);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ItemDto>>> Get(string? brandPrefix, string? colorPrefix)
    {
        var requests = new[] {
            (brandPrefix == null) ? string.Empty : brandPrefix.Trim().ToLower(), 
            (colorPrefix == null) ? string.Empty : colorPrefix.Trim().ToLower()
        };

        var res = (await itemRequests.FindAsync(requests)).Select(item => new ItemDto()
        {
            Id = item.Key,
            Brand = item.Value[0],
            Color = item.Value[1]
        }).ToArray();

        if (res.Length == 0)
        {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<int>> Update(IEnumerable<string> items)
    {
        if (items == null)
        {
            return BadRequest();
        }
        if (items.Count() == 0)
        {
            return Ok("Nothing to updated");
        }

        var res1 = context.Set<Brand>().ToList();
        var res2 = context.Set<Color>().ToList();

        var itemDtos = items.Select(item => item.Split(',')).Select(item => new ItemDto()
            { 
                Brand = item[0].Trim().ToLower(),
                Color = item[1].Trim().ToLower()    
            }).ToArray();

        var res = new List<int>();
        try {
            res = itemRequests.InsertAsync(itemDtos).Result.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return Ok(res);
    }
}
