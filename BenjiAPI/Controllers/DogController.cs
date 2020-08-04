using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace BenjiAPI
{
    [ApiController]
    [Route("[controller]")]
    public class DogController : ControllerBase
    {
        private DogManager _dogManager;
        private readonly ILogger<DogController> _logger;

        public DogController(ILogger<DogController> logger, DogManager dogManager)
        {
            _logger = logger;
            _dogManager = dogManager;
        }

        [HttpGet]
        public IEnumerable<DogModel> Get()
        {
            return _dogManager.GetAllDogs();
        }
        //[HttpGet]
        //public IEnumerable<DogModel> GetAllDogs()
        //{
        //    return _dogManager.GetAllDogs();
        //}
    }
}
