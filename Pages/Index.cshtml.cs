using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TibiaMobileWeb.DataBase.Tables;

namespace TibiaMobileWeb.Views.Home
{
    public class IndexModel : PageModel
    {
        public List<Players> players { set; get; }
        public void OnGet()
        {
            players = Players.Gets(PlayerOrder.experience);
        }
    }
}
