using System;
using Newtonsoft.Json;
namespace weixin.pojson
{
    /// <summary>
    /// access_token返回
    /// </summary>
    public class AccessToken
    {
        [JsonProperty("access_token")]
        private String access_token;

        [JsonProperty("expires_in")]
        private String expires_in;

        [JsonProperty("errcode")]
        private String errcode;

        [JsonProperty("errmsg")]
        private String errmsg;

        public String Access_token
        {
            get
            {
                return access_token;
            }
            set { access_token = value; }
        }
        public String Expires_in
        {
            get
            {
                return expires_in;
            }
            set { expires_in = value; }
        }
        public String Errcode
        {
            get
            {
                return errcode;
            }
            set { errcode = value; }
        }
        public String Errmsg
        {
            get
            {
                return errmsg;
            }
            set { errmsg = value; }
        }
    }    
}
