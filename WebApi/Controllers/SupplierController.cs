using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.BusinessLogic.Interfaces;
using Northwind.Model;
using WebApi.Models;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierLogic _logic;
        public SupplierController(ISupplierLogic unityOfWork)        {
            _logic = unityOfWork;
        }
        [HttpPost]
        [Route("GetPaginatedSupplier")]
        public IActionResult GetPaginateSupplier([FromBody] GetPaginatedSupplier request)
        {
            return Ok(_logic.SupplierPagedList(request.Page, request.Rows, request.SearchTerm));
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_logic.GetById(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_logic.Insert(supplier));

        }
        [HttpPut]
        public IActionResult Put([FromBody] Supplier supplier)
        {
            if (ModelState.IsValid && _logic.Update(supplier))
            {
                return Ok(new { Message = "Si supplier Update" });
            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] Supplier supplier)
        {
            if (supplier.Id > 0)
            {
                return Ok(_logic.Delete(supplier));
            }
            return BadRequest();
        }
    }
}