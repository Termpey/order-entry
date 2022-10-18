/// <summary>
/// Author: Connor Trempe
/// 
/// Controller that encompasses operations on the IClientServiceModel entity Client.
/// The Client model has two Child models, Locations and Contacts, so this controller implements IApiControllerWithChildren
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
    public sealed class ClientController : ClientServiceController<Client>, IApiControllerWithChildren<Client> {

        // Pass injected ClientContext to the parent class (ClientServiceController) which manages the context
        public ClientController(ClientContext context) : base(context){ }

        /// <summary>
        /// Gets all Client objects with Locations and Contacts included if children exist
        /// </summary>
        /// <returns>List of Clients with child models</returns>
        [HttpGet("WithChildren")]
        public Task<List<Client>> GetWithChildren(){
            return _context.Clients.Include(c => c.Locations).Include(c => c.Contacts).ToListAsync();
        }

        /// <summary>
        /// Gets Client object with locations and Contacts included if children exists
        /// Returns Client where Client.Id = id
        /// </summary>
        /// <param name="id">id to search on</param>
        /// <returns>Client where Client.Id = id with child models</returns>
        [HttpGet("WithChildren/{id}")]
        public Task<Client?> GetWithChildren(int id){
            return _context.Clients.Where(c => c.Id == id).Include(c => c.Locations).Include(c => c.Contacts).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets Client objects with Locations and Contacts included if children exists
        /// Returns Clients where Client.Id in ids
        /// </summary>
        /// <param name="ids">List of ids to search on</param>
        /// <returns>List of Clients where Client.Id in ids with child models</returns>
        [HttpGet("WithChildren/GetByIds")]
        public Task<List<Client>> GetWithChildren([FromQuery] int[] ids){
            return _context.Clients.Where(c => ids.Contains(c.Id)).Include(c => c.Locations).Include(c => c.Contacts).ToListAsync();
        }
    }
}