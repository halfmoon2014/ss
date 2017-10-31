using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace weixin.pojson
{
    /// <summary>
    ///WeiXinQYHMessageText 的摘要说明
    /// </summary>
    [JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public class WeiXinQYHMessageText
    {
        [JsonProperty("touser")]
        private String touser;

        [JsonProperty("toparty")]
        private String toparty;

        [JsonProperty("totag")]
        private String totag;

        [JsonProperty("msgtype")]
        private String msgtype;

        [JsonProperty("agentid")]        
        private int agentid;

        [JsonProperty("text")]
        private WeiXinQYHMessageTextContent text;

        [JsonProperty("safe")]
        private String safe;

        //[JsonIgnore]
        public String Touser
        {
            get
            {
                return touser;
            }
            set { touser = value; }
        }
        public String Toparty
        {
            get
            {
                return toparty;
            }
            set { toparty = value; }
        }
        public String Totag
        {
            get
            {
                return totag;
            }
            set { totag = value; }
        }
        public String Msgtype
        {
            get
            {
                return msgtype;
            }
            set { msgtype = value; }
        }
        public int Agentid
        {
            get
            {
                return agentid;
            }
            set { agentid = value; }
        }

        public WeiXinQYHMessageTextContent Text
        {
            get
            {
                return text;
            }
            set { text = value; }
        }
        public String Safe
        {
            get
            {
                return safe;
            }
            set { safe = value; }
        }
    }
    [JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public class WeiXinQYHMessageTextContent
    {
        [JsonProperty("content")]
        private String content;
        
        //[JsonIgnore]
        public String Content
        {
            get
            {
                return content;
            }
            set { content = value; }
        }
    }
}
