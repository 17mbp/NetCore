using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.BusinessLogic.Interfaces;
namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]    
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderLogic _unitOfWork;
        public OrderController(IOrderLogic unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetPaginatedOrders/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedOrders(int page, int rows)
        {
            return Ok(_unitOfWork.GetPaginatedOrders(page, rows));
        }         
        [HttpGet]
        [Route("GetOrderById/{orderId:int}")]
        public IActionResult GetOrderById(int orderId)
        {
            return Ok(_unitOfWork.GetOrderById(orderId));
        }
    }
}