using PluginSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TibiaMobileWeb.DataBase.Tables
{
    public class Players : TableBase
    {
        [PrimaryKey]
        public int id { set; get; }
        public DateTime date_created { set; get; }
        public DateTime last_login { set; get; }
        public int account_id { set; get; }
        public bool beta { set; get; }
        public int sex { set; get; }
        public string name { set; get; }
        public int level { set; get; }
        public int experience { set; get; }
        public float speed { set; get; }
        public int vocation { set; get; }
        public int direction { set; get; }
        public int heal_current { set; get; }
        public int heal_max { set; get; }
        public int mana_current { set; get; }
        public int mana_max { set; get; }
        public int money { set; get; }
        public int look { set; get; }
        public int head { set; get; }
        public int body { set; get; }
        public int legs { set; get; }
        public int feet { set; get; }
        public int attack { set; get; }
        public int defence { set; get; }
        public int pos_x { set; get; }
        public int pos_y { set; get; }
        public int pos_z { set; get; }


        public static Players Get(int player_id)
        {
            return MYSQL.Query<Players>($"SELECT * FROM players WHERE id='{player_id}'").FirstOrDefault();
        }

        public static Players Get(string player_name)
        {
            return MYSQL.Query<Players>($"SELECT * FROM players WHERE name='{player_name}'").FirstOrDefault();
        }

        public static List<Players> Gets(PlayerOrder order, int limit = 20)
        {
            string _order = string.Empty;
            switch (order)
            {
                case PlayerOrder.name:
                    _order = "name";
                    break;
                case PlayerOrder.level:
                    _order = "level";
                    break;
                case PlayerOrder.experience:
                    _order = "experience";
                    break;
            }
            return MYSQL.Query<Players>($"SELECT * FROM players ORDER BY {_order} DESC LIMIT {limit}");
        }
    }
}
