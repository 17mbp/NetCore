using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Model;
using Northwind.UnitOfWork;
using WebApi.Models;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly IUnityOfWork _unitofwork;
        public SupplierController(IUnityOfWork unityOfWork)        {
            _unitofwork = unityOfWork;
        }
        [HttpPost]
        [Route("GetPaginatedSupplier")]///{page:int}/{rows:int}")]
        public IActionResult GetPaginateSupplier([FromBody] GetPaginatedSupplier request)
        {
            return Ok(_unitofwork.Supplier.SupplierPagedList(request.Page, request.Rows, request.SearchTerm));
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unitofwork.Supplier.GetById(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_unitofwork.Supplier.Insert(supplier));

        }
        [HttpPut]
        public IActionResult Put([FromBody] Supplier supplier)
        {
            if (ModelState.IsValid && _unitofwork.Supplier.Update(supplier))
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
                return Ok(_unitofwork.Supplier.Delete(supplier));
            }
            return BadRequest();
        }
    }
}