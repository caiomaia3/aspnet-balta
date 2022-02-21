using System.Security.Claims;
using Blog.Models;

namespace Blog.Extensions;

public static class RoleClaimExtension
{
   public static IEnumerable<Claim> GetClaims(this User user)
   {
      List<Claim> result = new()
      {
         new Claim(ClaimTypes.Name, user.Email)
      };
      result.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role,x.Slug)));
      return result;
   }   
}