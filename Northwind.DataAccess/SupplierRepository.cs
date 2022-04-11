using Dapper;
using Northwind.Model;
using Northwind.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Northwind.DataAccess
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(string cnnstring) : base(cnnstring) { }
        public IEnumerable<Supplier> SupplierPagedList(int page, int rows, string term)
        {
            var parmeters = new DynamicParameters();
            parmeters.Add("@page",page);
            parmeters.Add("@rows", rows);
            parmeters.Add("@searchTerm", term);
            using (var conn = new SqlConnection(_cnnstring))
            {
                return conn.Query<Supplier>("dbo.SupplierPagedList", parmeters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}