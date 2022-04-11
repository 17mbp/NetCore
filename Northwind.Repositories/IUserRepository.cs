using Northwind.Model;

namespace Northwind.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User ValidateUser(string email, string password);
    }
}