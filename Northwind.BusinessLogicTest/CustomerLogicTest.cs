﻿using FluentAssertions;
using Northwind.BusinessLogic.Implementations;
using Northwind.BusinessLogic.Interfaces;
using Northwind.BusinessLogicTest.Mocked;
using Northwind.Model;
using Northwind.UnitOfWork;
using Xunit;
namespace Northwind.BusinessLogicTest
{
    public class CustomerLogicTest
    {
        private readonly IUnityOfWork _unitMocked;
        private readonly ICustomerLogic _customerLogic;
        public CustomerLogicTest()
        {
            var unitMocked = new CustomerRepositoryMocked();
            _unitMocked = unitMocked.GetInstance();
            _customerLogic = new CustomerLogic(_unitMocked);
        }
        [Fact(DisplayName = "[CustomerLogic] Update")]
        public void Update_Customer_Test()
        {
            var customer = new Customer
            {
                Id = 1,
                City = "Lima",
                Country = "México",
                FirstName = "Pablo",
                LastName = "Neruda",
                Phone = "69833898"
            };
            var result = _customerLogic.Update(customer);
            var currentCustomer = _customerLogic.GetById(1);
            currentCustomer.Should().NotBeNull();
            currentCustomer.Id.Should().Be(customer.Id);
            currentCustomer.City.Should().Be(customer.City);
            currentCustomer.Country.Should().Be(customer.Country);
            currentCustomer.FirstName.Should().Be(customer.FirstName);
            currentCustomer.LastName.Should().Be(customer.LastName);
            currentCustomer.Phone.Should().Be(customer.Phone); 
        }
        [Fact(DisplayName ="[CustomerLogic] Insert")]
        public void Insert_Customer_Test()
        {
            var customer = new Customer
            {
                Id = 101,
                City = "Lima",
                Country = "Peru",
                FirstName = "Cesar",
                LastName = "Vallejo",
                Phone = "6989898"
            };
            var result = _customerLogic.Insert(customer);
            result.Should().Be(101);
        }
        [Fact]
        public void GetById_Customer_Test()
        {
            var result = _customerLogic.GetById(1);
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }
    }
}