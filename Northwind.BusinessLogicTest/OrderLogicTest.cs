using Northwind.BusinessLogic.Implementations;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Northwind.BusinessLogicTest.Mocked;
using Northwind.BusinessLogic.Interfaces;
using Northwind.UnitOfWork;
using Xunit;
using FluentAssertions;

namespace Northwind.BusinessLogicTest
{
    public class OrderLogicTest
    {
        private readonly IUnityOfWork _unitMocked;
        private readonly IOrderLogic _orderlogic;
        public OrderLogicTest()
        {
            var unitMocked = new OrderRepositoryMocked();
            _unitMocked = unitMocked.GetInstance();
            _orderlogic = new OrderLogic(_unitMocked);
        }
        [Fact]
        public void GetOrderNumber_Order_Test()
        {
           var result = _orderlogic.GetOrderNumber(1);
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
        }
    }
}