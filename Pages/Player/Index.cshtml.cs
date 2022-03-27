using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TibiaMobileWeb.DataBase.Tables;

namespace TibiaMobileWeb.Pages.Player
{
    public class IndexModel : PageModel
    {
        public Players player { set; get; }

        public void OnGet(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return;
            }

            player = Players.Get(name);
            Debug.WriteLine($"[PLAYER] {player?.name}");

        }

    }
}
