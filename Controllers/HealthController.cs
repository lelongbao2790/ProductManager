using BEProductManager.Cache;
using BEProductManager.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BEProductManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly SakilaContext _dbContext;
        private readonly ICacheService _cacheService;
        public HealthController(SakilaContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        [HttpGet(Name = "GetHealth")]
        public async Task<IResult> Get()
        {
            try
            {
                var db = ConnectionHelper.Connection.GetDatabase();
                await db.PingAsync();
                return Results.Ok(new { status = "healthy", redis = "connected" });
            }
            catch
            {
                return Results.StatusCode(503);
            }
        }
    }
}
