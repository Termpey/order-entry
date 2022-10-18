/// <summary>
/// Author: Connor Trempe
/// 
/// Generic interface that encompasses all required endpoints for a Order Entry controller.
/// Service controllers should extend this interface and provide implementations for these methods
/// </summary>

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ServicesCommonLibrary.ApiController {
    public interface IApiController<T> {
        // Read only database context necessary for service controllers
        DbContext Context { get; }

        [HttpGet("")]
        public Task<List<T>> Get();

        [HttpGet("{id}")]
        public Task<T?> Get(int id);

        [HttpGet("GetByIds")]
        public Task<List<T>> Get([FromQuery] int[] Ids);

        [HttpPut("Update")]
        public Task<int> Update([FromBody] T updateObject);

        [HttpPut("BulkUpdate")]
        public Task<int> Update([FromBody] List<T> updateObjects);

        [HttpPost("Add")]
        public Task<int> Add([FromBody] T addObject);

        [HttpPost("BulkAdd")]
        public Task<int> Add([FromBody] List<T> addObjects);
    }
}