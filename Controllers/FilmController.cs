using BEProductManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net.NetworkInformation;
using BEProductManager.Cache;

namespace BEProductManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly SakilaContext _dbContext;
        private readonly ICacheService _cacheService;
        public FilmController(SakilaContext dbContext, ICacheService cacheService) {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        [HttpGet(Name = "GetFilms")]
        public IEnumerable<Film> Get()
        {
            var cacheData = _cacheService.GetData<IEnumerable<Film>>("Film");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            cacheData = _dbContext.Films.ToList();
            _cacheService.SetData<IEnumerable<Film>>("Film", cacheData, expirationTime);
            return cacheData;
        }
    }
}
