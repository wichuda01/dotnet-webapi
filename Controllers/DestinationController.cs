using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi_ef.Data;
using dotnet_webapi_ef.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi_ef.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public DestinationController(ApplicationDBContext context){
            _context=context;
        }
        [HttpGet]
        public IActionResult GetDestAll(){
            var Dest=_context.Destinations.Select(c=>c.ToDestinationDTO());
            return Ok(Dest);
        }
    }
}