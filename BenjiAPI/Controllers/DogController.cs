using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Business;
using DataExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace BenjiAPI
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("*", "*", "*")]
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
        [Route("GetAll")]
        public List<DogModel> GetAll()
        {
            return _dogManager.GetAllDogs();
        }
        [HttpGet]
        [Route("Get/{Id}")]
        [EnableCors("*", "*", "*")]
        public DogModel Get(int Id)
        {
            return _dogManager.GetDogById(new DogId() { Value = Id });
        }
        [HttpPost]
        [Route("Add")]
        public bool Add([FromBody] DogCreateRequest request)
        {
            return _dogManager.CreateNewDog(request);
        }
    }
}
