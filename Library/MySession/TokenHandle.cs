using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySession
{
    public static class TokenHandle
    {
        private static readonly List<TokenItem> tokenList = new List<TokenItem>();
        public static List<TokenItem> GetTokenList()
        {
            return tokenList;
        }
        public static TokenItem GetToken(string token)
        {
            for (int i = 0; i < tokenList.Count; i++)
            {
                if (tokenList[i].Token.Equals(token, StringComparison.OrdinalIgnoreCase))
                {
                    return tokenList[i];
                }
            }
            return null;
        }

        public static TokenItem AddUserid(int userid)
        {
            bool isExists = false;
            TokenItem item=new TokenItem();
            for (int i = 0; i < tokenList.Count; i++)
            {
                if (tokenList[i].Userid.Equals(userid))
                {
                    isExists = true;
                    item= tokenList[i];
                }
            }
            if (!isExists)
            {
                item = new TokenItem(userid);
                tokenList.Add(item);
               
            }
            return item;

        }

        public static void DelUserid(int userid)
        {   
            
            for (int i = 0; i < tokenList.Count; i++)
            {
                if (tokenList[i].Userid.Equals(userid))
                {
                    tokenList.Remove(tokenList[i]);
                }
            }           

        }

        public static TokenItem GetTokenByUserid(int userid)
        {
            for (int i = 0; i < tokenList.Count; i++)
            {
                if (tokenList[i].Userid.Equals(userid))
                {
                    return tokenList[i];
                }
            }
            return null;
        }


        public static void AddToken(TokenItem item)
        {
            bool isExists = false;
            for (int i = 0; i < tokenList.Count; i++)
            {
                if (tokenList[i].Token.Equals(item.Token, StringComparison.OrdinalIgnoreCase))
                {
                    tokenList[i] = item;
                    isExists = true;
                    break;
                }
            }
            if (!isExists) tokenList.Add(item);
        }
    }
}
