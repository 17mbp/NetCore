using Microsoft.AspNetCore.Mvc;
using Northwind.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Northwind.BusinessLogic.Interfaces;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/customer")]
    [Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
    public class CustomerController : Controller
    {
        private readonly ICustomerLogic _unityOfWork;
        public CustomerController(ICustomerLogic unitOfWork)
        {
            _unityOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("test")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "ejemplo" };
        }
        [HttpGet]
        [Route("GetPaginatedCustomer/{page:int}/{rows:int}")]
        public IActionResult GetPaginateCustomer(int page, int rows)
        { 
            return Ok(_unityOfWork.CustomerPagedList(page, rows));
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id) {
            return Ok(_unityOfWork.GetById(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                return Ok(_unityOfWork.Insert(customer));            
        }
        [HttpPut]
        public IActionResult Put([FromBody]Customer customer)
        {
            if (ModelState.IsValid && _unityOfWork.Update(customer))
            {
                return Ok(new { Message = "Si Update" });
            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody]Customer customer)
        {
            if (customer.Id > 0)
            {
                return Ok(_unityOfWork.Delete(customer));
            }
            return BadRequest();
        }
    }
}