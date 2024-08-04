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
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CustomerController(ApplicationDBContext context){
            _context=context;
        }

        [HttpGet]
        public IActionResult GetCustomerAll(){
            var customers =_context.Customers.Select(s=>s.ToCustomerDTO());
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerByID([FromRoute] int id){
            var customer=_context.Customers.Find(id);
            if (customer==null)
            {
                return NotFound();
            }else{
                return Ok(customer.ToCustomerDTO());
            }
            
        }

        [HttpGet("{name}")]
        public IActionResult GetCustomerByName([FromRoute] String name){
            var customer=_context.Customers.Where(c=>c.Fullname.Contains(name));
            if (customer==null)
            {
                return NotFound();
            }else{
                return Ok(customer);
            }
        }
    }
}