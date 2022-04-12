using Northwind.BusinessLogic.Interfaces;
using Northwind.Model;
using Northwind.UnitOfWork;
namespace Northwind.BusinessLogic.Implementations
{
    public class Token : ITokenLogic
    {
        private readonly IUnityOfWork _unityOfWork;
        public Token(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public User ValidateUser(string email, string password)
        {
            return _unityOfWork.User.ValidateUser(email, password);
        }
    }
}