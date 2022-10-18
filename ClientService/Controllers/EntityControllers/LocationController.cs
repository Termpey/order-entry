/// <summary>
/// Author: Connor Trempe
/// 
/// Controller that encompasses operations on the IClientServiceModel entity Location.
/// The Location model has one Child model Contacts, so this controller implements IApiControllerWithChildren
/// This controller marks the end of a polymorphism tree (Nothing extends or implements this class) so this class is sealed
/// Sealing classes prervents ill advised overriding of class methods along with a small performance gain at runtime.
/// See https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed for more details
/// </summary>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ClientService.Models;
using ClientService.Data;

using ServicesCommonLibrary.ApiController;

namespace ClientService.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public sealed class LocationController : ClientServiceController<Location>, IApiControllerWithChildren<Location> {
        public LocationController(ClientContext context) : base(context){ }

        /// <summary>
        /// Gets all Location objects with  Contacts included if children exist
        /// </summary>
        /// <returns>List of Location with child models</returns>
        [HttpGet("WithChildren")]
        public Task<List<Location>> GetWithChildren(){
            return _context.Locations.Include(l => l.Contacts).ToListAsync();
        }

        /// <summary>
        /// Gets Location object with Contacts included if children exists
        /// Returns Location where Location.Id = id
        /// </summary>
        /// <param name="id">id to search on</param>
        /// <returns>Location where Location.Id = id with child models</returns>
        [HttpGet("WithChildren/{id}")]
        public Task<Location?> GetWithChildren(int id){
            return _context.Locations.Where(l => l.Id == id).Include(l => l.Contacts).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets Location objects with Contacts included if children exists
        /// Returns Location where Location.Id in ids
        /// </summary>
        /// <param name="ids">List of ids to search on</param>
        /// <returns>List of Location where Location.Id in ids with child models</returns>
        [HttpGet("WithChildren/GetByIds")]
        public Task<List<Location>> GetWithChildren([FromQuery] int[] ids){
            return _context.Locations.Where(l => ids.Contains(l.Id)).Include(l => l.Contacts).ToListAsync();
        }
    }
}