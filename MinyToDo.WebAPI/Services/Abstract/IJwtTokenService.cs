using System.Threading.Tasks;
using MinyToDo.Models.Entity;

namespace MinyToDo.WebAPI.Services.Abstract
{
    public interface IJwtTokenService
    {
         string CreateToken(AppUser appUser);
    }
}