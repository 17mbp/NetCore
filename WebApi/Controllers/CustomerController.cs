using Microsoft.AspNetCore.Mvc;
using Northwind.UnitOfWork;
using Northwind.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [Route("api/customer")]
    [Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
    public class CustomerController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        public CustomerController(IUnityOfWork unitOfWork)
        {
            _unityOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetPaginatedCustomer/{page:int}/{rows:int}")]
        public IActionResult GetPaginateCustomer(int page, int rows)
        { 
            return Ok(_unityOfWork.Customer.CustomersPagedList(page, rows));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id) {
            return Ok(_unityOfWork.Customer.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                return Ok(_unityOfWork.Customer.Insert(customer));            
        }

        [HttpPut]
        public IActionResult Put([FromBody]Customer customer)
        {
            if (ModelState.IsValid && _unityOfWork.Customer.Update(customer))
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
                return Ok(_unityOfWork.Customer.Delete(customer));
            }
            return BadRequest();
        }
    }
}