using Dapper;
using Northwind.Model;
using Northwind.Repositories;
using System.Data.SqlClient;

namespace Northwind.DataAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(string cnnstring) : base(cnnstring)
        {
        }

        public User ValidateUser(string email, string password)
        {
            DynamicParameters parmeters = new DynamicParameters();
            parmeters.Add("@email", email, System.Data.DbType.String);
            parmeters.Add("@password", password, System.Data.DbType.String);

            using (var connection = new SqlConnection(_cnnstring))
            {
                return connection.QueryFirstOrDefault<User>(
                    "dbo.ValidateUser",parmeters,
                    commandType:System.Data.CommandType.StoredProcedure);
            }
        }
    }
}