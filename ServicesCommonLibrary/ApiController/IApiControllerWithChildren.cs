/// <summary>
/// Author: Connor Trempe
/// 
/// Generic interface that encompasses standard methods for all Order Entry controllers that return data with child relationships in entity framework.
/// </summary>

using Microsoft.AspNetCore.Mvc;

namespace ServicesCommonLibrary.ApiController {
    public interface IApiControllerWithChildren<T> {

        [HttpGet("WithChildren")]
        public Task<List<T>> GetWithChildren();
       
        [HttpGet("WithChildren/{id}")]
        public Task<T?> GetWithChildren(int id);

        [HttpGet("WithChildren/GetByIds")]
        public Task<List<T>> GetWithChildren(int[] ids);
    }
}