using System.Threading.Tasks;
using MinyToDo.Models.Entity;

namespace MinyToDo.WebAPI.Services.Abstract
{
    public interface IJwtTokenService
    {
         Task<string> CreateTokenAsync(AppUser appUser);
    }
}