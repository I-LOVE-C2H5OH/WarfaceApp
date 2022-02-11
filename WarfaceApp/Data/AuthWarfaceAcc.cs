using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesRegger.Data
{
    interface AuthWarfaceAcc
    {
        public List<Cookie> GetWarfaceCookies()
        { return new List<Cookie>(); }
        public string GetFisrstName()
        {
            return "";
        }
    }
    class WarfaceAccZaglush : AuthWarfaceAcc
    {
        public WarfaceAccZaglush()
        { }

    }
}
