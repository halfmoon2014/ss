using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Widget
{
    /// <summary>
    /// 控件值
    /// </summary>
    public class ClientWidgetDefaultValue
    {
        string session;
        string mrz;
        
        int iszb;

        public string Session
        {
            get
            {
                return session;
            }

            set
            {
                session = value;
            }
        }

        public string Mrz
        {
            get
            {
                return mrz;
            }

            set
            {
                mrz = value;
            }
        }
 

        public int Iszb
        {
            get
            {
                return iszb;
            }

            set
            {
                iszb = value;
            }
        }
    }
    public class ClientWidget
    {
        ClientControlType clientControlType;
        object widget;
        ClientWidgetDefaultValue clientWidgetDefaultValue = new ClientWidgetDefaultValue();

        public object Widget
        {
            get
            {
                return widget;
            }

            set
            {
                widget = value;
            }
        }

        internal ClientControlType ClientControlType
        {
            get
            {
                return clientControlType;
            }

            set
            {
                clientControlType = value;
            }
        }

        public ClientWidgetDefaultValue ClientWidgetDefaultValue
        {
            get
            {
                return clientWidgetDefaultValue;
            }

            set
            {
                clientWidgetDefaultValue = value;
            }
        }
    }
    public class ClientText
    {
        ClientLable lable = new ClientLable();
        bool visible;
        int width;
        string css;
        string yy;
        string bds;
        string id;
        string @event;
        bool @readonly;      
        public ClientLable Lable
        {
            get
            {
                return lable;
            }

            set
            {
                lable = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }

        public string Yy
        {
            get
            {
                return yy;
            }

            set
            {
                yy = value;
            }
        }

        public string Bds
        {
            get
            {
                return bds;
            }

            set
            {
                bds = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Event
        {
            get
            {
                return @event;
            }

            set
            {
                @event = value;
            }
        }

        public bool Readonly
        {
            get
            {
                return @readonly;
            }

            set
            {
                @readonly = value;
            }
        }

      
 
    }

    /// <summary>
    /// 标签
    /// </summary>
    public class ClientLable
    {
        int width;
        bool visible;
        string css;
        string value;

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }

    public class ClientSelect
    {
        ClientLable clientLable;
        bool visible;
        int width;
        string css;
        string yy;
        string bds;
        string id;
        string @event;
        bool @readonly;
        
        string option;

        public ClientLable ClientLable
        {
            get
            {
                return clientLable;
            }

            set
            {
                clientLable = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }

        public string Yy
        {
            get
            {
                return yy;
            }

            set
            {
                yy = value;
            }
        }

        public string Bds
        {
            get
            {
                return bds;
            }

            set
            {
                bds = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Event
        {
            get
            {
                return @event;
            }

            set
            {
                @event = value;
            }
        }

        public bool Readonly
        {
            get
            {
                return @readonly;
            }

            set
            {
                @readonly = value;
            }
        }

        public string Option
        {
            get
            {
                return option;
            }

            set
            {
                option = value;
            }
        }

      
    }
    public class ClientButton
    {
        bool visible;
        int width;
        string css;
        string id;
        bool @readonly;
        string value;
        string @event;

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public bool Readonly
        {
            get
            {
                return @readonly;
            }

            set
            {
                @readonly = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public string Event
        {
            get
            {
                return @event;
            }

            set
            {
                @event = value;
            }
        }
    }
    public class ClientCheckbox
    {
        ClientLable clientLable;
        bool visible;
        int width;
        string css;
        string yy;
        string bds;
        string id;
        string @event;
        bool @readonly;
        

        public ClientLable ClientLable
        {
            get
            {
                return clientLable;
            }

            set
            {
                clientLable = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }

        public string Yy
        {
            get
            {
                return yy;
            }

            set
            {
                yy = value;
            }
        }

        public string Bds
        {
            get
            {
                return bds;
            }

            set
            {
                bds = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Event
        {
            get
            {
                return @event;
            }

            set
            {
                @event = value;
            }
        }

        public bool Readonly
        {
            get
            {
                return @readonly;
            }

            set
            {
                @readonly = value;
            }
        }

      
    }

    public class ClientTextarea
    {
        ClientLable lable = new ClientLable();
        bool visible;
        int width;
        string css;
        string yy;
        string bds;
        string id;
        string @event;
        bool @readonly;
        

        public ClientLable Lable
        {
            get
            {
                return lable;
            }

            set
            {
                lable = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }

        public string Yy
        {
            get
            {
                return yy;
            }

            set
            {
                yy = value;
            }
        }

        public string Bds
        {
            get
            {
                return bds;
            }

            set
            {
                bds = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Event
        {
            get
            {
                return @event;
            }

            set
            {
                @event = value;
            }
        }

        public bool Readonly
        {
            get
            {
                return @readonly;
            }

            set
            {
                @readonly = value;
            }
        }

       
    }
    public class ClientTd
    {
        int width;
        string css;
        string value;

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }
    public class ClientHref {
        ClientLable lable = new ClientLable();
        bool visible;
        string @event;
        bool @readonly;
        int width;
        string css;
        string value;

        public ClientLable Lable
        {
            get
            {
                return lable;
            }

            set
            {
                lable = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public string Event
        {
            get
            {
                return @event;
            }

            set
            {
                @event = value;
            }
        }

        public bool Readonly
        {
            get
            {
                return @readonly;
            }

            set
            {
                @readonly = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }

    public class ClientDate
    {
        ClientLable lable = new ClientLable();
        bool visible;
        string id;        
        string bds;
        string yy;
        int width;
        string css;

        public ClientLable Lable
        {
            get
            {
                return lable;
            }

            set
            {
                lable = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

   
        public string Bds
        {
            get
            {
                return bds;
            }

            set
            {
                bds = value;
            }
        }

        public string Yy
        {
            get
            {
                return yy;
            }

            set
            {
                yy = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Css
        {
            get
            {
                return css;
            }

            set
            {
                css = value;
            }
        }
    }
    
    enum ClientControlType
    {
        /// <summary>
        /// 文本
        /// </summary>
        text=0,
        /// <summary>
        /// 下拉框
        /// </summary>
        select=1,
        /// <summary>
        /// 按钮
        /// </summary>
        button=2,
        /// <summary>
        /// 筛选框
        /// </summary>
        checkbox=3,
        /// <summary>
        /// 文本域
        /// </summary>
        textarea=4,
        /// <summary>
        /// 纯文字
        /// </summary>
        td=5,
        /// <summary>
        /// 超链接
        /// </summary>
        href=6,
        /// <summary>
        /// 日期
        /// </summary>
        data=7
    }

    public class ClientTable
    {
        List<ClientWidget> widgetList = new List<ClientWidget>();

        public List<ClientWidget> WidgetList
        {
            get
            {
                return widgetList;
            }

            set
            {
                widgetList = value;
            }
        }
    }

    public class ClientDiv
    {
        DivType divType;
        object clientData;
        int width;
        int height;
        internal DivType DivType
        {
            get
            {
                return divType;
            }

            set
            {
                divType = value;
            }
        }

        public object ClientData
        {
            get
            {
                return clientData;
            }

            set
            {
                clientData = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }
    }

    public class ClientPage
    {
        string id;
        string url;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }
    }

    public class ClientTree
    {
        bool visible;
        int width;
        string id;
        string data;

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }
    }
    enum DivType
    {
        /// <summary>
        /// 空白
        /// </summary>
        empty=0,
        /// <summary>
        /// 控件集合
        /// </summary>
        table=1,
        /// <summary>
        /// 表格
        /// </summary>
        content=2,
        /// <summary>
        /// 内嵌页
        /// </summary>
        page=3,
        /// <summary>
        /// 树结构
        /// </summary>
        tree=4
        
    }
}
