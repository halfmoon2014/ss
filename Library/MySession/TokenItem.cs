using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySession
{
    public class TokenItem
    {
        private int userid;
        private int tzid;

        private string token = System.Guid.NewGuid().ToString();
        public TokenItem()
        {          
        }
        public TokenItem(int userid) {
            this.userid = userid;
        }
        public int Userid { get => userid; set => userid = value; }
        public int Tzid { get => tzid; set => tzid = value; }
      

        public string Token { get => token;  }

    }
}
