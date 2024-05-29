using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Gestao.Online.Domain.Authorization
{
    public static class Roles
    {

        public static bool IsAuthenticated(this ClaimsPrincipal User)
        {
            return User?.Claims?.Any(p => p.Type == ClaimTypes.NameIdentifier) ?? false;
        }
    }
}
