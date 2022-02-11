using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesRegger.Data
{
    class Cookie
    {
        public string name = "";
        public string value = "";
    }
    public class Account
    {
        public string login = "";
        public string password = "";
    }
    public class Accounts
    {
        public Account account = new Account();
        public int id = 0;
        public string nickname = "";
    }
}
