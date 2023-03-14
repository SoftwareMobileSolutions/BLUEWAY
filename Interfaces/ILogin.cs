using System.Collections.Generic;
using System.Threading.Tasks;
using BLUEWAY.Models;
namespace BLUEWAY.Interfaces
{
    public interface ILogin
    {
        public Task<IEnumerable<LoginModel>> authlogin(string usuario, string password);
    }
}
