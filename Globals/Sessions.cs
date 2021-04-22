using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TibiaMobileWeb
{
    public static class Sessions
    {
        /*
        public static Cuentas GetSession(this HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                Cuentas cuenta = SQLFunctions.GetAccountByID(context.User.Identity.Name);
                return cuenta;
            }
            return null;
        }

        public static Cuentas GetHubSession(this HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                string user_id = context.User.Identity.Name;
                Cuentas cuenta = SQLFunctions.GetAccountByID(user_id);
                return cuenta;
            }

            return null;
        }*/
    }
}
