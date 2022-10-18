/// <summary>
/// Author: Connor Trempe
/// 
/// Controller that encompasses operations on the Client Service entity ClientContact.
/// The ClientContact model has one Child model ContactType, so this controller implements IApiControllerWithChildren
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
    public sealed class ClientContactController : ClientServiceController<ClientContact>, IApiControllerWithChildren<ClientContact> {

        public ClientContactController(ClientContext context) : base(context){ }

        /// <summary>
        /// Gets all ClientContact objects with ContactType included if child exist
        /// </summary>
        /// <returns>List of ClientContact with child model</returns>
        [HttpGet("WithChildren")]
        public Task<List<ClientContact>> GetWithChildren(){
            return _context.ClientContacts.Include(cc => cc.ContactType).ToListAsync();
        }

        /// <summary>
        /// Gets ClientContact object with ContactType included if child exists
        /// Returns ClientContact where ClientContact.Id = id
        /// </summary>
        /// <param name="id">id to search on</param>
        /// <returns>ClientContact where ClientContact.Id = id with child model</returns>
        [HttpGet("WithChildren/{id}")]
        public Task<ClientContact?> GetWithChildren(int id){
            return _context.ClientContacts.Where(cc => cc.Id == id).Include(cc => cc.ContactType).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets ClientContact objects with ContactType included if child exists
        /// Returns ClientContact where ClientContact.Id in ids
        /// </summary>
        /// <param name="ids">List of ids to search on</param>
        /// <returns>List of ClientContact where ClientContact.Id in ids with child model</returns>
        [HttpGet("WithChildren/GetByIds")]
        public Task<List<ClientContact>> GetWithChildren([FromQuery] int[] ids){
            return _context.ClientContacts.Where(cc => ids.Contains(cc.Id)).Include(cc => cc.ContactType).ToListAsync();
        }
    }
}