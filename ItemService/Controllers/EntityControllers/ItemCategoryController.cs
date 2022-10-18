/// <summary>
/// Author: Connor Trempe
/// 
/// Controller that encompasses operations on the Item Service entity Item Category.
/// The ItemCategory model has has no Child models, so this controller doesnt implement IApiControllerWithChildren
/// This controller marks the end of a polymorphism tree (Nothing extends or implements this class) so this class is sealed
/// Sealing classes prervents ill advised overriding of class methods along with a small performance gain at runtime.
/// See https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed for more details
/// </summary>

using Microsoft.AspNetCore.Mvc;

using ItemService.Models;
using ItemService.Data;

namespace ItemService.Controllers {
    [ApiController]
    [Route("api/[controller")]
    public sealed class ItemCategoryController : ItemServiceController<ItemCategory> {
        public ItemCategoryController(ItemContext context) : base(context){ }
    }
}