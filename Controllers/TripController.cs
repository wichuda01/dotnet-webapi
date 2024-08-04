using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi_ef.Data;
using dotnet_webapi_ef.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi_ef.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
          // Attribute context is private and readonly
        // _name => only use in this class => this.xxxx (_==this)
        private readonly ApplicationDBContext _context;
        public TripController(ApplicationDBContext context)
        {
            _context = context;
        }

        //create API
        //GET http://Localhost:5000/api/trip เส้นจะเป็นงี้
        [HttpGet]//บอกว่าเป็นเส้นอะไร
        public IActionResult GetAll(){
            //include=การจอย
            var trips=_context.Trips.Include(d=>d.Destination).Select(t=>t.ToTripDTO());//เอา(Select)แต่ละตัวมาแปลงๆๆแล้วส่งออกมา
            return Ok(trips);
        }

        [HttpGet("{id}")]//get have path parameter
        public IActionResult GetByID([FromRoute] int id){
            // var trip=_context.Trips.Find(id);
            var trip=_context.Trips.Include(i=>i.Destination).Where(t=>t.Idx==id).FirstOrDefault();
            if (trip ==null)
            {
                return NotFound();
            }else{
                return Ok(trip.ToTripDTO());//เรียกใช้ DTO
            }
            
        }
    }
}