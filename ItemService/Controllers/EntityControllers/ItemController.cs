/// <summary>
/// Author: Connor Trempe
/// 
/// Controller that encompasses operations on the Item Service entity Item.
/// The Item model has has one Child model, so this controller implements IApiControllerWithChildren
/// This controller marks the end of a polymorphism tree (Nothing extends or implements this class) so this class is sealed
/// Sealing classes prervents ill advised overriding of class methods along with a small performance gain at runtime.
/// See https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed for more details
/// </summary>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ItemService.Models;
using ItemService.Data;

using ServicesCommonLibrary.ApiController;

namespace ItemService.Controllers {
    [ApiController]
    [Route("api/[controller")]
    public sealed class ItemController : ItemServiceController<Item>, IApiControllerWithChildren<Item> {

        public ItemController(ItemContext context) : base(context){ }

        [HttpGet("WithChildren")]
        public Task<List<Item>> GetWithChildren(){
            return _context.Items.Include(i => i.ItemCategory).ToListAsync();
        }
       
        [HttpGet("WithChildren/{id}")]
        public Task<Item?> GetWithChildren(int id){
            return _context.Items.Where(i => i.Id == id).Include(i => i.ItemCategory).FirstOrDefaultAsync();
        }

        [HttpGet("WithChildren/GetByIds")]
        public Task<List<Item>> GetWithChildren(int[] ids){
            return _context.Items.Where(i => ids.Contains(i.Id)).Include(i => i.ItemCategory).ToListAsync();
        }
    }
}