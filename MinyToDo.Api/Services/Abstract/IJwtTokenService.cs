using System.Threading.Tasks;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Services.Abstract
{
    public interface IJwtTokenService
    {
         Task<string> CreateTokenAsync(AppUser appUser);
    }
}