using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
//using Proprette.Domain.Data.Models;
//using Proprette.Domain.Services.DataSeeding;

namespace Proprette.API.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    //public class WarehouseController : ControllerBase
    //{
    //    private readonly PropretteNewDbContext context;
    //    private readonly IMapper mapper;
    //    private readonly IPopulateTable<WarehouseDto> table;

    //    public WarehouseController(PropretteNewDbContext context,
    //                                IPopulatorFactory factory,
    //                                IMapper mapper)
    //    {
    //        this.context = context;
    //        this.table = (factory != null) ? factory.CreateWarehousePopulator<WarehouseDto>() : throw new ArgumentNullException(nameof(factory));
    //        this.mapper = mapper;
    //    }

    //    [HttpGet]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //    public async Task<ActionResult<IEnumerable<WarehouseDto>>> Get(string? itemName, int? maxCount, DateTime? maxDateTime)
    //    {
    //        if(maxCount <= 0)
    //        {
    //            return BadRequest("Field count could not be less then zero!");
    //        }
    //        var query = context.Set<Warehouse>().AsNoTracking();
    //        if(itemName != null)
    //        {
    //            query = query.Where(el => el.Item.ItemName.Contains(itemName));
    //        }
    //        if(maxCount != null)
    //        {
    //            query = query.Where(el => el.Count < maxCount);
    //        }
    //        if(maxDateTime != null)
    //        {
    //            query = query.Where(el=> el.DateTime <=  maxDateTime);   
    //        }

    //        var res = await query
    //            .Include(el => el.Item)
    //            .ToListAsync();
    //        if (res.Count == 0)
    //        {
    //            return NotFound();
    //        }
    //        return Ok(mapper.Map<IEnumerable<WarehouseDto>>(res));
    //    }

    //    [HttpPost]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //    public async Task<ActionResult<IEnumerable<Warehouse>>> Update(IEnumerable<WarehouseDto> items)
    //    {
    //        if (items == null)
    //        {
    //            return BadRequest();    
    //        }
    //        if(items.Count() == 0)
    //        {
    //            return Ok("Nothing to updated");
    //        }
    //        await table.UpdateOrInsert(items);

    //        var itemNameKeys = items.Select(el => el.ItemName).ToHashSet(); 
    //        var itemTypeKeys = items.Select(el => el.ItemType).ToHashSet();
    //        var dataTimeKeys = items.Select(el => el.DateTime).ToHashSet();
    //        var combinedKeys = items.Select(el => new {el.ItemName, el.ItemType, el.DateTime}).ToHashSet();

    //        var rows = await context.Set<Warehouse>()
    //            .AsNoTracking()
    //            .Where(el => itemNameKeys.Contains(el.Item.ItemName)
    //            && itemTypeKeys.Contains(el.Item.ItemType)
    //            && dataTimeKeys.Contains(el.DateTime))
    //            .Include(el => el.Item)
    //            .ToListAsync();
    //        rows = rows.Where(el => combinedKeys.Contains(new { el.Item.ItemName, el.Item.ItemType, el.DateTime })).ToList();

    //        return Ok(rows);
    //    }

    //    [HttpDelete]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //    public async Task<IActionResult> Delete(IEnumerable<WarehouseDto> items)
    //    {
    //        if(items == null)
    //        {
    //            return BadRequest();
    //        }
    //        if (items.Count() == 0)
    //        {
    //            return Ok("Nothing to delete");
    //        }

    //        var itemNameKeys = items.Select(el => el.ItemName).ToHashSet();
    //        var itemTypeKeys = items.Select(el => el.ItemType).ToHashSet();
    //        var dataTimeKeys = items.Select(el => el.DateTime).ToHashSet();
    //        var combinedKeys = items.Select(el => new { el.ItemName, el.ItemType, el.DateTime }).ToHashSet();

    //        var rows = await context.Set<Warehouse>()
    //            .Where(el => itemNameKeys.Contains(el.Item.ItemName)
    //            && itemTypeKeys.Contains(el.Item.ItemType)
    //            && dataTimeKeys.Contains(el.DateTime))
    //            .Include(el => el.Item)
    //            .ToListAsync();
    //        rows = rows.Where(el => combinedKeys.Contains(new { el.Item.ItemName, el.Item.ItemType, el.DateTime })).ToList();
    //        if (!rows.Any())
    //        {
    //            return NotFound();
    //        }
    //        context.Set<Warehouse>().RemoveRange(rows);
    //        await context.SaveChangesAsync();
    //        return Ok();
    //    }
    //}
}
