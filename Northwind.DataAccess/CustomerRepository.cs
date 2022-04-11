using Dapper;
using Northwind.Model;
using Northwind.Repositories; 
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace Northwind.DataAccess
{
    public class CustomerRepository : Repository<Customer>, ICustomer 
    {
        public CustomerRepository(string cnnstring) : base(cnnstring)
        {

        }

        public IEnumerable<CustomerList> CustomersPagedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);

            using (var conn = new SqlConnection(_cnnstring))
            {
                return conn.Query<CustomerList>("dbo.CustomerPagedList", 
                                        parameters,
                                        commandType:CommandType.StoredProcedure);
            }
        } 
    }
}