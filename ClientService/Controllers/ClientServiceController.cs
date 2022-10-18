/// <summary>
/// Author: Connor Trempe
/// 
/// Generic controller for all Client Service entities
/// Extends IApiController and provides common implementations for all client service models and available client context database sets
/// </summary>

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using ClientService.Data;

using ServicesCommonLibrary.ApiController;
using ServicesCommonLibrary.Models;

namespace ClientService.Controllers {
    // Limit generic type T to be a class and a Client Service model, Allows for use of DbContext.Set() (Type needs to be class) and to only allow Service Model Types
    public abstract class ClientServiceController<T> : ControllerBase, IApiController<T> where T : class, IBaseModel {
        // ClientContext access set to protected so child classes can utilize it
        protected readonly ClientContext _context;

        // Readonly param that gets ClientContext IQueryable based on passed generic type T
        private IQueryable<T> GetDynamicQueryable{
            get => _context.Set<T>();
        }

        // Sets maximum number of entities for writing with any client service api
        protected static int BULK_OPERATIONS_MAXIMUM { get => 1000; }

        public DbContext Context {
            get => _context;
        }

        /// <summary>
        /// Constructor for a Client Service controller generically
        /// 
        /// Checks to see if generic Type T pertains to a valid DB set in type Client Context
        /// </summary>
        /// <param name="context">Client Context for database operations</param>
        public ClientServiceController(ClientContext context){
            if(context.Set<T>() == null) {
                throw new Exception("Type of T (" + typeof(T).ToString() + ") Not DbSet of ClientContext.");
            }
            _context = context;
        }

        /// <summary>
        /// Gets all T (IBaseModel) that exists in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public Task<List<T>> Get(){
           return GetDynamicQueryable.ToListAsync();
        }

        /// <summary>
        /// Gets T (IBaseModel) where T.Id equals passed in id
        /// </summary>
        /// <param name="id">Id to search entity on</param>
        /// <returns>T (IBaseModel)</returns>
        [HttpGet("{id}")]
        public Task<T?> Get(int id) {
            return GetDynamicQueryable.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets T (IBaseModel) where T.Id is in passed array of ids
        /// </summary>
        /// <param name="Ids">List of entity Ids to search on</param>
        /// <returns>List of T (IBaseModel)</returns>
        [HttpGet("GetByIds")]
        public Task<List<T>> Get([FromQuery] int[] Ids){
            return GetDynamicQueryable.Where(x => Ids.Contains(x.Id)).ToListAsync();
        }

        /// <summary>
        /// Updates passed entity T (IBaseModel) with passed entity params
        /// </summary>
        /// <param name="updateObject">Entity to be updated</param>
        /// <returns>Integer representing number of entities updated in the database</returns>
        [HttpPut("Update")]
        public Task<int> Update([FromBody] T updateObject){
            _context.Update(updateObject);

            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates passed entities T (IBaseModel) with passed entity params
        /// </summary>
        /// <param name="updateObjects">List of entities to be updated</param>
        /// <returns>Number representing the number of entities updated in the database</returns>
        [HttpPut("BulkUpdate")]
        public Task<int> Update([FromBody] List<T> updateObjects){

            if(updateObjects.Count > BULK_OPERATIONS_MAXIMUM){
                throw new Exception("Cannot exceed " + BULK_OPERATIONS_MAXIMUM + " items when performing bulk operations");
            }

            foreach(IBaseModel model in updateObjects){
                _context.Update(model);
            }

            return _context.SaveChangesAsync();
        }
        
        /// <summary>
        /// Adds new entity T (IBaseModel) to the database
        /// </summary>
        /// <param name="addObject">Entity to write</param>
        /// <returns>Number representing the number of entities written to the database</returns>
        [HttpPost("Add")]
        public Task<int> Add([FromBody] T addObject){
            _context.Add(addObject);

            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds new entities T (IBaseModel) to the database
        /// </summary>
        /// <param name="addObjects">Entities to write</param>
        /// <returns>Number representing the number of entities written to the database</returns>
        [HttpPost("BulkAdd")]
        public Task<int> Add([FromBody] List<T> addObjects){

            if(addObjects.Count > BULK_OPERATIONS_MAXIMUM){
                throw new Exception("Cannot exceed " + BULK_OPERATIONS_MAXIMUM + " items when performing bulk operations");
            }

            foreach(IBaseModel model in addObjects){
                _context.Add(model);
            }

            return _context.SaveChangesAsync();
        }
    }
}