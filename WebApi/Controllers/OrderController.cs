using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Model;
using Northwind.UnitOfWork;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]    
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnityOfWork _unitOfWork;
        public OrderController (IUnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetPaginatedOrders/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedOrders(int page, int rows)
        {
            return Ok(_unitOfWork.Order.getPaginatedOrder(page, rows));
        }
        [HttpPost]
        [Route("GetPaginatedSupplier")]///{page:int}/{rows:int}")]
        public IActionResult GetPaginatedSupplier([FromBody] GetPaginatedSupplier request)
        {
            return Ok(_unitOfWork.Supplier.SupplierPagedList(request.Page, request.Rows, request.SearchTerm));
        }
        [HttpGet]
        [Route("GetOrderById/{orderId:int}")]
        public IActionResult GetOrderById(int orderId)
        {
            return Ok(_unitOfWork.Order.GetOrderById(orderId));
        }
    }
}