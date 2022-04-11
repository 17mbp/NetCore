﻿using Northwind.Model;
using System.Collections.Generic;

namespace Northwind.Repositories
{
    public interface ICustomer : IRepository<Customer> 
    {
        IEnumerable<CustomerList> CustomersPagedList(int page, int rows);         
    }
}