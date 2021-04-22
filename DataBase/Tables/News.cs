using PluginSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TibiaMobileWeb.DataBase.Tables
{
    public class News : TableBase
    {
        [PrimaryKey]
        public int id { set; get; }
        public DateTime date_created { set; get; }
        public DateTime date_updated { set; get; }
        public string title { set; get;}
        public string content { set; get; }
    }
}
